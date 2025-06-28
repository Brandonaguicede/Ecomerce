using System.Globalization;
using System.Text.Json.Serialization;

namespace Database.Entities
{
    public class Product
    {
        public int Id { get; set; } // Primary Key 
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

        //Relationships  
        [JsonIgnore]
        public Category? Category { get; set; }
        [JsonIgnore]
        public List<CartDetail>? CartDetail { get; set; }

    }
}
