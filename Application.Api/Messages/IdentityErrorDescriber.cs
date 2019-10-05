using Domain.Core.Enumerators;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Api.Messages
{
  public class PersonalIdentityErrorDescriber : Microsoft.AspNetCore.Identity.IdentityErrorDescriber
  {
    private IdentityError GetErrorByReturnCode(ReturnCode code)
    {
      return new IdentityError()
      {
        Code = ((int) code).ToString(),
        Description = code.GetDisplayValue()
      };
    }
    public override IdentityError DefaultError()
    {
      return GetErrorByReturnCode(ReturnCode.ImpossivelFinalizarAcao);
    }

    public override IdentityError ConcurrencyFailure()
    {
      return DefaultError();
    }

    public override IdentityError DuplicateEmail(string email)
    {
      return GetErrorByReturnCode(ReturnCode.EmailExiste);
    }

    public override IdentityError DuplicateUserName(string userName)
    {
      return GetErrorByReturnCode(ReturnCode.EmailExiste);
    }

    public override IdentityError PasswordMismatch()
    {
      return GetErrorByReturnCode(ReturnCode.UsuarioOuSenhaErrados);
    }

    public override IdentityError PasswordTooShort(int length)
    {
      return GetErrorByReturnCode(ReturnCode.TamanhoSenhaInvalido);
    }
  }
}
