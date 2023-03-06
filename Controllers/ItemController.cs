using AssetTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private IItem _iItem;
        private ICategory _iCategory;

        public ItemController(IItem iitem, ICategory icategory)
        {
            _iItem = iitem;
            _iCategory = icategory;
        }

        [HttpGet]
        public JsonResult Get()
        {
            List<Item> items = _iItem.GetAllItems();
            Dictionary<string, List<Item>> sortedItemsByCategory = new Dictionary<string, List<Item>>();

            foreach (Item item in items)
            {
                string categoryName = _iCategory.GetCategoryNameById(item.CategoryId);

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
    }
}
