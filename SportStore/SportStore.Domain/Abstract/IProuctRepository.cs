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
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
    }
}
