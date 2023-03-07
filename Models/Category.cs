﻿namespace AssetTracker.Models
{
    public class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }
    }
}
