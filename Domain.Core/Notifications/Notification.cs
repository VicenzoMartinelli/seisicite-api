using Newtonsoft.Json;
using System;
using System.Net;

namespace Domain.Core.Notifications
{
  public class Notification
  {
    public string Field { get; }
    public string ErrorCode { get; }
    public string Value { get; }
    public string Message { get; }
    [JsonIgnore]
    public int? StatusCode { get; }

    public DateTime OccurrenceDate { get; } = DateTime.UtcNow;

    public Notification(string field, string errorCode, string value, string message, int? statusCode)
    {
      Field          = field;
      ErrorCode      = errorCode;
      Value          = value;
      Message        = message;
      StatusCode     = statusCode;
    }

    public Notification(string field, string errorCode, string message)
    {
      Field     = field;
      ErrorCode = errorCode;
      Message   = message;
    }

    public override bool Equals(object obj)
    {
      return obj is Notification notification &&
             ErrorCode == notification.ErrorCode &&
             Message == notification.Message;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(ErrorCode, Message);
    }
  }
}
