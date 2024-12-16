using Abp.Domain.Repositories;
using CrudAppProject.Categories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CrudAppProject.Categories
{
    public class CategoryAppService : CrudAppProjectAppServiceBase, ICategoryAppService
    {
        private readonly IRepository<Category, long> _categoryRepository;

        public CategoryAppService(IRepository<Category, long> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        //crete
        public async Task<CategoryDto> CreateCategoty(CategoryDto input)
        {
            var category = new Category
            {
                TenantId = (int)AbpSession.TenantId,
                Description = input.Description,
                CategoryName = input.CategoryName,
                CategoryThumbnail = input.CategoryThumbnail
            };
            await _categoryRepository.InsertAsync(category);
            return ObjectMapper.Map<CategoryDto>(category);
        }

        //getAll
        public async Task<Abp.Application.Services.Dto.PagedResultDto<CategoryDto>> GetAllCategories(PagedCategoryResultRequestDto input)
        {
            var allCategories = await _categoryRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(input.Keyword))
            {
                allCategories = allCategories.Where(category =>
                category.CategoryName.Contains(input.Keyword) ||
                category.Description.Contains(input.Keyword)) ;
            }
            var totalCount = allCategories.Count();
            var pagedCategories = allCategories.OrderByDescending(category => category.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            var result = ObjectMapper.Map<List<CategoryDto>>(pagedCategories);
            return new Abp.Application.Services.Dto.PagedResultDto<CategoryDto>(totalCount,result);
        }
        //Delete
        public async Task DeleteCategory(long id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        //update
        public async Task<CategoryDto> updateCategory(long id , CategoryDto input) {
            var existingcategory = await _categoryRepository.GetAsync(id);
            existingcategory.Description = input.Description;
            existingcategory.CategoryName = input.CategoryName;
            existingcategory.CategoryThumbnail = input.CategoryThumbnail;
            var updateCategory = await _categoryRepository.UpdateAsync(existingcategory);
            return ObjectMapper.Map<CategoryDto>(updateCategory);

        }
        //getSingleCategory
        public async Task<CategoryDto> getSingleCategory(long id)
        {
            var category = await _categoryRepository.GetAsync(id);
            return ObjectMapper.Map<CategoryDto>(category);
        }

        public async Task<List<CategoryNameDto>> GetAllCategoryNames()
        {
            var categories = await _categoryRepository.GetAllListAsync();
            var categoryNames = ObjectMapper.Map<List<CategoryNameDto>>(categories);
            return categoryNames;
        }
    }
}
