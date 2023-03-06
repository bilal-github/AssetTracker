namespace AssetTracker.Models
{
    public interface IItem
    {
        int DeleteItem(int id);
        List<Item> GetAllItems();
        string InsertItem(Item item);
    }
}