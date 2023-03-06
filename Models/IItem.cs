namespace AssetTracker.Models
{
    public interface IItem
    {
        bool CheckIfItemExists(Item item);
        int DeleteItem(int id);
        List<Item> GetAllItems();
        string InsertItem(Item item);
    }
}