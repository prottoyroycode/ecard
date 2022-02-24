using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using ViewModels.RockVilleResponse;

namespace Services.InterfaceImplementations
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string subject, string html, string from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ecardgakk@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com",587,SecureSocketOptions.StartTls);
            smtp.Authenticate("ecardgakk@gmail.com", "ecardgakk-200");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

       

      public async Task SendInvoiceMail(string to, string subject, CardResultResponse cardResultResponse , OrderResponseForEmail orderResponseForEmail)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            var today = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
            MailText = MailText.Replace("[pin_code]", cardResultResponse.card_number).Replace("[card_number]", cardResultResponse.pin_code)
            .Replace("[product_Name]", orderResponseForEmail.productName).Replace("[todaydate]", today).
            Replace("[order_price]", orderResponseForEmail.price.ToString());
            var email = new MimeMessage();
           
            email.From.Add(MailboxAddress.Parse("ecardgakk@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ecardgakk@gmail.com", "ecardgakk-200");
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            //email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //email.To.Add(MailboxAddress.Parse(request.ToEmail));
            //email.Subject = $"Welcome {request.UserName}";
            //var builder = new BodyBuilder();
            //builder.HtmlBody = MailText;
            //email.Body = builder.ToMessageBody();
            //using var smtp = new SmtpClient();
            //smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            //smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            //await smtp.SendAsync(email);
            //smtp.Disconnect(true);

            //string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            //StreamReader str = new StreamReader(FilePath);
            //string MailText = str.ReadToEnd();
            //string BODY = File.ReadAllText(FilePath);

            //str.Close();
            //BODY.Replace("[userName]", "hi");
            //var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse("ecardgakk@gmail.com"));
            //email.To.Add(MailboxAddress.Parse(to));
            //email.Subject = subject;
            //var builder = new BodyBuilder();
            //builder.HtmlBody = BODY;
            //email.Body = builder.ToMessageBody();
            //using var smtp = new SmtpClient();
            //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate("ecardgakk@gmail.com", "ecardgakk-200");
            //await smtp.SendAsync(email);
            //smtp.Disconnect(true);

        }
    }
}
