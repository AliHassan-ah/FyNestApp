using Abp.Domain.Entities;
using CrudAppProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.OrderDetails.Dto
{
    public class OrderDetailDto : Entity<long ?>
    {
        public long ? OrderId { get; set; }
        public long ProductId { get; set; }
        public long Price { get; set; }
        public long Quantity { get; set; }

        public long Total { get; set; }
    }
}
