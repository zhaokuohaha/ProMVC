using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Domain.Contrete
{
    class DefaultEmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFormAddress = "sportsstore@example.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MyPassword";
        public string ServerName = "smtp.example.com";
        public int Serverport = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"\sports_store_emails";//看不懂啥意思
    }
}
