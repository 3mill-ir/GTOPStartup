using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Areas.eshop.Models.Home
{
    public class HomeProducts
    {
        public HomeProducts()
        {
            //ProductTypes = new Collection<ProductTypesList>();
            RandomProducts = new Collection<ProductsToDisplay>();
            NewProducts_Normal = new Collection<ProductsToDisplay>();
            NewProducts_Boronz = new Collection<ProductsToDisplay>();
            NewProducts_Silver = new Collection<ProductsToDisplay>();
            NewProducts_Gold = new Collection<ProductsToDisplay>();
            ScoreLowProducts = new Collection<ProductsToDisplay>();
            ScoreHighProducts = new Collection<ProductsToDisplay>();
        }
        //public Collection<ProductTypesList> ProductTypes { get; set; }
        public Collection<ProductsToDisplay> RandomProducts { get;  set; }
        public Collection<ProductsToDisplay> NewProducts_Normal { get; set; }
        public Collection<ProductsToDisplay> NewProducts_Boronz { get; set; }
        public Collection<ProductsToDisplay> NewProducts_Silver { get; set; }
        public Collection<ProductsToDisplay> NewProducts_Gold { get; set; }
        public Collection<ProductsToDisplay> ScoreLowProducts { get; set; }
        public Collection<ProductsToDisplay> ScoreHighProducts { get; set; }
      
    }

    public class ProductsToDisplay {
        public ProductsToDisplay(){
            ImageThumb = new List<byte[]>();
            ImageThumbID = new List<int>();
        }

        public int P_ID { get; set; }
        public string P_Brand { get; set; }
        public string P_ModelName { get; set; }
        public string P_Description { get; set; }
        public Nullable<int> P_ScoreCost { get; set; }
        public string FullImage { get; set; }
        public List<byte[]> ImageThumb { get; set; }
        public List<int> ImageThumbID { get; set; }
        public string ScoreClass { get; set; }
    }

    //public class ProductTypesList {
    //    public int ProductTypesId { get; set; }
    //    public string ProductTypesName { get; set; }
    //    public int ProductTypesCount{ get; set; }
    //    public int ProductTypesCount_Normal { get; set; }
    //    public int ProductTypesCount_Boronz { get; set; }
    //    public int ProductTypesCount_Silver { get; set; }
    //    public int ProductTypesCount_Gold { get; set; }
    //}
}