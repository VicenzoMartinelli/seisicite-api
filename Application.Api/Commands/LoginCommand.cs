using Application.Api.ViewModels;
using Domain.Core.Commands;
using System.ComponentModel.DataAnnotations;

namespace Services.Seisicite.Api.Commands
{
  public class LoginCommand : Command<Token>
  {
    private string _email;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email
    {
      get => _email;
      set => _email = value?.Trim();
    }

    [Required(ErrorMessage = "O campo Senha é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo Senha precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }

  }
}
