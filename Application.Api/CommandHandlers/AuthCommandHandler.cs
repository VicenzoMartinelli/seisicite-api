using Application.Api.Commands;
using Application.Api.Services;
using Application.Api.ViewModels;
using Domain.Core.Enumerators;
using Domain.Core.Notifications;
using Domain.Domains.Article;
using Domain.Interfaces;
using Infra.Data.MongoIdentityStore;
using Infra.Data.MongoIdentityStore.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Seisicite.Api.Commands;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Seisicite.Api.CommandHandlers
{
  public class AuthCommandHandler : 
      IRequestHandler<RegisterUserCommand, Token>, 
      IRequestHandler<RegisterUserEvaluatorCommand, Token>, 
      IRequestHandler<LoginCommand, Token>,
      IRequestHandler<ResetPasswordCommand, bool>
  {
    private readonly SignInManager<MongoIdentityUser> _signInManager;
    private readonly UserManager<MongoIdentityUser> _userManager;
    private readonly NotificationContext _notificationContext;
    private readonly SecuritySettings _secSettings;
    private readonly IRepository _repository;
    private readonly EmailSender _emailSender;

    public AuthCommandHandler(
      SignInManager<MongoIdentityUser> signInManager,
      UserManager<MongoIdentityUser> userManager,
      NotificationContext notificationContext,
      SecuritySettings secSettings,
      IRepository repository,
      EmailSender emailSender)
    {
      _signInManager = signInManager;
      _userManager = userManager;
      _notificationContext = notificationContext;
      _secSettings = secSettings;
      _repository = repository;
      _emailSender = emailSender;
    }

    public async Task<Token> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
      var identityUser = new MongoIdentityUser(request.Email, request.Email);

      identityUser.AddClaim(new MongoUserClaim("Email", request.Email));
      identityUser.AddClaim(new MongoUserClaim("Type", EUserType.Committee.ToString()));

      var result = await _userManager.CreateAsync(identityUser, request.Password);

      if (!result.Succeeded)
      {
        foreach (int er in result.Errors.Select(x => Convert.ToInt32(x.Code)))
        {
          _notificationContext.PushNotification((ReturnCode)er);
        }
        return null;
      }

      var user = new Person()
      {
        IdentityUserId = identityUser.Id,
        Name           = request.Name,
        Email          = request.Email,
        Type           = EUserType.Committee,
        Approved       = true,
        ArticlesCount  = 0
      };

      await _repository.AddAsync<Person>(user);

      await _signInManager.SignInAsync(identityUser, false);

      return await GerarJwt(identityUser);
    }

    public async Task<Token> Handle(RegisterUserEvaluatorCommand request, CancellationToken cancellationToken)
    {
      var identityUser = new MongoIdentityUser(request.Email, request.Email);

      identityUser.AddClaim(new MongoUserClaim("Email", request.Email));
      identityUser.AddClaim(new MongoUserClaim("Type", EUserType.Evaluator.ToString()));

      var result = await _userManager.CreateAsync(identityUser, request.Password);

      if (!result.Succeeded)
      {
        foreach (int er in result.Errors.Select(x => Convert.ToInt32(x.Code)))
        {
          _notificationContext.PushNotification((ReturnCode)er);
        }
        return null;
      }

      await _repository.AddAsync<Person>(new Person()
      {
        IdentityUserId     = identityUser.Id,
        Name               = request.Name,
        IsSei              = request.IsSei,
        Email              = request.Email,
        IsSicite           = request.IsSicite,
        AttendedModalities = request.Modalities,
        Institution        = request.Institution,
        Type               = EUserType.Evaluator,
        Approved           = false,
        ArticlesCount      = 0
      });

      await _signInManager.SignInAsync(identityUser, false);

      return await GerarJwt(identityUser);
    }

    public async Task<Token> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
      if (cancellationToken.IsCancellationRequested)
      {
        return default;
      }

      var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

      if (!result.Succeeded)
      {
        if (result.IsLockedOut)
        {
          _notificationContext.PushNotification(ReturnCode.UsuarioBloqueado);
        }
        else
        {
          _notificationContext.PushNotification(ReturnCode.DadosDeLoginInvalidos);
        }

        return default;
      }

      var identityUser = await _userManager.FindByEmailAsync(request.Email);
      var user         = _repository.Query<Person>().FirstOrDefault(x => x.Email == request.Email);

      if (!user.Approved)
      {
        _notificationContext.PushNotification(ReturnCode.UsuarioNaoAprovado);
        return null;
      }

      var tokenResult = await GerarJwt(identityUser);

      tokenResult.Email      = user.Email;
      tokenResult.IdentityId = identityUser.Id;
      tokenResult.Name       = user.Name;
      tokenResult.IsSei      = user.IsSei;
      tokenResult.IsSicite   = user.IsSicite;
      tokenResult.PersonId   = user.Id;
      
      return tokenResult;
    }

    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
      var identityUser = await _userManager.FindByEmailAsync(request.Email);

      if (identityUser == null)
      {
        return false;
      }

      var newPass = new PassowordGenerator().Generate();

      var r1 = await _userManager.RemovePasswordAsync(identityUser);
      var r2 = await _userManager.AddPasswordAsync(identityUser, newPass);

      if (!(r1.Succeeded && r2.Succeeded))
      {
        return false;
      }

      try
      {
        await _emailSender.SendEmailAsync("Nova senha", $"Olá, sua nova senha para acesso ao Avaliador SeiSicite é {newPass}", request.Email);
      }
      catch (Exception)
      {
        try
        {
          await _emailSender.SendEmailAsync("Nova senha", $"Olá, sua nova senha para acesso ao Avaliador SeiSicite é {newPass}", request.Email);
        }
        catch (Exception)
        {
          try
          {
            await _emailSender.SendEmailAsync("Nova senha", $"Olá, sua nova senha para acesso ao Avaliador SeiSicite é {newPass}", request.Email);
          }
          catch (Exception)
          {
            try
            {
              await _emailSender.SendEmailAsync("Nova senha", $"Olá, a senha do email {request.Email} foi trocada mas o envio do email nao foi feito, a nova senha para acesso ao Avaliador SeiSicite é {newPass}", "martinellivicenzo@gmail.com");
            }
            catch (Exception)
            {
            }
          }
        }

      }

      return true;

    }
    private async Task<Token> GerarJwt(MongoIdentityUser user)
    {
      var identityClaims = new ClaimsIdentity();

      identityClaims.AddClaims(await _userManager.GetClaimsAsync(user));

      var tokenHandler = new JwtSecurityTokenHandler();
      byte[] key = Encoding.ASCII.GetBytes(_secSettings.Secret);
      var created = DateTime.UtcNow;
      var valid = created.AddHours(_secSettings.DurationHours);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = identityClaims,
        Issuer = _secSettings.Issuer,
        Expires = valid,
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      return new Token()
      {
        Created = created.ToString(),
        Expiration = valid.ToString(),
        AccessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor))
      };
    }
  }
}
