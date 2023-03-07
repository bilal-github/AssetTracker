using AssetTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IItem _iItem;
        private ICategory _iCategory;

        public ItemsController(IItem iitem, ICategory icategory)
        {
            _iItem = iitem;
            _iCategory = icategory;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            List<Item> items = await _iItem.GetAllItems();
            Dictionary<string, List<Item>> sortedItemsByCategory = new Dictionary<string, List<Item>>();

            foreach (Item item in items)
            {
                string categoryName = await _iCategory.GetCategoryNameById(item.CategoryId);

                if (!sortedItemsByCategory.ContainsKey(categoryName))
                {
                    sortedItemsByCategory[categoryName] = new List<Item>();
                }

                sortedItemsByCategory[categoryName].Add(item);
            }

            foreach (var category in sortedItemsByCategory)
            {
                category.Value.Sort((x, y) => x.ItemName.CompareTo(y.ItemName));
            }

            sortedItemsByCategory = sortedItemsByCategory.OrderBy(category => category.Key).ToDictionary(category => category.Key, category => category.Value);

            return new JsonResult(sortedItemsByCategory);
        }

        [HttpPost]
        public async Task<JsonResult> Post(Item item)
        {
            if(await _iItem.CheckIfItemExists(item))
            {
                return new JsonResult( new { Error = "Item already exists" });
            }
            return new JsonResult(await _iItem.InsertItem(item));

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            int rowsAffected = _iItem.DeleteItem(id);
            if (rowsAffected > 0)
            {
                return new JsonResult("Item Deleted Successfully");
            }
            return new JsonResult("Item was not Deleted");


        }
    }
}
