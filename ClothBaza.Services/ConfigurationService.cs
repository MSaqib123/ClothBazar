using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBaza.Services
{
    public class ConfigurationService
    {
        public Conf GetConfig(string Key)
        {
            using (var context = new CBContext())
            {
                return context.configuraton.Where(x=>x.Name == Key).FirstOrDefault();
            }
        }
    }
}
