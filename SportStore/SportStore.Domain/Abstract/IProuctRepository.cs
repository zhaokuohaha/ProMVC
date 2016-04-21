using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Domain.Entities;

namespace SportStore.Domain.Abstract
{
    /// <summary>
    /// 产品集合接口, 其中定义了一个产品集合
    /// </summary>
    public interface IProuctRepository
    {
        /// <summary>
        /// 返回产品的集合
        /// </summary>
        IQueryable<Product> Products { get; }
        /// <summary>
        /// 保存产品, Savechange() , 其中有 Add 和 Update 的代码
        /// </summary>
        /// <param name="product"></param>
        void SaveProduct(Product product);
        /// <summary>
        /// 删除一个商品
        /// </summary>
        Product DeleteProduct(int productId);
    }
}
