using Application.Api.ViewModels;
using Domain.Core.Commands;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Seisicite.Api.Commands
{
  public class RegisterUserCommand : Command<Token>
  {
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; }
    [Required]
    public string Name { get; set; }
  }

  public class RegisterUserEvaluatorCommand : RegisterUserCommand
  {
    public List<string> Modalities { get; set; }
    public string Institution { get; set; }
    public bool IsSei { get; set; }
    public bool IsSicite { get; set; }
  }
}
