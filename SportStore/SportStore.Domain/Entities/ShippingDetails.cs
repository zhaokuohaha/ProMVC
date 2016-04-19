using System.ComponentModel.DataAnnotations;

namespace SportStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "请输入您的名字")]
        public string Name { get; set; }
        [Required(ErrorMessage = "请输入收获地址")]
        public string Line1 { get; set; }
        [Required(ErrorMessage = "请输入城市")]
        public string City { get; set; }
        [Required(ErrorMessage = "请输入省名")]
        public string Province { get; set; }
        public bool GifWrap { get; set; }

    }
}
