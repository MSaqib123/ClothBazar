﻿using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBaza.Services
{
    public class CategoriesService
    {
        //____________________ SingleTone _________________________________
        //humain barr object create nhin krna paraa gaa
        //bulkaa hum Object create krna ke zimadarum  --> single ton ko da dain gaa
        #region SingleTone
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();
                return instance;
            }
        }

        private static CategoriesService instance { get; set; }
        private CategoriesService() { }
        #endregion

        //____ create ______
        public void Save(Category model)
        {
            using (var context = new CBContext())
            {
                context.categories.Add(model);
                context.SaveChanges();
            }
        }

        //_____ List ________
        public List<Category> GetList()
        {
            using (var context = new CBContext())
            {
                return context.categories.Include(x=>x.Products).ToList();
            }
        }
       
        public List<Category> GetList(string search,int pageNo)
        {
            int pageSize = Convert.ToInt32(ConfigurationService.Instance.GetConfig("paginationSize").Value);
            using (var context = new CBContext())
            {
                if (search != null && search != "")
                {
                    var list = context.categories.Where(x => x.Name != null && x.Name.ToLower()
                   .Contains(search.ToLower()))
                   .OrderBy(x => x.Id)
                   .Skip((pageNo - 1) * pageSize)
                   .Take(pageSize)
                   .Include(x => x.Products)
                   .ToList();

                    return list;
                }
                else
                {
                    var list = context.categories
                   .OrderBy(x => x.Id)
                   .Skip((pageNo - 1) * pageSize)
                   .Take(pageSize)
                   .Include(x => x.Products)
                   .ToList();
                    return list;
                }
                
            }
        }

        //_____ Feature List ________
        public List<Category> GetFeaturedList()
        {
            using (var context = new CBContext())
            {
                return context.categories.Include(x => x.Products).Where(x=>x.isFeatured).ToList();
            }
        }
        
        //_____ Single Record ________
        public Category GetSingleRecord(int Id)
        {
            using (var context = new CBContext())
            {
                return context.categories.Include(x=>x.Products).Where(x=>x.Id == Id).FirstOrDefault();
            }
        }


        //_____ Update Record ________
        public void Update(Category model)
        {
            using (var context = new CBContext())
            {
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        //_____ Delete Record ________
        public void Delete(Category model)
        {
            using (var context = new CBContext())
            {
                context.Entry(model).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        //_____ Search Record ________
        public List<Category> SearchRecords(string search)
        {
            using (var context = new CBContext())
            {
                //if any product Name is null in database then will give error
                //var list = context.Products.Where(x => x.Name.Contains(search)).ToList();
                var list = context.categories
                    .Include(x => x.Products)
                    .Where(x => x.Name != null && x.Name.ToLower()
                    .Contains(search.ToLower()))
                    .ToList();
                return list;
            }
        }
         //_____ ListCount ________
        public int GetListCount(string search)
        {
            using (var context = new CBContext())
            {
                if (search != null && search!="")
                {
                    var list = context.categories
                   .Include(x => x.Products)
                   .Where(x => x.Name != null && x.Name.ToLower()
                   .Contains(search.ToLower()))
                   .ToList().Count;
                    return list;
                }
                else
                {
                    return context.categories.Include(x => x.Products).ToList().Count;
                }
                
            }
        }


    }
}
