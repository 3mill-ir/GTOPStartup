using GPLUSPLUS.Areas.seller.Models;
using GPLUSPLUS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.SqlServer;

namespace GPLUSPLUS.Areas.seller.Controllers
{
    public class ShopsController : Controller
    {
        //
        // GET: /seller/Shops/

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ShopTypesList()
        {
            ShopTypes model = new ShopTypes();
            using (EntitiesConnection EC = new EntitiesConnection())
            {
                var ShopTypesTypes = from c in EC.Shops
                                     where (c.ShopRegisterStatus == true) || (c.ShopRegisterStatus == false & SqlFunctions.DateDiff("ss", c.DateJoined, DateTime.Now) <= 7)
                                   group c by new { c.ShopType.Name, c.ShopType.Id } into grp
                                   select new
                                   {
                                       Name = grp.Key.Name,
                                       Id = grp.Key.Id,
                                       count = grp.Count(),
                                   };

                foreach (var q in ShopTypesTypes)
                {
                    ShopTypesList temp_shop = new ShopTypesList();
                    temp_shop.ShopTypesName = q.Name;
                    temp_shop.ShopTypesCount = q.count;
                    model.Types.Add(temp_shop);
                }


                var my = from c in EC.Shops
                         where (c.ShopRegisterStatus == true) || (c.ShopRegisterStatus == false & SqlFunctions.DateDiff("ss", c.DateJoined, DateTime.Now) <= 7)
                         group c by new { c.ShopState } into stategrp
                         select new
                         {
                             state = stategrp.Key.ShopState,
                             count = stategrp.Count(),
                             bycity = (from d in stategrp
                                       group d by new { d.ShopCity  } into citygrp
                                       select new
                                       {
                                           city = citygrp.Key.ShopCity,
                                           Id = citygrp.Key.ShopCity,
                                           count = citygrp.Count(),
                                           byType=( from e in citygrp group e by new {e.ShopType.Name} into typegrp
                                                    select new {
                                                    typename=typegrp.Key.Name,
                                                    typecount=typegrp.Count(),
                                                    }),
                                       }
                                           ),
                         };

                int stateId = 0;
                int CityId = 0;
                int TypeId = 0;

                foreach (var q in my) {
                    ShopTypeStateBase temp_shop_state = new ShopTypeStateBase();
                    temp_shop_state.Shop_state_Id = stateId++; ;
                    temp_shop_state.Shop_state_Name = q.state;
                    temp_shop_state.Shop_state_Count = q.count;
                    foreach (var p in q.bycity) {
                        ShopTypeCityBase temp_shop_city = new ShopTypeCityBase();
                        temp_shop_city.Shop_city_Id = CityId++; ;
                        temp_shop_city.Shop_city_Name = p.city;
                        temp_shop_city.Shop_city_Count = p.count;
                        foreach (var r in p.byType) {
                            ShopTypesList temp_shop = new ShopTypesList();
                            temp_shop.ShopTypesId =TypeId++;
                            temp_shop.ShopTypesName = r.typename;
                            temp_shop.ShopTypesCount = r.typecount;
                            temp_shop_city.Types.Add(temp_shop);
                        }
                        temp_shop_state.Types.Add(temp_shop_city);
                    }
                    model.DetailedTypes.Add(temp_shop_state);
                }

            }
            return PartialView("_CategoryPanel", model);

        }



     
        public JsonResult GetShoptDesc(string id)
        {
            int myid = int.Parse(id);
            using (EntitiesConnection EC = new EntitiesConnection())
            {
                Shop temp_s = EC.Shops.FirstOrDefault(u => u.Id == myid);
                if (temp_s != null)
                {
                    List<string> result = new List<string>();
                    result.Add((temp_s.ShopName == null) ? "نامشخص" : temp_s.ShopName);
                    result.Add((temp_s.ShopType.Name == null) ? "نامشخص" : temp_s.ShopType.Name);
                    result.Add((temp_s.ShopTell == null) ? "نامشخص" : temp_s.ShopTell);
                    result.Add((temp_s.ShopFax == null) ? "نامشخص" : temp_s.ShopFax);
                    result.Add((temp_s.ShopEmail == null) ? "نامشخص" : temp_s.ShopEmail);
                    result.Add((temp_s.ShopAddress == null) ? "نامشخص" : temp_s.ShopAddress);
                    result.Add((temp_s.PosterQuotes == null) ? "نامشخص" : temp_s.PosterQuotes);
                    result.Add((temp_s.ShopDescription == null) ? "نامشخص" : temp_s.ShopDescription);
                    result.Add((temp_s.PersonalSiteAddress == null) ? "نامشخص" : temp_s.PersonalSiteAddress);
                    result.Add((temp_s.InstagramAddress == null) ? "نامشخص" : temp_s.InstagramAddress);

                    var query = from c in EC.ShopImages where c.F_Shop_ID == temp_s.Id select c;
                    foreach (var q in query)
                    {
                        result.Add(System.Convert.ToBase64String(q.ShopImage1));
                    }
                    return Json(result);

                }
                else
                    return Json(id, JsonRequestBehavior.AllowGet);
            }

        }






