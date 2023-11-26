using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPLUSPLUS.Models
{
    public class myProductModel
    {
        public int P_ID { get; set; }
        public string P_Category { get; set; }
        public string P_Brand { get; set; }
        [AllowHtml]
        public string P_Description { get; set; }
        public Nullable<int> P_ScoreCost { get; set; }
        public string P_ModelName { get; set; }
        public Nullable<System.DateTime> P_DateAdded { get; set; }
        public Nullable<int> P_Count { get; set; }
        public string P_Name { get; set; }

 

        public HttpPostedFileBase PI_Image { get; set; }


 
        public string PS_ModelName { get; set; }
        public Nullable<System.DateTime> PS_DateAdded { get; set; }



    }

    public class ListModel<T>
    {
        public List<T> Items { get; set; }

        public ListModel(List<T> list)
        {
            Items = list;
        }
    }


    public class ProductCustom {
        public int P_ID { get; set; }

        [Required]
        public string P_Category { get; set; }
        [Required]
        public string P_Brand { get; set; }
        [Required]
        [AllowHtml]
        public string P_Description { get; set; }

         [Required]
        public Nullable<int> P_Cost { get; set; }
         [Required]
         public string P_ModelName { get; set; }
          [Required]
         public string P_Name { get; set; }

       
        public Nullable<int> P_ScoreCost { get; set; }
     
        public Nullable<System.DateTime> P_DateAdded { get; set; }
       
        public Nullable<int> P_Count { get; set; }

        public Nullable<bool> P_Status { get; set; }
    }

    public class ProductCustomDetails
    {
       public ProductCustomDetails() {
       ImageThumb=new List<string>();
       ImageThumbID = new List<int>();
       }
        public int P_ID { get; set; }
        public string P_Category { get; set; }
        public string P_Brand { get; set; }
        [AllowHtml]
        public string P_Description { get; set; }
        public Nullable<int> P_ScoreCost { get; set; }
        public Nullable<int> P_Cost { get; set; }
        public string P_ModelName { get; set; }
        public string P_Name { get; set; }
        public Nullable<System.DateTime> P_DateAdded { get; set; }
        public Nullable<int> P_Count { get; set; }
        public Nullable<bool> P_Status { get; set; }
        public List<string> ImageThumb { get; set; }
        public List<int> ImageThumbID { get; set; }
       
        [Required]
        public HttpPostedFileBase PI_Image { get; set; }

    }


    public class ProductBuy
    {
        public ProductBuy()
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