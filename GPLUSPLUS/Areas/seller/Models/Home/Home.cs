using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Areas.seller.Models.Home
{
    public class HomeShops
    {
        public HomeShops()
        {
            //ProductTypes = new Collection<ProductTypesList>();
            RandomShops = new Collection<ShopsToDisplay>();
            NewShops = new Collection<ShopsToDisplay>();
            NewSmartShops = new Collection<ShopsToDisplay>();
        }
        //public Collection<ProductTypesList> ProductTypes { get; set; }
        public Collection<ShopsToDisplay> RandomShops { get; set; }
        public Collection<ShopsToDisplay> NewShops { get; set; }
        public Collection<ShopsToDisplay> NewSmartShops { get; set; }
      
    }

    public class ShopsToDisplay {

        public int Shop_ID { get; set; }
        public string ShopName{ get; set; }
        public byte[] ImageThumb { get; set; }
        public int ImageThumbID { get; set; }
        public byte[] Image { get; set; }
    }


}