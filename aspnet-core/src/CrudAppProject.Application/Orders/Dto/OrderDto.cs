using Abp.Application.Services.Dto;
using CrudAppProject.OrderDetails;
using CrudAppProject.OrderDetails.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Orders.Dto
{
    public class OrderDto 
    {
        public int? TenantId { get; set; }
        public long GrandTotal { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string Email { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
