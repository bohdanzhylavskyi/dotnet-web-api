using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "SupplierID is required")]
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "CategoryID is required")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "QuantityPerUnit is required")]
        public string QuantityPerUnit { get; set; }

        [Required(ErrorMessage = "UnitPrice is required")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "UnitsInStock is required")]
        public int UnitsInStock { get; set; }

        [Required(ErrorMessage = "UnitsOnOrder is required")]
        public int UnitsOnOrder { get; set; }

        [Required(ErrorMessage = "ReorderLevel is required")]
        public int ReorderLevel { get; set; }

        [Required(ErrorMessage = "Discontinued is required")]
        public int Discontinued { get; set; }
    }
}
