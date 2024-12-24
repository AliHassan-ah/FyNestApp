using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudAppProject.OrderDetails;

namespace CrudAppProject.Orders
{
    public class Order : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public long grandTotal { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string Email { get; set; }

        public ICollection<OrderDetail> OrderDetail { get; set; }

    }
}
