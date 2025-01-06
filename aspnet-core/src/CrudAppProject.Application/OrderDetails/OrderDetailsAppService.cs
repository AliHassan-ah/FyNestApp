using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CrudAppProject.OrderDetails.Dto;
using CrudAppProject.Orders;
using CrudAppProject.Products;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Abp.Application.Services.Dto.PagedResultDto<OrderDetailDto>> GetAllOrderDetails(PagedOrderDetailsResultRequestDto input)
        {
            var query = await _OrderDetailRepository.GetAllListAsync();

            //if (!string.IsNullOrWhiteSpace(input.Keyword))
            //{
            //    query = query.Where(order =>
            //        order.OrderNumber.ToString().Contains(input.Keyword)).ToList();
            //}
            var totalCount = query.Count;

            var pagedOrderDetails = query.OrderBy(order => order.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            var result = ObjectMapper.Map<List<OrderDetailDto>>(pagedOrderDetails);

            return new Abp.Application.Services.Dto.PagedResultDto<OrderDetailDto>(totalCount, result);
        }
        public async Task<OrderDetailDto> GetSingleOrderDetail(long id)
        {
            var orderDetail = await _OrderDetailRepository.GetAsync(id);
            return ObjectMapper.Map<OrderDetailDto>(orderDetail);
        }

        public async Task<ListResultDto<OrderDetailDto>> GetAllOrderDetailsByOrderId(long orderId)
        {
            //To GWet All Order Details with same Order ID (Means all details while creating an Order)
            var orderDetails = await _OrderDetailRepository.GetAll()
                .Where(od => od.OrderId == orderId)
                .ToListAsync();

            return new ListResultDto<OrderDetailDto>(
                ObjectMapper.Map<List<OrderDetailDto>>(orderDetails)
            );
        }

    }
}
