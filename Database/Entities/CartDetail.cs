using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System.Text.Json.Serialization;

namespace Database.Entities
{
    public class CartDetail
    {
        public int Id { get; set; }
        public double SubTotal { get; set; }
        public int ProductId { get; set; } // FK
        public int ShoppingCartId { get; set; } // FK
        public int Quantity { get; set; }

        //Relationships
        [JsonIgnore]
        public ShoppingCart? ShoppingCart { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
