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
            int pageSize = ConfigurationService.Instance.PageSize();
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

        //_______________________ Shoping Product ____________________
        public int GetMaximumPrice()
        {
            //Get ListProducts Based on Cateogyr
            using (var context = new CBContext())
            {
                return (int)context.Products.Max(x=>x.Price);
            }
        }
        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId,int? sortyBy , int? pageNo , int pageSize)
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
                if (sortyBy.HasValue)
                {
                    switch (sortyBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                    }
                }
                return products.Skip((pageNo.Value-1)*pageSize).Take(pageSize).ToList();
            }
        }

        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId,int? sortyBy)
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
                if (sortyBy.HasValue)
                {
                    switch (sortyBy.Value)
                    {
                        case 2:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                    }
                }
                return products.Count();
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
                var list = context.Products.Include(x => x.Category)
                    .Where(x => x.Name != null && x.Name.ToLower().Contains(search.ToLower()))
                    .ToList();
                return list;
            }
        }

        //____________________ ProductCheckOutRecord Return ________________________
        public List<ProductCheckOoutVM> ProductCheckOutRecord(List<int> IDs)
        {
            //_________ create services __________
            //service method jo  --> List Ids  laa ker ---> List Product return kraa
            var List = GetListRecordbyListIds(IDs);

            //____________ Adding TotalQuentity  , TotalPrice with VM
            List<ProductCheckOoutVM> productVM = new List<ProductCheckOoutVM>();
            ProductCheckOoutVM pro = new ProductCheckOoutVM();
            foreach (var item in List)
            {
                pro.Id = item.Id;
                pro.Name = item.Name;
                pro.Description = item.Description;
                pro.ImageURL = item.ImageURL;
                pro.Price = item.Price;
                pro.Qty = IDs.Count(id => id == item.Id);
                pro.TotalPrice = IDs.Count(id => id == item.Id) * item.Price; // Calculate total price for the product

                productVM.Add(pro);
            }
            return productVM;
        }

    }
}
