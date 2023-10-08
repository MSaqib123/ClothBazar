using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class ShopVM
    {
        //public int MinimumPrice { get; set; }          Home Work
        public int MaximumPrice { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategory { get; set; }


        //Pagination
        public int? pageNo { get; set; }
        public double? TotalPages { get; set; }

        public Pager Pager { get; set; }

        //Sorting 
        public int? SortBy { get; set; }
    }

    public class ShopProductFilterVM
    {
        public List<Product> Products { get; set; }
    }
}