        public ActionResult ShopsList(string City, string ScoreBase, string Category, string sortOrder, string searchString, string currentFilter, int? page)
        {
            if (City == "All") {
                ViewBag.ShopCategoryBase = Category;
            }
            else
            {
                ViewBag.ShopCategoryBase = Category + " در شهرستان " + City;
            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Score" ? "score_desc" : "Score";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            EntitiesConnection EC = new EntitiesConnection();
            //    {

            var query = Enumerable.Empty<Shop>().AsQueryable();

            if (City == "All")
            {
                if (Category == "All") {
                    query = from c in EC.Shops where (c.ShopRegisterStatus == true) || (c.ShopRegisterStatus == false & SqlFunctions.DateDiff("ss", c.DateJoined, DateTime.Now) <= 7) select c;
                } else {
                    query = from c in EC.Shops where (c.ShopType.Name == Category & c.ShopRegisterStatus == true) || (c.ShopType.Name == Category & c.ShopRegisterStatus == false & SqlFunctions.DateDiff("ss", c.DateJoined, DateTime.Now) <= 7) select c;
                }

                @ViewBag.CatBaseParam = Category;
                @ViewBag.CityBaseParam = City;

            }
            else {
                if (Category == "All") {
                    query = from c in EC.Shops where (c.ShopRegisterStatus == true & c.ShopCity == City) || (c.ShopRegisterStatus == false & c.ShopCity == City & SqlFunctions.DateDiff("ss", c.DateJoined, DateTime.Now) <= 7) select c;
                }else{

                    query = from c in EC.Shops where (c.ShopType.Name == Category & c.ShopRegisterStatus == true & c.ShopCity == City) || (c.ShopType.Name == Category & c.ShopRegisterStatus == false & c.ShopCity == City & SqlFunctions.DateDiff("ss", c.DateJoined, DateTime.Now) <= 7) select c;
                }

                @ViewBag.CatBaseParam = Category;
                @ViewBag.CityBaseParam = City;
            }
           


            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.ShopName.Contains(searchString)
                                       || s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }
            string sorttype = "Dec";
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(s => s.ShopName);
                    sorttype = "Dec";
                    break;
                default:
                    query = query.OrderBy(s => s.ShopName);
                    sorttype = "Asc";
                    break;
            }



            @ViewBag.SortTypeList = new SelectList(new[]
    {
        new { Id = "Dec", Name = "نزولی" },
        new { Id = "Asc", Name = "صعودی" },
    }, "Id", "Name");
            ViewBag.SortType = sorttype;

            int pageSize = 12;
            int pageNumber = (page ?? 1);

            return View(query.ToPagedList(pageNumber, pageSize));

        }

    }
}
