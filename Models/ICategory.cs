namespace AssetTracker.Models
{
    public interface ICategory
    {
        List<Category> GetAllCategories();
        string GetCategoryNameById(int categoryId);
    }
}