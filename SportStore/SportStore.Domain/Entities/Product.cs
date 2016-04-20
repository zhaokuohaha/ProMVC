using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.Domain.Entities
{
    public class Product
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name ="编号")]
        public int ProductID { get; set; }


        [Display(Name = "名称")]
        [Required(ErrorMessage ="Please enter a product name")]
        public string Name { get; set; }


        [Display(Name = "简介")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a product description")]
        public string Description { get; set; }


        [Display(Name = "价格")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please entera positive price")]
        public decimal Price { get; set; }


        [Display(Name = "分类")]
        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }
    }
}