using SocksWebsite.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SocksWebsite.Data.ViewModels
{
    public class NewProductM
    {
        public int Id { get; set; }

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Name is required")]
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

        [Display(Name = "Product description")]
        [Required(ErrorMessage = "Description is required")]
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public string Description { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

        [Display(Name = "Qte in stock")]
        [Required(ErrorMessage = "Qte is required")]
        public double Quantity { get; set; }

        [Display(Name = "Product poster URL")]
        [Required(ErrorMessage = "Product poster URL is required")]
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public string ImageURL { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

        [Display(Name = "Select a Subcategory")]
        [Required(ErrorMessage = "Product category is required")]
        public ProductCategory ProductCategory { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Product category is required")]
        public ProductSubCategory ProductSubCategory { get; set; }


        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Available Size")]
        public ProductSize ProductSize { get; set; }
    }
}
