using Domain.Core;
using Domain.Core.Enumerators;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Domain.Core.Notifications
{
  public class NotificationContext
  {
    private List<Notification> _notificacoes;
    public IReadOnlyCollection<Notification> Notificacoes => _notificacoes;
    public virtual bool AnyNotification => _notificacoes.Any();

    public NotificationContext()
    {
      _notificacoes = new List<Notification>();
    }

    public void NotFound(string errorCode, string message)
    {
      PushNotification(string.Empty, errorCode, string.Empty, message, 404);
    }

    public void BadRequest(string errorCode, string message)
    {
      PushNotification(string.Empty, errorCode, string.Empty, message, 400);
    }

    public void BadRequest(string field, string errorCode, string message)
    {
      PushNotification(field, errorCode, string.Empty, message, 400);
    }

    public void BadRequest(string field, string errorCode, string valor, string message)
    {
      PushNotification(field, errorCode, valor, message, 400);
    }

    public void BadRequest(IEnumerable<Notification> notificacoes)
    {
      AddNotificationList(notificacoes);
    }

    public void PushNotification(ReturnCode code, string field = null)
    {
      _notificacoes.Add(new Notification(field, code.GetNumberStringEnumValue(), code.GetDisplayValue()));
    }
    public void PushNotification(string field, string errorCode, string message)
    {
      _notificacoes.Add(new Notification(field, errorCode, message));
    }
    public void PushNotification(string field, string errorCode, string valor, string message, int status)
    {
      _notificacoes.Add(new Notification(field, errorCode, valor, message, status));
    }
    public void PushNotification(string field, string errorCode, string valor, string message)
    {
      _notificacoes.Add(new Notification(field, errorCode, valor, message, null));
    }

    public void AddNotificationList(IEnumerable<Notification> notificacoes)
    {
      _notificacoes.AddRange(notificacoes);
    }

    public virtual IEnumerable<Notification> GetNotifications()
    {
      return _notificacoes;
    }
    public virtual void UnionNotifications(IEnumerable<Notification> notifications)
    {
      _notificacoes = _notificacoes.Union(notifications).ToList();

    }
  }
}
