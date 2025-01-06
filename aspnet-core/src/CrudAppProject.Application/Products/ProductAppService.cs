using Abp.Domain.Repositories;
using Abp.Timing;
using Abp.UI;
using CrudAppProject.BackgroundJobs.EmailJobs;
using CrudAppProject.Images;
using CrudAppProject.Orders.Dto;
using CrudAppProject.Orders;
using CrudAppProject.ProductDetails;
using CrudAppProject.Products.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudAppProject.ProductReviews;
using CrudAppProject.Authorization.Users;

namespace CrudAppProject.Products
{
    public class ProductAppService : CrudAppProjectAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product,long> _productRepository;
        private readonly IRepository<ProductDetail, long> _productDetailRepository;
        private readonly IRepository <Image,long> _imageRepository;
        private readonly IRepository<ProductReview, long> _productReviewRepository;
        private readonly UserManager _userManager;


        public ProductAppService(IRepository<Product, long> productRepository,
                                 IRepository<ProductDetail, long> productDetailRepository,
                                 IRepository<Image, long> imageRepository,
                                  IRepository<ProductReview, long> productReviewRepository,
                                   UserManager userManager
            )
        {
            _productRepository = productRepository;
            _productDetailRepository = productDetailRepository;
            _imageRepository = imageRepository;
            _productReviewRepository = productReviewRepository;
            _userManager = userManager;
        }
        public async Task<ProductWithDetailDto> CreateProductWithDetailsAsync(ProductWithDetailDto input)
        {
            var product = new Product
            {
                TenantId = (int)AbpSession.TenantId,
                ProductName = input.ProductName,
                ProductDescription = input.ProductDescription,
                Images = input.Images.Select(imageUrl => new Image
                {
                    ImageUrl = imageUrl
                }).ToList(),
                ProductThumbnail = input.ProductThumbnail
            };

           var productId = await _productRepository.InsertAndGetIdAsync(product);
            var productDetails = new ProductDetail
            {
                TenantId = (int)AbpSession.TenantId,
                Status = input.Status,
                Quantity = input.Quantity,
                BarCode = input.BarCode,
                Price = input.Price,
                CategoryId = input.CategoryId,
                ProductId = productId,


            };
            await _productDetailRepository.InsertAsync(productDetails);
            var savedProduct = await _productRepository.GetAllIncluding(p => p.Images)
                                                        .FirstOrDefaultAsync(p => p.Id == productId);

            var result = ObjectMapper.Map<ProductWithDetailDto>(savedProduct);
            return result;
        }

        //getAll
        public async Task<Abp.Application.Services.Dto.PagedResultDto<ProductWithDetailDto>> GetAllProductsWithDetailsAsync(PagedProductResultRequestDto input)
        {
            var query = _productRepository.GetAllIncluding(p => p.Images, p => p.ProductDetails)
                                  .Where(p => p.TenantId == (int)AbpSession.TenantId);
            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                query = query.Where(product =>
                product.ProductName.Contains(input.Keyword) ||
                product.ProductDescription.Contains(input.Keyword)
                ); 
            }
            var totalCount = await query.CountAsync();
            var pagedProducts = query.OrderByDescending(product=>product.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var productRatings = await _productReviewRepository.GetAll()
                                     .GroupBy(r => r.ProductId)
                                     .Select(group => new
                                     {
                                         ProductId = group.Key,
                                         AverageRating = (int?)Math.Round((decimal)group.Average(r => r.Rating), MidpointRounding.AwayFromZero)
                                     })
                                     .ToListAsync();
            foreach (var rating in productRatings)
            {
                var productDetail = await _productDetailRepository.FirstOrDefaultAsync(pd => pd.ProductId == rating.ProductId);
                if (productDetail != null)
                {
                    productDetail.Rating = rating.AverageRating;
                    await _productDetailRepository.UpdateAsync(productDetail);
                }
            }
            var result = ObjectMapper.Map<List<ProductWithDetailDto>>(pagedProducts);


            foreach (var product in result)
            {
                var rating = productRatings.FirstOrDefault(r => r.ProductId == product.Id)?.AverageRating;
                product.Rating = rating; 
            }
            return new Abp.Application.Services.Dto.PagedResultDto<ProductWithDetailDto>(totalCount, result);

        }

