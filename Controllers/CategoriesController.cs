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
        public async Task<JsonResult> Get()
        {
            List<Category> categories = await _iCategory.GetAllCategories();
            return new JsonResult(categories);
        }
    }
}
