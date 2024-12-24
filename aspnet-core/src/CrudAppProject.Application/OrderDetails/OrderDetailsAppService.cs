using Abp.Domain.Repositories;
using CrudAppProject.OrderDetails.Dto;
using CrudAppProject.Orders;
using CrudAppProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.OrderDetails
{
    public class OrderDetailsAppService : CrudAppProjectAppServiceBase, IOrderDetailsAppService
    {
        private readonly IRepository<OrderDetail, long> _OrderDetailRepository;
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IRepository<Product, long> _productRepository;

        public OrderDetailsAppService(IRepository<OrderDetail, long> orderDetailRepository)
        {
            _OrderDetailRepository = orderDetailRepository;
        }
        public async Task<OrderDetailDto> CreateOrderDetail(OrderDetailDto input)
        {
            var orderDetailCount = await _OrderDetailRepository.CountAsync();



            var orderDetail = new OrderDetail
            {
                OrderId = (long)input.OrderId,
                ProductId = input.ProductId,
                Price = (long)input.Price,
                Quantity = (long)input.Quantity,
                Total = (long)input.Total,
                TenantId = AbpSession.TenantId ?? null,

            };

            var createdOrderDetail = await _OrderDetailRepository.InsertAsync(orderDetail);
            return ObjectMapper.Map<OrderDetailDto>(createdOrderDetail);

        }

    }
}
