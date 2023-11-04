using ClothBazar.Database;
using ClothBazar.Database.Models;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBaza.Services
{
    public class OrderUserService
    {
        //____________________ SingleTone _________________________________
        //humain barr object create nhin krna paraa gaa
        //bulkaa hum Object create krna ke zimadarum  --> single ton ko da dain gaa
        #region SingleTone
        public static OrderUserService Instance
        {
            get
            {
                if (instance == null) instance = new OrderUserService();
                return instance;
            }
        }

        private static OrderUserService instance { get; set; }
        private OrderUserService() { }
        #endregion

        //____ create ______
        public void Save(OrderUserInfo model)
        {
            using (var context = new ApplicationDbContext())
            {
                context.orderUsers.Add(model);
                context.SaveChanges();
            }
        }

        //_____ List ________
        public List<OrderUserInfo> GetList()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.orderUsers.ToList();//Include(x=>x.Products).ToList();
            }
        }


        //_____ Single Record ________
        //public Category GetSingleRecord(int Id)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        return context.categories.Include(x=>x.Products).Where(x=>x.Id == Id).FirstOrDefault();
        //    }
        //}


        ////_____ Update Record ________
        //public void Update(Category model)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        context.Entry(model).State = EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //}

        ////_____ Delete Record ________
        //public void Delete(Category model)
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        context.Entry(model).State = EntityState.Deleted;
        //        context.SaveChanges();
        //    }
        //}



    }
}
