using System.Net.Mail;
using System.Text;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using System.Net;

namespace SportStore.Domain.Contrete
{
    /// <summary>
    /// 处理订单 --- 给管理员发送订单邮件 --- 实现IOrderProcesser接口(自定义)
    /// </summary>
    public class EmailOrderProcessor : IOrderProcesser
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings emailsettings)
        {
            this.emailSettings = emailsettings;
        }

        /// <summary>
        /// 实现接口   处理函数--- 发送邮件
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="shippongInfo"></param>
        public void ProcessOrder(Cart cart, ShippingDetails shippongInfo)
        {
            using(var smtpClient = new SmtpClient())
            {
                //配置邮件发送服务
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.Serverport;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new Order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items: ");

                foreach(var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0}x{1} (subtotal: {2:c})", line.Quantity, line.Product.Name, subtotal);
                }

                body.AppendFormat("Total order value : {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippongInfo.Name)
                    .AppendLine(shippongInfo.Line1)
                    .AppendLine(shippongInfo.City)
                    .AppendLine(shippongInfo.Province)
                    .AppendLine("---")
                    .AppendFormat("GiftWrap: {0}", shippongInfo.GifWrap ? "Yes" : "No");

                MailMessage mailMassage = new MailMessage(
                    emailSettings.MailFormAddress,  //Form
                    emailSettings.MailToAddress,    //To
                    "New Order Submitted",          //Subject
                    body.ToString()                 //Body
                );

                if (emailSettings.WriteAsFile)
                {
                    mailMassage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMassage);
            }
        }
    }
}
