namespace AssetTracker.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int CategoryId { get; set; }
        public decimal Value { get; set; }
    }
}
