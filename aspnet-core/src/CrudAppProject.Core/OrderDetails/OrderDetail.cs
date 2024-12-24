using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CrudAppProject.Orders;
using CrudAppProject.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.OrderDetails
{
    public class OrderDetail : FullAuditedEntity<long>, IMayHaveTenant
    {
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public long Price { get; set; }
        public long Quantity { get; set; }

        public long Total { get; set; }
        public int? TenantId { get; set; }

        [ForeignKey("OrderId")]
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
