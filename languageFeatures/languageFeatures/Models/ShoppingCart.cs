using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace languageFeatures.Models
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShoppingCart : IEnumerable<Product>
    {
        public List<Product> Products { get; set; }

        public IEnumerator<Product> GetEnumerator()
        {
            return Products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}