using System;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using SporOkulu.Application.Interfaces;

namespace SporOkulu.Application.Services;

public class EmailManager : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");
        using var client = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"] ?? "587"))
        {
            Credentials = new NetworkCredential(smtpSettings["User"],smtpSettings["Pass"]),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpSettings["User"], "Spor Okulu Yönetimi"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);
        await client.SendMailAsync(mailMessage);
    }
}
