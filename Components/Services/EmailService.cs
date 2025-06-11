using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpSettings = _configuration.GetSection("Smtp");
        var fromAddress = smtpSettings["Address"];
        var password = smtpSettings["Password"];
        var smtpHost = smtpSettings["SmtpHost"];
        var smtpPort = int.Parse(smtpSettings["SmtpPort"]);

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromAddress),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(toEmail);

        using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
        {
            smtpClient.Credentials = new NetworkCredential(fromAddress, password);
            smtpClient.EnableSsl = true;

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
        
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
