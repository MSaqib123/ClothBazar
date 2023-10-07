using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class ProductsWidgetVM
    {       
        public List<Product> ProductsList{ get; set; }
        public int widgetType { get; set; }
    }
}