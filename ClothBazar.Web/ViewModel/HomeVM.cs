using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class HomeVM
    {
        public List<Category> FeatureCategoryList { get; set; }
        public List<Product> FeatureProductList { get; set; }
    }
}