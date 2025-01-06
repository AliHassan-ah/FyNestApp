using Abp;
using Abp.Application.Services;
using Abp.Runtime.Session;
using CrudAppProject.Configuration.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Configuration
{
    public class AppSettingAppService : ApplicationService
    {
        private readonly IAbpSession _abpSession;
        public AppSettingAppService(IAbpSession abpSession)
        {
            _abpSession = abpSession;
        }
        public async Task EditEmailSettings(EditEmailSettingsDto input)
        {
            var tenantId = _abpSession.TenantId;
            if (!tenantId.HasValue)
            {
                throw new AbpException("Tenant ID is not available.");
            }

            await SettingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.DefaultSenderEmail, input.DefaultSenderEmail);
            await SettingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.SMTPHost, input.SMTPHost);
            await SettingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.SMTPPort, input.SMTPPort.ToString());
            await SettingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.UserName, input.UserName);
            await SettingManager.ChangeSettingForTenantAsync(tenantId.Value, AppSettingNames.Password, input.Password);
        }
        public async Task<EditEmailSettingsDto> GetEmailSettingsAsync()
        {
            var tenantId = _abpSession.TenantId;
            if (!tenantId.HasValue)
            {
                throw new AbpException("Tenant ID is not available.");
            }

            return new EditEmailSettingsDto
            {
                DefaultSenderEmail = await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.DefaultSenderEmail, tenantId.Value),
                SMTPHost = await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.SMTPHost, tenantId.Value),
                SMTPPort = int.Parse(await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.SMTPPort, tenantId.Value)),
                UserName = await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.UserName, tenantId.Value),
                Password = await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.Password, tenantId.Value)
            };
        }
        public async Task<string> TestEmailAsync(EditEmailSettingsDto input)
        {
            var tenantId = _abpSession.TenantId;
            if (!tenantId.HasValue)
            {
                throw new AbpException("Tenant ID is not available.");
            }

            try
            {
                var emailSettings = new EditEmailSettingsDto
                {
                    DefaultSenderEmail = await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.DefaultSenderEmail, tenantId.Value),
                    SMTPHost = await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.SMTPHost, tenantId.Value),
                    SMTPPort = int.Parse(await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.SMTPPort, tenantId.Value)),
                    UserName = await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.UserName, tenantId.Value),
                    Password = await SettingManager.GetSettingValueForTenantAsync(AppSettingNames.Password, tenantId.Value)
                };

                var smtpClient = new SmtpClient("smtp.ethereal.email", 587)
                {
                    Credentials = new NetworkCredential("pablo57@ethereal.email", "JKQasZsgaau1eUjD1D"),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailSettings.DefaultSenderEmail),
                    Subject = "Test Email",
                    Body = "This is a test email to confirm SMTP settings.",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(input.TestEmail);

                await smtpClient.SendMailAsync(mailMessage);
                return "Email sent successfully.";
            }
            catch (Exception ex)
            {
                return $"Email sending failed: {ex.Message}";
            }
        }
    }
}
