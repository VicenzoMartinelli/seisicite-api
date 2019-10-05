namespace Application.Api.ViewModels
{
  public class Token
  {
    public string IdentityId { get; set; }
    public string PersonId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsSei { get; set; }
    public bool IsSicite { get; set; }
    public string Created { get; set; }
    public string Expiration { get; set; }
    public string AccessToken { get; set; }
  }
}
