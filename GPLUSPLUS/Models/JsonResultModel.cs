using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Models
{
    public class JsonResultModel
    {
        public string Key { get; set; }
        public string Text { get; set; }

    }
    public static class AndroidJson
    {
        public static string GenerateJsonResponse(string AKey, string AText)
        {
            dynamic collectionWrapper = new { Root = new List<JsonResultModel> { new JsonResultModel { Key = AKey, Text = AText } } };
            return Newtonsoft.Json.JsonConvert.SerializeObject(collectionWrapper);
        }
    }

    public class AndroidProductDetailModel
    {
        public AndroidProductDetailModel()
        {
            ProductImges = new List<string>();
        }
        public List<string> ProductImges { get; set; }
        public string ProductName { get; set; }
        public string ProductBrand { get; set; }
        public string ProductModelName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductScoreCost { get; set; }


    }
    public static class Tools
    {
        public static string GeneratePassWord(int count = 12)
        {
            string RandomValueString;
            Random rnd = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            RandomValueString = new string(Enumerable.Repeat(chars, count)
             .Select(s => s[rnd.Next(s.Length)]).ToArray());
            return RandomValueString;
        }
    }
}