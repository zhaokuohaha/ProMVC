using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{   
    /// <summary>
    /// 计算产品价格
    /// </summary>
    public class LinqValueCalculater:IValueCalculator
    {
        private IDiscountHelper discounter;

        public LinqValueCalculater(IDiscountHelper discounter) { this.discounter = discounter; }
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}