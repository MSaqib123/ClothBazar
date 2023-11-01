using ClothBazar.Entities;
using ClothBazar.Entities.VM;
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

        //_____ NetAmmount _______
        public decimal NetAmount { get; set; }

        //_____ Total Product ____
        public int TotalProduct { get; set; }

        //_____ UserVM ____
        public userVM userDetail { get; set; }

    }
}