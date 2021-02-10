using System.Collections.Generic;

namespace WebApp.Models
{
    public class Basket
    {
        public string UserName { get; set; }
        public List<BasketItem> Items { get; set; }
            = new List<BasketItem>();
        public decimal TotalPrice { get; set; }
    }
}
