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
    public class OrderDto: EntityDto<long>
    {
        public int? TenantId { get; set; }
        public long GrandTotal { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string Email { get; set; }
        public DateTime CreationTime { get; set; }
        //public string? OrderStatus { get; set; }
        public OrderStatus? Status { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }
    }
    public class PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
