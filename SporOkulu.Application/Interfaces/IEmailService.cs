using System;

namespace SporOkulu.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject,string body);
}
