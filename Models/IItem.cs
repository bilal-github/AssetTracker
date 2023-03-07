namespace AssetTracker.Models
{
    public interface IItem
    {
        Task<bool> CheckIfItemExists(Item item);
        int DeleteItem(int id);
        Task<List<Item>> GetAllItems();
        Task<string> InsertItem(Item item);
    }
}