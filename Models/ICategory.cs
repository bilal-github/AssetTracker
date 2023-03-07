namespace AssetTracker.Models
{
    public interface ICategory
    {
        Task<List<Category>> GetAllCategories();
        Task<string> GetCategoryNameById(int categoryId);
    }
}