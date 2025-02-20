using Microsoft.AspNetCore.Identity;

namespace MVCLearning.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public IdentityUser? CreatedBy { get; set; }
        public string? CreatedById { get; set; }
    }
}
