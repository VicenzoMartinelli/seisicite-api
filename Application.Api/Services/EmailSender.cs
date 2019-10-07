using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.Api.Services
{
  public class EmailSender
  {
    public async Task SendEmailAsync(string subject, string message, params string[] emails)
    {
      using (var smtp = new SmtpClient("smtp.gmail.com"))
      {
        smtp.EnableSsl = true;
        smtp.Port = 587;
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;

        smtp.Credentials = new NetworkCredential("comissaoseisicite@gmail.com", "se5a6P8f");

        var mailMessage = new MailMessage
        {
          From = new MailAddress("comissaoseisicite@gmail.com"),
          Body = message,
          IsBodyHtml = true,
          Subject = "Sei Sicite 2019 - " + subject
        };

        foreach (var email in emails)
        {
          mailMessage.To.Add(email);
        }

        await smtp.SendMailAsync(mailMessage);
      }
    }

  }
}
