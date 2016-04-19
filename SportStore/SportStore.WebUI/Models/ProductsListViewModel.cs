using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportStore.Domain.Entities;
namespace SportStore.WebUI.Models
{
    /// <summary>
    /// 分页视图模型数据类, 用于给视图传递分页信息
    /// </summary>
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo paginginfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}