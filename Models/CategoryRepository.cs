using System.Data;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Models
{
    public class CategoryRepository : ICategory
    {
        private readonly  AssetDBContext _assetDBContext;

        public CategoryRepository(AssetDBContext assetDBContext)
        {
            _assetDBContext = assetDBContext;
        }

        public async Task<List<Category>> GetAllCategories()
        {

           return await _assetDBContext.Categories.ToListAsync();

        }

        public async Task<string> GetCategoryNameById(int categoryId)
        {
            return await _assetDBContext.Categories
                .Where(category => category.CategoryId.Equals(categoryId))
                .Select(category => category.CategoryName)
                .FirstAsync();
                
        }
    }
}
