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

        //_____ Feature List ________
        public List<Product> GetFeaturedList()
        {
            using (var context = new CBContext())
            {
                return context.Products.Include(x => x.Category).Where(x => x.isFeatured).ToList();
            }
        }



        //_____ Single Record ________
        public Product GetSingleRecord(int Id)
        {
            using (var context = new CBContext())
            {
                //return context.Products.Where(x => x.Id == Id).Include(x=>x.Category).FirstOrDefault();
                return context.Products.Find(Id); //on the base  primary key automaticlay find  category as well as
            }
        }

        //_____ GetProductList base on  Ids List ________ Add to Cart
        public List<Product> GetListRecordbyListIds(List<int> Id)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x => Id.Contains(x.Id)).Include(x=>x.Category).ToList();
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

        //_____ Search Record ________
        public List<Product> SearchRecords(string search)
        {
            using (var context = new CBContext())
            {
                //if any product Name is null in database then will give error
                //var list = context.Products.Where(x => x.Name.Contains(search)).ToList();
                var list = context.Products.Include(x => x.Category).Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower())).ToList();
                return list;
            }
        }


    }
}
