using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Timing;
using Abp.UI;
using CrudAppProject.BackgroundJobs.EmailJobs;
using CrudAppProject.EmailSender.EmailSenderManager;
using CrudAppProject.OrderDetails;
using CrudAppProject.Orders.Dto;
using CrudAppProject.ProductDetails;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using CrudAppProject.EmailSender.EmailSenderEntities;
using CrudAppProject.Products;

namespace CrudAppProject.Orders
{
    public class OrderAppService : CrudAppProjectAppServiceBase, IOrderAppService
    {
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IRepository<OrderDetail, long> _orderDetailRepository;
        private readonly IRepository<ProductDetail, long> _productDetailRepository;
        private readonly IEmailSenderManager _emailSenderManager;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly IRepository<QueuedEmail, long> _queuedRepository;
        private readonly IRepository<Product, long> _productRepository;






        public OrderAppService(
            IRepository<Order, long> orderRepository,
            IRepository<OrderDetail, long> orderDetailRepository,
            IRepository<ProductDetail, long> productDetailRepository,
            IEmailSenderManager emailSenderManager,
            IBackgroundJobManager backgroundJobManager,
            IRepository<QueuedEmail, long> queuedRepository,
            IRepository<Product, long> productRepository
)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository; 
            _productDetailRepository = productDetailRepository;
            _emailSenderManager = emailSenderManager;
            _backgroundJobManager = backgroundJobManager;
            _queuedRepository = queuedRepository;
            _productRepository = productRepository;

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
            order.Status = OrderStatus.Processing;

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
            await _emailSenderManager.SendEmailAsync(order.Email, order.Id.ToString(), order.CreationTime, order.TenantId, order.grandTotal,OrderStatus.Processing);
            BackgroundJob.Schedule<OrderEmailJob>((jobArgs) => jobArgs.ExecuteAsync(createdOrderId), Clock.Now.AddSeconds(30));
            return $"Order {createdOrderId} created successfully.";
        }


        public async Task<Abp.Application.Services.Dto.PagedResultDto<OrderDto>> GetAllOrders(PagedOrderResultRequestDto input)
        {
            var query = await _orderRepository.GetAllListAsync();
            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                query = query.Where(order =>
                    order.Email.ToString().Contains(input.Keyword) ||
                    order.Id.ToString().Contains(input.Keyword).ToString().Contains(input.Keyword)).ToList();
            }
            var totalCount = query.Count;

            var pagedOrders = query.OrderBy(order => order.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            var result = ObjectMapper.Map<List<OrderDto>>(pagedOrders);

            return new Abp.Application.Services.Dto.PagedResultDto<OrderDto>(totalCount, result);

        }

        public async Task<OrderDto> GetSingleOrder(long id)
        {
            var order = await _orderRepository.GetAsync(id);
            return ObjectMapper.Map<OrderDto>(order);
        }

        public async Task<string> UpdateOrderStatus(OrderDto input)
        {
            var existingOrder = await _orderRepository.FirstOrDefaultAsync(input.Id);
            if (existingOrder == null)
            {
                throw new UserFriendlyException($"Order with ID {input.Id} not found.");
            }

            ObjectMapper.Map(input, existingOrder);
            existingOrder.Status = input.Status;
            await _orderRepository.UpdateAsync(existingOrder);
            await _emailSenderManager.SendEmailAsync(existingOrder.Email, existingOrder.Id.ToString(), existingOrder.CreationTime, existingOrder.TenantId, existingOrder.grandTotal, (OrderStatus)input.Status);
            BackgroundJob.Schedule<OrderEmailJob>((jobArgs) => jobArgs.ExecuteAsync(input.Id), Clock.Now.AddSeconds(30));

            return $"Order {input.Id} updated successfully.";
        }

            public async Task<List<OrderWithProductsDto>> GetOrdersWithProductsAsync(List<OrderDto> orders)
        {
            var orderIds = orders.Select(o => o.Id).ToList();

            var fetchedOrders = await _orderRepository.GetAllListAsync(o => orderIds.Contains(o.Id));

            var orderDetails = await _orderDetailRepository.GetAllListAsync(od => orderIds.Contains(od.OrderId));

            var productIds = orderDetails.Select(od => od.ProductId).Distinct();
            var products = await _productRepository.GetAllListAsync(p => productIds.Contains(p.Id));

            var result = fetchedOrders.Select(order => new OrderWithProductsDto
            {
                OrderId = order.Id,
                Products = orderDetails
                    .Where(od => od.OrderId == order.Id)
                    .Join(
                        products,
                        od => od.ProductId,
                        p => p.Id,
                        (od, p) => new ProductDetailsDto
                        {
                            ProductId = p.Id,
                            ProductName = p.ProductName,
                            ProductDescription = p.ProductDescription,
                            ProductThumbnail = p.ProductThumbnail
                        }
                    ).ToList()
            }).ToList();

            return result;
        }

    }
}
