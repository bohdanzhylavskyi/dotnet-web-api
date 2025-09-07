using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class UpdateCategoryDTO
    {
        [Required(ErrorMessage = "Category name is required")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
