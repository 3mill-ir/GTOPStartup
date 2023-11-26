using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Models
{
    public class myShopTypeModel
    {
    }
    public class ShopTypeCustom
    {
        public int ST_ID { get; set; }

     
        public string ST_Name { get; set; }
        public string ST_DescriptionOf { get; set; }
        public string ImageThumb { get; set; }


 
        public HttpPostedFileBase ST_ImageOf { get; set; }
    }

    public class ShopTypeCustomDetails
    {
      
        public int ST_ID { get; set; }
        public string ST_Name { get; set; }
        public string ST_DescriptionOf { get; set; }
        public string ImageThumb { get; set; }
  

        [Required]
        public HttpPostedFileBase ST_ImageOf { get; set; }

    }
}