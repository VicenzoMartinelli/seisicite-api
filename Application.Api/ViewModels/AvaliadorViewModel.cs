using Newtonsoft.Json;

namespace Application.Api.ViewModels
{
  [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
  public class AvaliadorViewModel
  {
    public string Institution { get; set; }
    public string[] AttendedModalities { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Id { get; set; }
    public bool IsSei { get; set; }
    public bool IsSicite { get; set; }
  }
}
