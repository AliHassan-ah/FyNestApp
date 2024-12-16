using AutoMapper;
using CrudAppProject.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Products
{
    public class ProductMapProfile: Profile
    {
        public ProductMapProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto,Product>();
            CreateMap<Product, ProductWithDetailDto>()
                          .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ProductDetails.FirstOrDefault().Status))
                          .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ProductDetails.FirstOrDefault().Quantity))
                          .ForMember(dest => dest.BarCode, opt => opt.MapFrom(src => src.ProductDetails.FirstOrDefault().BarCode))
                          .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductDetails.FirstOrDefault().Price))
                          .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.ProductDetails.FirstOrDefault().CategoryId))
                        .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(img => img.ImageUrl).ToList()));

            CreateMap<ProductWithDetailDto, Product>();
        }
    }
}
