using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AssetTracker.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public int CategoryId { get; set; }
        public decimal Value { get; set; }

        [ValidateNever]
        public virtual Category Category { get; set; } = null!;
    }
}
