using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class CategorySearchVM
    {
        public List<Category> CategoryList { get; set; }
        //Search
        public string SearchTerms { get; set; }
        //Pagination
        public int? pageNo { get; set; }
        public double? TotalPages { get; set; }
    }
}