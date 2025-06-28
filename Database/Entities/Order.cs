using Database.Entities;
using System.Text.Json.Serialization;

namespace Database.Entities
{
    public class Order
    {
        public int Id { get; set; }   //PK
        public double TotalAmount { get; set; } // Total 
        public int UserId { get; set; } // FK
        public int ShoppingCartId { get; set; } // FK

        //Relationships
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public ShoppingCart? ShoppingCart { get; set; }
    }

}
