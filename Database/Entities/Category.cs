using System.Text.Json.Serialization;

namespace Database.Entities
{
    public class Category
    {
        public int Id { get; set; }    // Primary Key
        public string CategoryName { get; set; }

        //Relationships
        [JsonIgnore]
        public List<Product>? Products { get; set; }
    }
}
