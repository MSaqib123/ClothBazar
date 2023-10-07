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
        //____________________ SingleTone _________________________________
        //humain barr object create nhin krna paraa gaa
        //bulkaa hum Object create krna ke zimadarum  --> single ton ko da dain gaa
        #region SingleTone
        public static ProductsService Instance
        {
            get
            {
                if (instance == null) instance = new ProductsService();
                return instance;
            }
        }

        private static ProductsService instance { get; set; }
        private ProductsService() { }
        #endregion


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
        public List<Product> GetList(int pageNo)
        {
            int pageSize = 5;
            using (var context = new CBContext())
            {
                return context.Products.OrderBy(x=>x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
                //return context.Products.Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetLatestList(int numberOfItems)
        {
            //geting number  latest Product
            using (var context = new CBContext())
            {
                return context.Products.OrderByDescending(x => x.Id).Take(numberOfItems).Include(x => x.Category).ToList();
            }
        }
        public List<Product> GetLatestList(int pageNo, int pageSize)
        {
            //geting number  latest Product based on Categories
            //pageSize == totalitem toShow
            using (var context = new CBContext())
            {
                return context.Products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
                //return context.Products.Include(x => x.Category).ToList();
            }
        }

        public List<Product> GetListByCategory(int CategoryId, int pageSize)
        {
            //Get ListProducts Based on Cateogyr
            using (var context = new CBContext())
            {
                return context.Products.Where(x=>x.Category.Id == CategoryId).OrderByDescending(x => x.Id).Take(pageSize).Include(x => x.Category).ToList();
            }
        }
        public int GetMaximumPrice()
        {
            //Get ListProducts Based on Cateogyr
            using (var context = new CBContext())
            {
                return (int)context.Products.Max(x=>x.Price);
            }
        }

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId)
        {
            using (var context = new CBContext())
            {
                var products = context.Products.ToList();
                if (categoryId.HasValue)
                {
                    products = products.Where(x => x.Category.Id == categoryId.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }
                return products;
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
                return context.Products.Include(x=>x.Category).Where(x=>x.Id == Id).FirstOrDefault(); //on the base  primary key automaticlay find  category as well as
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
