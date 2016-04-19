using SportStore.Domain.Entities;

namespace SportStore.Domain.Abstract
{
    /// <summary>
    /// 订单处理接口
    /// </summary>
    public interface IOrderProcesser
    {
        void ProcessOrder(Cart cart, ShippingDetails shippongDetails);
    }
}
