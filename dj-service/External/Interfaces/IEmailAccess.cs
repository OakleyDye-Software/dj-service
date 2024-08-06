using Microsoft.AspNetCore.Mvc;
using SendGrid;

namespace dj_service;

public interface IEmailAccess
{
    Task<Response> SendEmailAsync(ClientInfo clientInfo, string toAddress, string fullName, string subject, string plainTextContent, string htmlContent, string replyTo = "", string replyToName = "", string cc = "", string ccName = "");
}
