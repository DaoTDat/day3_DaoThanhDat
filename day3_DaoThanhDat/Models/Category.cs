using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace day3_DaoThanhDat.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
