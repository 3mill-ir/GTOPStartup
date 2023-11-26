using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Models
{
    public class AndroidMainJsonModel
    {
        public AndroidMainJsonModel()
        {
            Slider = new List<SliderJsonModel>();
            PishnehadHa = new List<ProductsJsonModel>();
        }
        public string TotalGhoreKeshiScore { get; set; }
        public string TotalRialiScore { get; set; }
        public List<SliderJsonModel> Slider { get; set; }
        public List<ProductsJsonModel> PishnehadHa { get; set; }

    }
    public class SliderJsonModel
    {
        public string ImgUrl { get; set; }
        public string Title { get; set; }
    }
    public class ProductsJsonModel
    {
        public string ProductImg { get; set; }
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public string ProductPrice { get; set; }
    }
}