using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModel
{
    public class CheckoutVM
    {
        //_____ Id List ________ for Quentities
        //public List<int> CartProductsID { get; set; }

        //_____ Product List _______
        public List<ProductCheckOoutVM> CartProducts { get; set; }
    }
}