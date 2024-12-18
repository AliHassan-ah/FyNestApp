using Abp.Domain.Repositories;
using Abp.UI;
using CrudAppProject.Images;
using CrudAppProject.ProductDetails;
using CrudAppProject.Products.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Products
{
    public class ProductAppService : CrudAppProjectAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product,long> _productRepository;
        private readonly IRepository<ProductDetail, long> _productDetailRepository;
        private readonly IRepository <Image,long> _imageRepository;

        public ProductAppService(IRepository<Product, long> productRepository,
                                 IRepository<ProductDetail, long> productDetailRepository,
                                 IRepository<Image, long> imageRepository   
            )
        {
            _productRepository = productRepository;
            _productDetailRepository = productDetailRepository;
            _imageRepository = imageRepository;
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
            var result = ObjectMapper.Map<List<ProductWithDetailDto>>(pagedProducts);
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

    }
}
