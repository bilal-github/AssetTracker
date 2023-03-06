using AssetTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory _iCategory;

        public CategoriesController(ICategory icategory)
        {
            _iCategory = icategory;
        }

        [HttpGet]
        public JsonResult Get()
        {
            List<Category> categories = _iCategory.GetAllCategories();
            return new JsonResult(categories);
        }
    }
}
