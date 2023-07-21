using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace microsMVCNET7.Models
{
    public class Report
    {
        public int Id { get; set; }
        public Category? Category { get; set; }
        public CategoryValue? CategoryValue { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate{ get; set; }
        public IdentityUser? Author{ get; set; }
    }
}
