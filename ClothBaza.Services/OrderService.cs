using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBaza.Services
{
    public class OrderService
    {
        //____________________ SingleTone _________________________________
        //humain barr object create nhin krna paraa gaa
        //bulkaa hum Object create krna ke zimadarum  --> single ton ko da dain gaa
        #region SingleTone
        public static OrderService Instance
        {
            get
            {
                if (instance == null) instance = new OrderService();
                return instance;
            }
        }

        private static OrderService instance { get; set; }
        private OrderService() { }
        #endregion

        //____ create ______
        public void Save(Order model)
        {
            using (var context = new CBContext())
            {
                context.orders.Add(model);
                context.SaveChanges();
            }
        }

        //_____ List ________
        public List<Order> GetList()
        {
            using (var context = new CBContext())
            {
                return context.orders.ToList();//Include(x=>x.Products).ToList();
            }
        }
       
        
        //_____ Single Record ________
        //public Category GetSingleRecord(int Id)
        //{
        //    using (var context = new CBContext())
        //    {
        //        return context.categories.Include(x=>x.Products).Where(x=>x.Id == Id).FirstOrDefault();
        //    }
        //}


        ////_____ Update Record ________
        //public void Update(Category model)
        //{
        //    using (var context = new CBContext())
        //    {
        //        context.Entry(model).State = EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //}

        ////_____ Delete Record ________
        //public void Delete(Category model)
        //{
        //    using (var context = new CBContext())
        //    {
        //        context.Entry(model).State = EntityState.Deleted;
        //        context.SaveChanges();
        //    }
        //}



    }
}
