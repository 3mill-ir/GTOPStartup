using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Areas.eshop.Models
{
    public class ProductTypes
    {
        public ProductTypes() {
            Types = new Collection<ProductTypesList>();
        }
        public Collection<ProductTypesList> Types { get; set; }
        public int Types_Normal { get; set; }
        public int Types_Boronz { get; set; }
        public int Types_Silver { get; set; }
        public int Types_Gold { get; set; }

    }
    public class ProductTypesList
    {
        public int ProductTypesId { get; set; }
        public string ProductTypesName { get; set; }
        public int ProductTypesCount { get; set; }
        public int ProductTypesCount_Normal { get; set; }
        public int ProductTypesCount_Boronz { get; set; }
        public int ProductTypesCount_Silver { get; set; }
        public int ProductTypesCount_Gold { get; set; }
    }
}