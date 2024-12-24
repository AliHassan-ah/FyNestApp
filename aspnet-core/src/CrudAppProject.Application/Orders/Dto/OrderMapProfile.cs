using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Orders.Dto
{
    public class OrderMapProfile: Profile
    {
        public OrderMapProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
           

        }
    }
}
