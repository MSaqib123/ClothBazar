using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class ProductSearchVM
    {
        public List<Product> ProductsList { get; set; }
        //search
        public string SearchTerms { get; set; }
        //Pagination
        public int? pageNo { get; set; }
        public double? TotalPages { get; set; }

        public Pager Pager { get; set; }
    }
}