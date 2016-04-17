using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    /// <summary>
    /// 此例的目标是让MinimumdiscountHelper演示以下行为:
    /// 总额大于100时,折扣为10%
    /// 总额介于10和100之间时,折扣为5%
    /// 总额小于10时,不打折
    /// 总额为负数时,抛出异常
    /// </summary>
    public class MinumumDiscountHelper:IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            if (totalParam < 0) { throw new ArgumentOutOfRangeException(); }
            else if (totalParam > 100) { return totalParam * 0.9m; }
            else if (totalParam >= 10 && totalParam <= 100) { return totalParam - 5; }
            else { return totalParam; }
        }
    }
}