        //getSingle
        public async Task<ProductWithDetailDto> GetSingleProductWithDetailsAsync(long productId) {
           var product = await _productRepository.GetAllIncluding(p => p.Images,p => p.ProductDetails)
                .Where(p=>p.Id == productId && p.TenantId== (int)AbpSession.TenantId)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                throw new UserFriendlyException("Product not found");
            }
            var result = ObjectMapper.Map<ProductWithDetailDto>(product);
            return result;
        }
        public async Task DeleteProductWithDetails (long productId)
        {
            using (var unitOfWork = UnitOfWorkManager.Begin())
            {
                var product = await _productRepository.GetAllIncluding(product => product.Images, product => product.ProductDetails)
                   .Where(p => p.Id == productId && p.TenantId == (int)AbpSession.TenantId)
                   .FirstOrDefaultAsync();
                if (product == null)
                {
                    throw new UserFriendlyException("Product not found");
                }
                if (product.Images != null && product.Images.Any())
                {
                    foreach (var image in product.Images)
                    {

                        await _imageRepository.DeleteAsync(image);
                    }

                }   
                if(product.ProductDescription !=null && product.ProductDetails.Any())
                {
                    foreach (var detail in product.ProductDetails)
                    {
                        await _productDetailRepository.DeleteAsync(detail);
                    }
                }
                await _productRepository.DeleteAsync(product);
                await unitOfWork.CompleteAsync();
            }
        }

        public async Task<ProductWithDetailDto> UpdateProductAsync(ProductWithDetailDto input)
        {
            var product = await _productRepository.GetAllIncluding(p => p.Images, p => p.ProductDetails)
                                                  .FirstOrDefaultAsync(p => p.Id == input.Id && p.TenantId == 
                                                  (int)AbpSession.TenantId);

            if (product == null)
            {
                throw new UserFriendlyException("Product not found");
            }

            product.ProductName = input.ProductName;
            product.ProductDescription = input.ProductDescription;
            product.ProductThumbnail = input.ProductThumbnail;

            if (input.Images != null && input.Images.Any())
            {
                product.Images.Clear();
                foreach (var imageUrl in input.Images)
                {
                    product.Images.Add(new Image
                    {
                        ImageUrl = imageUrl,
                        ProductId = product.Id
                    });
                }
            }
            var existingProductDetail = product.ProductDetails.FirstOrDefault(); 


            if (existingProductDetail != null)
            {
                existingProductDetail.Status = input.Status;
                existingProductDetail.Quantity = input.Quantity;
                existingProductDetail.BarCode = input.BarCode;
                existingProductDetail.Price = input.Price;
                existingProductDetail.CategoryId = input.CategoryId;
            }            

            await _productRepository.UpdateAsync(product);

            var result = ObjectMapper.Map<ProductWithDetailDto>(product);
            return result;
        }
        public async Task AddOrUpdateRatings(ProductReviewsDto input)
        {
            var existingReview = await _productReviewRepository.FirstOrDefaultAsync(r =>
                r.UserId == input.UserId && r.ProductId == input.ProductId);

            if (existingReview != null)
            {
                existingReview.Rating = input.Rating;
                existingReview.Review = input.Review;

                await _productReviewRepository.UpdateAsync(existingReview);
            }
            else
            {
                // Insert a new review
                    var userData = _userManager.GetUserById((long)AbpSession.UserId);

                var data = new ProductReview
                {
                    UserId = input.UserId,
                    ProductId = input.ProductId,
                    Rating = input.Rating,
                    Review = input.Review,
                    UserName= userData.UserName,
                };

                await _productReviewRepository.InsertAsync(data);
            }
        }

        public async Task<List<ProductReviewsDto>> GetReviewsByProductIdAsync(long productId)
        {
            var reviews = await _productReviewRepository.GetAll()
                            .Where(review => review.ProductId == productId)
                            .ToListAsync();
         
            var result = ObjectMapper.Map<List<ProductReviewsDto>>(reviews);

            return result;
        }



    }
}
