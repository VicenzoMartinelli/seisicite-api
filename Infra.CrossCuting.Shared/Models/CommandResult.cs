namespace Infra.CrossCuting.Shared.Models
{
  public class CommandResult<T>
  {
    public bool IsSuccess { get; set; }
    public T Result { get; set; }

    public CommandResult(bool isSuccess)
    {
      IsSuccess = isSuccess;
    }

    public CommandResult(bool isSuccess, T result)
    {
      IsSuccess = isSuccess;
      Result = result;
    }
  }

}
