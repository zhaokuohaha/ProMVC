using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class ShoppingCart
    {
        private IValueCalculator calc;
        public ShoppingCart(IValueCalculator calc)
        {
            this.calc = calc;
        }
        public IEnumerable<Product> Products { get; set; }
        public decimal CalulaterProductTotal()
        {
            return calc.ValueProducts(Products);
        }
    }
}