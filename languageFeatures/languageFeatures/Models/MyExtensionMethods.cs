using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace languageFeatures.Models
{
    public static class MyExtensionMethods
    {
        //public static decimal TotalPrices(this ShoppingCart cartParam)
        //{
        //    decimal total = 0;
        //    foreach (Product pro in cartParam.Products)
        //    {
        //        total += pro.Price;
        //    }
        //    return total;
        //}
        public static decimal TotalPrices(this IEnumerable<Product> productParam)
        {
            decimal total = 0;
            foreach (Product pro in productParam)
            {
                total += pro.Price;
            }
            return total;
        }
    }
}