using Abp.BackgroundJobs;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using CrudAppProject.Configuration;
using CrudAppProject.EmailSender.EmailSenderEntities;
using CrudAppProject.EmailSender.EmailSenderManager;
using CrudAppProject.MultiTenancy;
using CrudAppProject.Orders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.BackgroundJobs.EmailJobs
{
    public class OrderEmailJob : AsyncBackgroundJob<long>, ITransientDependency
    {
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IEmailSenderManager _emailSenderManager;
        private readonly IRepository<Tenant, int> _tenantRepository;
        private readonly ISettingManager _settingManager;
        private readonly ILogger<OrderEmailJob> _logger;
        private readonly IRepository<QueuedEmail, long> _queuedRepository;

        public OrderEmailJob(IRepository<Order, long> orderRepository, IEmailSenderManager emailSenderManager, IRepository<Tenant, int> tenantRepository, ISettingManager settingManager, ILogger<OrderEmailJob> logger, IRepository<QueuedEmail, long> queuedRepository)
        {
            _orderRepository = orderRepository;
            _emailSenderManager = emailSenderManager;
            _tenantRepository = tenantRepository;
            _settingManager = settingManager;
            _logger = logger;
            _queuedRepository = queuedRepository;
        }
        [UnitOfWork]
        public override async Task<bool> ExecuteAsync(long Id)
        {
            try
            {
                var order = await _orderRepository.GetAsync(Id);
                if (order == null)
                {
                    throw new UserFriendlyException($"Order with ID {Id} not found.");
                }
                // Disable filters for tenant if needed
                using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
                {
                    var pendingQueuedEmails = await _queuedRepository.GetAllListAsync(email =>
                       email.IsSent == false
                        );

                    if (!pendingQueuedEmails.Any())
                    {
                        _logger.LogInformation($"No pending queued emails found for TenantId {order.TenantId}.");
                        return false;
                    }
                    foreach (var queuedEmail in pendingQueuedEmails)
                    {
                        var toEmail = queuedEmail.ToEmail;
                        var subject = queuedEmail.Subject;
                        var body = queuedEmail.Body;
                        // Fetch email settings for the tenant
                        var defaultSenderEmail = await _settingManager.GetSettingValueForTenantAsync(AppSettingNames.DefaultSenderEmail, (int)order.TenantId);
                        var smtpHost = await _settingManager.GetSettingValueForTenantAsync(AppSettingNames.SMTPHost, (int)order.TenantId);
                        var smtpPort = int.Parse(await _settingManager.GetSettingValueForTenantAsync(AppSettingNames.SMTPPort, (int)order.TenantId));
                        var userName = await _settingManager.GetSettingValueForTenantAsync(AppSettingNames.UserName, (int)order.TenantId);
                        var password = await _settingManager.GetSettingValueForTenantAsync(AppSettingNames.Password, (int)order.TenantId);
                        using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                        {
                            smtpClient.Credentials = new NetworkCredential(userName, password);
                            smtpClient.EnableSsl = true;
                            var mailMessage = new MailMessage
                            {
                                From = new MailAddress(defaultSenderEmail),
                                Subject = subject,
                                Body = body,
                                IsBodyHtml = true
                            };
                            mailMessage.To.Add(toEmail);
                            try
                            {
                                await smtpClient.SendMailAsync(mailMessage);

                                // Update the queued email status to "Sent" and increment retry count
                                queuedEmail.IsSent = true;
                                //queuedEmail.Status = OrderStatus.Created;
                                //queuedEmail.Status = "Sent";
                                await _queuedRepository.UpdateAsync(queuedEmail);

                                _logger.LogInformation($"Email sent successfully to {toEmail}.");
                            }
                            catch (Exception ex)
                            {
                                //queuedEmail.Status = "Failed";
                                queuedEmail.ErrorMessage = ex.Message;
                                queuedEmail.RetryCount++;
                                await _queuedRepository.UpdateAsync(queuedEmail);

                                _logger.LogError(ex, $"Error sending email to {toEmail}.");
                            }
                        }


                    }
                

                 }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OrderEmailJob execution.");
                return false;

            }

        }
    }
}
