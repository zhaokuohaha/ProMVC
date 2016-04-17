using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totoalParam);
    }

    public class DefaultDiscountHelper : IDiscountHelper
    {
        /// <summary>
        /// 打折 固定10%
        /// </summary>
        /// <param name="totalParam"></param>
        /// <returns></returns>
        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (10m / 100m * totalParam));
        }
    }
}