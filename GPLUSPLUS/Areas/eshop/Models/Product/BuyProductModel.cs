using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPLUSPLUS.Areas.eshop.Models.Product
{
    public class BuyProductModel
    {
        public BuyProductModel()
        {
       ImageThumb=new List<string>();
       ImageThumbID = new List<int>();
       }
        public int P_ID { get; set; }
        public string P_Category { get; set; }
        public string P_Brand { get; set; }
        [AllowHtml]
        public string P_Description { get; set; }
        public Nullable<int> P_ScoreCost { get; set; }
        public string P_ModelName { get; set; }
        public string P_Name { get; set; }
        public List<string> ImageThumb { get; set; }
        public List<int> ImageThumbID { get; set; }


        public string U_FullName { get; set; }
        public string U_Tell { get; set; }
        public string U_FullAddress { get; set; }
        public string U_PostalCode { get; set; }


        public bool HaveNewAddress { get; set; }
        public string PU_NewAddress { get; set; }
        public string PU_NewState { get; set; }
        public string PU_NewCity { get; set; }
        public string PU_NewPostalCode { get; set; }
    }
}