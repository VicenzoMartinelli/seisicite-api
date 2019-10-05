namespace Application.Api.ViewModels
{
  public class SecuritySettings
  {
    public string Secret { get; set; }
    public int DurationHours { get; set; }
    public string Issuer { get; set; }
  }
}
