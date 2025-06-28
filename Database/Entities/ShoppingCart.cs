using Database.Entities;
using System.Text.Json.Serialization;

namespace Database.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; } // Primary Key

        public int UserId { get; set; } // Foreign key

        public double Total { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; } = true;


        //Relationships
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }
        [JsonIgnore]
        public List<CartDetail>? CartDetails { get; set; }
    }
}
