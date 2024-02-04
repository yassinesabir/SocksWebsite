using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SocksWebsite.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public string FullName { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
    }
}