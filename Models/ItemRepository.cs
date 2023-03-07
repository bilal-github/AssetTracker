using System.Data;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Models
{
    public class ItemRepository : IItem
    {
        private readonly AssetDBContext _assetDBContext;

        public ItemRepository(AssetDBContext assetDBContext)
        {
            _assetDBContext = assetDBContext;
        }

        public async Task<List<Item>> GetAllItems()
        {

            return await _assetDBContext.Items.ToListAsync();

        }

        public async Task<bool> CheckIfItemExists(Item item)
        {
            _ = item ?? throw new ArgumentException("Item cannot be null");

            return await _assetDBContext.Items.Where(i => i.ItemName == item.ItemName && i.Value == item.Value).AnyAsync();

        }

        public async Task<string> InsertItem(Item item)
        {
            _ = item ?? throw new ArgumentException("Item cannot be null");

            await _assetDBContext.Items.AddAsync(item);
            if (_assetDBContext.SaveChanges() > 0)
            {
                return "Item Added Successfully";
            }
            return "Item was not added";



        }

        public int DeleteItem(int id)
        {

            Item item = new Item() { ItemId = id };
            _assetDBContext.Attach(item);
            _assetDBContext.Items.Remove(item);
            return _assetDBContext.SaveChanges();            
        }
    }
}
