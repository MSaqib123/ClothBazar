using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Database
{
    public class CBContext:DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
        public CBContext() :base ("ClothBazarConnection")
        {
        }

        public  DbSet<Product> Products { get; set; }
        public  DbSet<Category> categories{ get; set; }
        public  DbSet<Conf> configuraton{ get; set; }

        //______________ CheckOut ______________
        public DbSet<Order> orders  { get; set; }
        public DbSet<OrderUserInfo> orderUsers  { get; set; }
        public DbSet<OrderItem> orderItems  { get; set; }
    }
}
