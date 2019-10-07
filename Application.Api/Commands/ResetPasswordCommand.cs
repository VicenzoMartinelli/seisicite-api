using MediatR;

namespace Application.Api.Commands
{
  public class ResetPasswordCommand : IRequest<bool>
  {
    public string Email { get; private set; }

    public ResetPasswordCommand setEmail(string email)
    {
      Email = email;

      return this;
    }
  }
}
