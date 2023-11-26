using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Areas.seller.Models
{
    public class ShopTypes
    {
        public ShopTypes()
        {
            Types = new Collection<ShopTypesList>();
            DetailedTypes = new Collection<ShopTypeStateBase>();
        }
        public Collection<ShopTypesList> Types { get; set; }
        public Collection<ShopTypeStateBase> DetailedTypes { get; set; }


    }
    public class ShopTypesList
    {
        public int ShopTypesId { get; set; }
        public string ShopTypesName { get; set; }
        public int ShopTypesCount { get; set; }

    }




  
    public class ShopTypeStateBase {
        public ShopTypeStateBase()
        {
            Types = new Collection<ShopTypeCityBase>();
        }
        public Collection<ShopTypeCityBase> Types { get; set; }
        public int Shop_state_Id { get; set; }
        public string Shop_state_Name { get; set; }
        public int Shop_state_Count { get; set; }
    }

    public class ShopTypeCityBase {
        public ShopTypeCityBase() {
            Types = new Collection<ShopTypesList>();
        }
        public Collection<ShopTypesList> Types { get; set; }
        public int Shop_city_Id { get; set; }
        public string Shop_city_Name { get; set; }
        public int Shop_city_Count { get; set; }

    }
}