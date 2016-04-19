using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Domain.Entities
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// 清单列表
        /// </summary>
        private List<CartLine> lineCollection = new List<CartLine>();
        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="product">商品</param>
        /// <param name="quantity">数量</param>
        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if(line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// 从购物车中移除
        /// </summary>
        /// <param name="product">商品</param>
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        /// <summary>
        /// 购物车结算
        /// </summary>
        /// <returns></returns>
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        public void Clear()
        {
            lineCollection.Clear();
        }

        /// <summary>
        /// 购物车清单
        /// </summary>
        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
    }

    /// <summary>
    /// 购物车清单的每一行, 表示一种商品
    /// </summary>
    public class CartLine
    {
        /// <summary>
        /// 商品
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Quantity { get; set; }
    }
}
