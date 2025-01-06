using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using CrudAppProject.EmailSender.EmailSenderEntities;
using CrudAppProject.EmailSender.EmailTemplateManager;
using CrudAppProject.OrderDetails;
using CrudAppProject.Orders;
using CrudAppProject.ProductDetails;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.EmailSender.EmailSenderManager
{
    public class  EmailSenderManager : IEmailSenderManager , ITransientDependency
    {
        private readonly IAbpSession _abpSession;
        private readonly IEmailTemplateManager _templateManager;
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IRepository<OrderDetail, long> _orderDetailRepository;
        private readonly IRepository<ProductDetail, long> _productDetailRepository;
        private readonly IRepository<QueuedEmail, long> _queuedRepository;
        private readonly ILogger<EmailSenderManager> _logger;
        private readonly ISettingManager _settingManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public EmailSenderManager(IAbpSession abpSession, IEmailTemplateManager templateManager, IRepository<Order, long> orderRepository, IRepository<OrderDetail, long> orderDetailRepository, IRepository<ProductDetail, long> productDetailRepository, IRepository<QueuedEmail, long> queuedRepository, ILogger<EmailSenderManager> logger, ISettingManager settingManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _abpSession = abpSession;
            _templateManager = templateManager;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productDetailRepository = productDetailRepository;
            _queuedRepository = queuedRepository;
            _logger = logger;
            _settingManager = settingManager;
            _unitOfWorkManager = unitOfWorkManager;
        }
        [UnitOfWork]
        public async Task<bool> SendEmailAsync(string customerEmail, string orderNumber,  DateTime orderCreationTime, int? TenantId, long grandTotal,OrderStatus status)
        {
            string templateName = "";

            if (status == OrderStatus.Processing)
            {
                templateName = "Created";
            }
            else if (status == OrderStatus.Shipped)
            {
                templateName = "Dispatched";
            }
            else if (status == OrderStatus.Delivered)
            {
                templateName = "Delivered";
            }
            else if (status == OrderStatus.Cancelled)
            {
                templateName = "Failed";
            }
            var emailTemplate = await _templateManager.GetTemplateByIdAsync((int)TenantId, templateName);
            if (emailTemplate == null)
            {
                throw new Exception($"Email template for tenant ID {TenantId} not found.");
            }
            var tokenReplacements = new Dictionary<string, string>
        {
            { "{{customerEmail}}", customerEmail },
            { "{{orderNumber}}", orderNumber },
            { "{{orderCreationTime}}", orderCreationTime.ToString() },
            { "{{grandTotal}}", grandTotal.ToString() },
        };
            string emailContent = ReplaceTokens(emailTemplate.Content, tokenReplacements);
            var emailQueue = new QueuedEmail
            {
                ToEmail = customerEmail,
                Subject = emailTemplate.Subject,
                Body = emailContent,
                TenantId = TenantId,
                //Status = status,
                IsSent = false,
                RetryCount = 0

            };
            await _queuedRepository.InsertAsync(emailQueue);
            return true;

        }

        public Task<bool> TestMail(string To)
        {
            throw new NotImplementedException();
        }

        private string ReplaceTokens(string template, Dictionary<string, string> tokens)
        {
            if (string.IsNullOrEmpty(template))
                return template;

            foreach (var token in tokens)
            {
                template = template.Replace(token.Key, token.Value);
            }

            return template;
        }
    }

}