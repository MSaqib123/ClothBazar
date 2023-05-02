using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public List<Category> categoryList { get; set; }
    }
}