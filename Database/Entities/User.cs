using Database.Entities;
using System.Text.Json.Serialization;

namespace Database.Entities
{
   

    public class User
    {

        public int Id { get; set; } // PK

        public string UserName { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        //Relationships
        [JsonIgnore]  // previene ciclos de referencia en la serialización JSON
        public List<ShoppingCart>? ShoppingCarts { get; set; }
        [JsonIgnore]
        public List<Order>? Orders { get; set; }

    }
}
