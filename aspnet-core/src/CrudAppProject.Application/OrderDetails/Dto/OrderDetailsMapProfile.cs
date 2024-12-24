using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.OrderDetails.Dto
{
    public class OrderDetailsMapProfile : Profile
    {
        public OrderDetailsMapProfile()
        {
            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>();

        }
    }
}
