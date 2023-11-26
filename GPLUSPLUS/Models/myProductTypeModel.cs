using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Models
{
    public class myProductTypeModel
    {
    }
    public class ProductTypeCustom
    {
        public int PT_ID { get; set; }


        public string PT_Name { get; set; }
        public string PT_DescriptionOf { get; set; }
        public string ImageThumb { get; set; }



        public HttpPostedFileBase PT_ImageOf { get; set; }
    }
}