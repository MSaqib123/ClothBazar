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
        //____________________ SingleTone _________________________________
        //humain barr object create nhin krna paraa gaa
        //bulkaa hum Object create krna ke zimadarum  --> single ton ko da dain gaa
        #region SingleTone
        public static  ConfigurationService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationService();
                return instance;
            }
        }

        private static ConfigurationService instance { get; set; }
        private ConfigurationService(){}
        #endregion


        public Conf GetConfig(string Key)
        {
            using (var context = new CBContext())
            {
                return context.configuraton.Where(x=>x.Name == Key).FirstOrDefault();
            }
        }

        
        public int PageSize()
        {
            using (var context = new CBContext())
            {
                var pageSizeConfig = context.configuraton.Where(x=>x.Name == "PageSize").FirstOrDefault();
                return pageSizeConfig.Value  != null ? int.Parse(pageSizeConfig.Value) : 10;
            }
        }

        

        
    }
}
