using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Models
{
    public class ProductsModelNew
    {
        public ProductsModelNew()
        {
            HighProducts = new Collection<ProductsNew>();
            LowProducts = new Collection<ProductsNew>();
        }
        public Collection<ProductsNew> HighProducts { get; set; }
        public Collection<ProductsNew> LowProducts { get; set; }
    }

    public class ProductsNew
    {
            
          public ProductsNew()
        {
            ImageThumb = new List<byte[]>();
        }
        public int ProductId { get; set; }
        public List<byte[]> ImageThumb { get; set; }
    }
}