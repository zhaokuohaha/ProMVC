using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportStore.Domain.Entities;
namespace SportStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}