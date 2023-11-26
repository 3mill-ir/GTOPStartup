using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPLUSPLUS.Models.Bussiness
{
    public class ShopManagement
    {
        public static List<SelectListItem> LoadShopType()
        {

            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "انتخاب کنید", Value = "انتخاب کنید" });
            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                var query = from c in EC_DB.ShopTypes select c.Name;

                foreach (var q in query)
                {
                    li.Add(new SelectListItem { Text = q, Value = q });
                }
            }
            return li;
        }
    }

}