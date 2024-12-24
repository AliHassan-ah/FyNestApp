using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.UI;
using Castle.Core.Resource;
using CrudAppProject.OrderDetails;
using CrudAppProject.Orders.Dto;
using CrudAppProject.ProductDetails;
using CrudAppProject.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Orders
{
    public class OrderAppService :CrudAppProjectAppServiceBase,IOrderAppService
    {
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IRepository<OrderDetail, long> _orderDetailRepository;
        private readonly IRepository<ProductDetail, long> _productDetailRepository;



        public OrderAppService(IRepository<Order, long> orderRepository
            , IRepository<OrderDetail, long> orderDetailRepository,
            IRepository<ProductDetail, long> productDetailRepository
            )
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productDetailRepository = productDetailRepository;
        }
        public async Task<string> CreateOrder(OrderDto input)
        {
            var order = ObjectMapper.Map<Order>(input);

            foreach (var item in input.OrderDetails)
            {
                var product = await _productDetailRepository.GetAsync(item.ProductId);
                if (item.Quantity > product.Quantity)
                {
                    throw new UserFriendlyException($"Insufficient quantity available for product ID {product.ProductId}.");
                }
            }
            //order.Id = null;
            
            var createdOrderId = await _orderRepository.InsertAndGetIdAsync(order);

            foreach (var orderDetailDto in input.OrderDetails)
            {
                var productDetail = await _productDetailRepository.GetAsync(orderDetailDto.ProductId);

                productDetail.Quantity -= (int)orderDetailDto.Quantity;
                await _productDetailRepository.UpdateAsync(productDetail);

                orderDetailDto.OrderId = createdOrderId;
                var orderDetail = ObjectMapper.Map<OrderDetail>(orderDetailDto);
                await _orderDetailRepository.InsertAsync(orderDetail);
            }
            return $"Order {createdOrderId} created successfully.";
        }

    }
}
    