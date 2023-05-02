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
    public class ProductsService
    {
        //____ create ______
        public void Save(Product model)
        {
            using (var context = new CBContext())
            {
                context.Products.Add(model);
                context.SaveChanges();
            }
        }

        //_____ List ________
        public List<Product> GetList()
        {
            //______ if add Vertual ______
            //var context = new CBContext();
            //return context.Products.ToList();

            //______ with Using  block ---> add Include() _____________
            using (var context = new CBContext())
            {
                return context.Products.Include(x=>x.Category).ToList();
            }
        }

        //_____ Single Record ________
        public Product GetSingleRecord(int Id)
        {
            using (var context = new CBContext())
            {
                return context.Products.Find(Id);
            }
        }


        //_____ Update Record ________
        public void Update(Product model)
        {
            using (var context = new CBContext())
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        //_____ Delete Record ________
        public void Delete(Product model)
        {
            using (var context = new CBContext())
            {
                context.Entry(model).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        //_____ Delete Record ________
        public List<Product> SearchRecords(string search)
        {
            using (var context = new CBContext())
            {
                //if any product Name is null in database then will give error
                //var list = context.Products.Where(x => x.Name.Contains(search)).ToList();
                var list = context.Products.Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower())).ToList();
                return list;
            }
        }


    }
}
