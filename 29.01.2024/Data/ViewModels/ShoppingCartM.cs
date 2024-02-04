using SocksWebsite.Data.Cart;

namespace SocksWebsite.Data.ViewModels

{
    public class ShoppingCartM
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public ShoppingCart ShoppingCart { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public double ShoppingCartTotal { get; set; }
    }
}
