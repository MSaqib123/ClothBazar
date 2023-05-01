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
    public class CategoriesService
    {
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
                return context.categories.ToList();
            }
        }
        
        //_____ Single Record ________
        public Category GetSingleRecord(int Id)
        {
            using (var context = new CBContext())
            {
                return context.categories.Find(Id);
            }
        }


        //_____ Update Record ________
        public void Update(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        //_____ Delete Record ________
        public void Delete(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }


    }
}
