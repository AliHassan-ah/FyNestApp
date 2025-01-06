using Abp.Application.Services;
using Abp.Dependency;
using CrudAppProject.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.EmailSender.EmailSenderManager
{
    public interface IEmailSenderManager : IApplicationService, ITransientDependency
    {
        Task<bool> SendEmailAsync(string customerEmail, string orderNumber, DateTime orderCreationTime, int? TenantId, long grandTotal, OrderStatus status);
        Task<bool> TestMail(string To);
    }
}
