using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class HomeVM
    {
        public List<Category> CategoryList { get; set; }
        public List<Product> ProductList { get; set; }
    }
}