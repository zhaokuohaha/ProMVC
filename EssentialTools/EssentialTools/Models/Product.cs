using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    /// <summary>
    /// Product模型类
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 产品名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 产品分类
        /// </summary>
        public string Category { get; set; }
    }
}