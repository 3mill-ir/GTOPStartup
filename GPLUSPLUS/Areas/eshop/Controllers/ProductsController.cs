using GPLUSPLUS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using GPLUSPLUS.Areas.eshop.Models;
using GPLUSPLUS.Models.Bussiness;
using WebMatrix.WebData;
using GPLUSPLUS.Areas.eshop.Models.Product;
using GPLUSPLUS.Filters;

namespace GPLUSPLUS.Areas.eshop.Controllers
{
     //[InitializeSimpleMembership]
    public class ProductsController : Controller
    {
        //
        // GET: /eshop/Products/

        public ActionResult Index()
        {
            return View();
        }


        [ChildActionOnly]
        public ActionResult ProductTypesList()
        {
            ProductTypes model = new ProductTypes();
            using (EntitiesConnection EC = new EntitiesConnection())
            {
                var ProductTypes = from c in EC.Products
                                   group c by new { c.ProductType.Name, c.ProductType.Id } into grp
                                   select new
                                   {
                                       Name = grp.Key.Name,
                                       Id = grp.Key.Id,
                                       count = grp.Count(),
                                       Count_Normal = (from c in grp where c.P_ScoreCost <= 100000 select c).Count(),
                                       Count_Boronz = (from c in grp where c.P_ScoreCost > 100000 && c.P_ScoreCost <= 500000 select c).Count(),
                                       Count_Silver = (from c in grp where c.P_ScoreCost > 500000 && c.P_ScoreCost <= 1500000 select c).Count(),
                                       Count_Gold = (from c in grp where c.P_ScoreCost > 1500000 select c).Count(),
                                   };

                foreach (var q in ProductTypes)
                {
                    ProductTypesList temp_product = new ProductTypesList();
                    temp_product.ProductTypesId = q.Id;
                    temp_product.ProductTypesName = q.Name;
                    temp_product.ProductTypesCount = q.count;
                    temp_product.ProductTypesCount_Normal = q.Count_Normal;
                    temp_product.ProductTypesCount_Boronz = q.Count_Boronz;
                    temp_product.ProductTypesCount_Silver = q.Count_Silver;
                    temp_product.ProductTypesCount_Gold = q.Count_Gold;
                    model.Types.Add(temp_product);
                }

                model.Types_Normal = (from c in EC.Products where c.P_ScoreCost <= 100000 select c).Count();
                model.Types_Boronz = (from c in EC.Products where c.P_ScoreCost > 100000 && c.P_ScoreCost <= 500000 select c).Count();
                model.Types_Silver = (from c in EC.Products where c.P_ScoreCost > 500000 && c.P_ScoreCost <= 1500000 select c).Count();
                model.Types_Gold = (from c in EC.Products where c.P_ScoreCost > 1500000 select c).Count();
            }
            return PartialView("_CategoryPanel", model);

        }

        public ActionResult ProductsList(string ScoreBase,string Category, string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Score" ? "score_desc" : "Score";

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

            var query = Enumerable.Empty<Product>().AsQueryable();
            if (ScoreBase == "normal")
            {
                if(Category=="All"){
                query = from c in EC.Products where c.P_ScoreCost <= 100000 select c;
                @ViewBag.ProductCategoryBase = "همه دسته ها";
                }else{
                      query = from c in EC.Products where c.P_ScoreCost <= 100000 & c.ProductType.Name==Category select c;
                      @ViewBag.ProductCategoryBase ="دسته "+ Category;
                }
                @ViewBag.ProductScoreBase = "جوایز عادی";
                @ViewBag.ScoreBaseParam = "normal";
                @ViewBag.CatBaseParam = Category;
            }
            else if (ScoreBase == "boronz")
            {
                if (Category == "All")
                {
                    query = from c in EC.Products where c.P_ScoreCost > 100000 & c.P_ScoreCost <= 500000 select c;
                    @ViewBag.ProductCategoryBase = "همه دسته ها";
                }
                else { query = from c in EC.Products where c.P_ScoreCost > 100000 & c.P_ScoreCost <= 500000 & c.ProductType.Name == Category select c;
                @ViewBag.ProductCategoryBase = "دسته " + Category;
                }
                @ViewBag.ProductScoreBase = "جوایز برنزی";
                @ViewBag.ScoreBaseParam = "boronz";
                @ViewBag.CatBaseParam = Category;
            }
            else if (ScoreBase == "silver")
            {
                if (Category == "All")
                {
                    query = from c in EC.Products where c.P_ScoreCost > 500000 & c.P_ScoreCost <= 1500000 select c;
                    @ViewBag.ProductCategoryBase = "همه دسته ها";
                }
                else { query = from c in EC.Products where c.P_ScoreCost > 500000 & c.P_ScoreCost <= 1500000 & c.ProductType.Name==Category select c;
                @ViewBag.ProductCategoryBase = "دسته " + Category;
                }
                @ViewBag.ProductScoreBase = "جوایز نقره ایی";
                @ViewBag.ScoreBaseParam = "silver";
                @ViewBag.CatBaseParam = Category;
                
            }
            else if (ScoreBase == "gold")
            {
                if (Category=="All")
                {
                    query = from c in EC.Products where c.P_ScoreCost > 1500000 select c;
                    @ViewBag.ProductCategoryBase = "همه دسته ها";
                }
                else { query = from c in EC.Products where c.P_ScoreCost > 1500000 & c.ProductType.Name == Category select c;
                @ViewBag.ProductCategoryBase = "دسته " + Category;
                }
                @ViewBag.ProductScoreBase = "جوایز طلایی";
                @ViewBag.ScoreBaseParam = "gold";
                @ViewBag.CatBaseParam = Category;
            }
            else if (ScoreBase == "All")
            {
                if (Category == "All")
                {
                    query = from c in EC.Products select c;
                }
                else { query = from c in EC.Products where c.ProductType.Name == Category select c; }
                @ViewBag.ProductScoreBase = "تمامی جوایز";
                @ViewBag.ScoreBaseParam = "All";
                @ViewBag.CatBaseParam = Category;
            }
            else {
                return View("NOT Found");
            }
           

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(s => s.P_Name.Contains(searchString)
                                       || s.P_ModelName.Contains(searchString) || s.P_Brand.Contains(searchString));
            }
            string sortbase = "Name";
            string sorttype = "Dec";
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(s => s.P_Brand);
                    sortbase="Name";
                    sorttype = "Dec";
                    break;
                case "Score":
                    query = query.OrderBy(s => s.P_ScoreCost);
                    sortbase="Score";
                    sorttype = "Asc";
                    break;
                case "score_desc":
                    query = query.OrderByDescending(s => s.P_ScoreCost);
                    sortbase="Score";
                    sorttype = "Dec";
                    break;
                default:
                    query = query.OrderBy(s => s.P_Brand);
                    sortbase="Name";
                    sorttype = "Asc";
                    break;
            }
            @ViewBag.SortBaseList=  new SelectList(new[]
    {
        new { Id = "Name", Name = "نام" },
        new { Id = "Score", Name = "امتیاز" },
    }, "Id", "Name");
            ViewBag.SortBase = sortbase;


            @ViewBag.SortTypeList = new SelectList(new[]
    {
        new { Id = "Dec", Name = "نزولی" },
        new { Id = "Asc", Name = "صعودی" },
    }, "Id", "Name");
            ViewBag.SortType = sorttype;

            int pageSize = 12;
            int pageNumber = (page ?? 1);

            using (UsersContext db = new UsersContext())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                if (user != null)
                {
                    string welcome = "جناب ";
                    if (user.U_Gender.Equals("مرد"))
                        welcome = welcome + "آقای ";
                    else
                        welcome = welcome + "خانم ";
                    @ViewBag.UserFullName = user.U_FirstName + " " + user.U_LastName;
                    @ViewBag.Userwelcome = welcome;
                }
            }
            return View(query.ToPagedList(pageNumber, pageSize));

        }


        [AllowAnonymous]
        [HttpPost]
        public JsonResult GetProductDesc(string id)
        {
            int myid = int.Parse(id);
            using (EntitiesConnection EC = new EntitiesConnection())
            {
                Product temp_p = EC.Products.FirstOrDefault(u => u.P_ID == myid);
                if (temp_p != null)
                {
                    List<string> result = new List<string>();
                    result.Add(temp_p.P_Name);
                    result.Add(temp_p.P_Brand);
                    result.Add(temp_p.P_ModelName);
                    result.Add(temp_p.P_Description);
                    result.Add(temp_p.P_Cost.ToString());
                    var query = from c in EC.ProductImages where c.F_P_ID == temp_p.P_ID select c;
                    foreach (var q in query)
                    {
                        result.Add(System.Convert.ToBase64String(q.PI_Image));
                    }
                    return Json(result);
                }
                else
                    return Json(id);
            }

        }


        [Authorize]
        public JsonResult GetProductBuy(string id)
        {
            using (UsersContext db = new UsersContext())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                if (user != null)
                {
                    string welcome = "جناب ";
                    if (user.U_Gender.Equals("مرد"))
                        welcome = welcome + "آقای ";
                    else
                        welcome = welcome + "خانم ";
                    @ViewBag.UserFullName = user.U_FirstName + " " + user.U_LastName;
                    @ViewBag.Userwelcome = welcome;
                }
            }
            int myid = int.Parse(id);
            using (EntitiesConnection EC = new EntitiesConnection())
            {
                Product temp_p = EC.Products.FirstOrDefault(u => u.P_ID == myid);
                if (temp_p != null)
                {
                    ScoreManagement SM = new ScoreManagement();
                    int TotalScore = SM.TotalUserScore(WebSecurity.CurrentUserId) ?? default(int);
                    List<string> result = new List<string>();
                    if (TotalScore >= temp_p.P_ScoreCost)
                    {
                        result.Add("OK");
                        int temp_p_scorCost = temp_p.P_ScoreCost ?? default(int);
                        int ScoreAfterBuy = TotalScore - temp_p_scorCost;
                        result.Add(ScoreAfterBuy.ToString());


                    }
                    else
                    {

                        result.Add("NOK");
                        int temp_p_scorCost = temp_p.P_ScoreCost ?? default(int);
                        int ScoreAfterBuy = temp_p_scorCost - TotalScore;
                        result.Add(ScoreAfterBuy.ToString());

                    }

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(id);
            }

        }

        [Authorize]
        public ActionResult BuyProduct(string product)
        {
            using (UsersContext db = new UsersContext())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                if (user != null)
                {
                    string welcome = "جناب ";
                    if (user.U_Gender.Equals("مرد"))
                        welcome = welcome + "آقای ";
                    else
                        welcome = welcome + "خانم ";
                    @ViewBag.UserFullName = user.U_FirstName + " " + user.U_LastName;
                    @ViewBag.Userwelcome = welcome;
                }
            }
            ViewData["StateName"] = AddressManagement.LoadState();
            BuyProductModel model = new BuyProductModel();

            if (string.IsNullOrEmpty(product))
            {
                return View("NoProduct");
            }
            int productID;
            if (int.TryParse(product, out productID) == false)
            {
                return View("NoProduct");
            }

            using (EntitiesConnection EC = new EntitiesConnection())
            {
                Product temp_p = EC.Products.FirstOrDefault(u => u.P_ID == productID);
                if (temp_p == null)
                {
                    return View("NoProduct");
                }
                ScoreManagement SM = new ScoreManagement();
                int TotalScore = SM.TotalUserScore(WebSecurity.CurrentUserId) ?? default(int);
                if (TotalScore < temp_p.P_ScoreCost)
                {
                    return View("LowScore");
                }

                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Account", new { ReturnUrl = "products/buyproduct?product=" + product });
                    }

                    if (string.IsNullOrEmpty(user.U_Address))
                    {
                        return RedirectToAction("CompleteProfile", "Account", new { returnurl = "products/buyproduct?product=" + product });
                    }

                    model.U_FullName = user.U_FirstName + " " + user.U_LastName;
                    model.U_Tell = user.UserId.ToString();
                    model.U_PostalCode = user.U_CodeMelli;
                    model.U_FullAddress = user.U_State + " " + user.U_City + " " + user.U_Address;
                }

                model.P_ID = temp_p.P_ID;
                model.P_Brand = temp_p.P_Brand;
                model.P_Category = temp_p.ProductType.Name;
                model.P_Description = temp_p.P_Description;
                model.P_ModelName = temp_p.P_ModelName;
                model.P_Name = temp_p.P_Name;
                model.P_ScoreCost = temp_p.P_Cost;

                var query = from c in EC.ProductImages where c.F_P_ID == temp_p.P_ID select c;
                foreach (var q in query)
                {

                    model.ImageThumb.Add(string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.PI_Thumb)));
                    model.ImageThumbID.Add(q.PI_ID);
                }
                return View(model);
            }

        }

        [Authorize]
        [HttpPost]
        public ActionResult BuyProduct(ProductBuy model, string product)
        {
            using (UsersContext db = new UsersContext())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                if (user != null)
                {
                    string welcome = "جناب ";
                    if (user.U_Gender.Equals("مرد"))
                        welcome = welcome + "آقای ";
                    else
                        welcome = welcome + "خانم ";
                    @ViewBag.UserFullName = user.U_FirstName + " " + user.U_LastName;
                    @ViewBag.Userwelcome = welcome;
                }
            }
            ViewData["StateName"] = AddressManagement.LoadState();

            if (model.HaveNewAddress == true)
            {
                if (string.IsNullOrEmpty(model.PU_NewState))
                {
                    ModelState.AddModelError("PU_NewState", "");
                    @ViewBag.MyErrorState = "لطفاً استان را تعیین کنید.";
                }

                if (!string.IsNullOrEmpty(model.PU_NewCity))
                {
                    if (model.PU_NewCity.Equals("انتخاب کنید"))
                    {
                        ModelState.AddModelError("PU_NewCity", "");
                        @ViewBag.MyErrorCity = "لطفاً شهرستان را تعیین کنید.";
                    }
                }

                if (string.IsNullOrEmpty(model.PU_NewAddress))
                {
                    ModelState.AddModelError("PU_NewAddress", "");
                    @ViewBag.MyErrorState = "لطفاً آدرس پستی را وارد کنید.";
                }

                if (string.IsNullOrEmpty(model.PU_NewPostalCode))
                {
                    ModelState.AddModelError("PU_NewPostalCode", "");
                    @ViewBag.MyErrorState = "لطفاً کدپستی را وارد کنید.";
                }
            }
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(product))
                {
                    return View("NoProduct");
                }
                int productID;
                if (int.TryParse(product, out productID) == false)
                {
                    return View("NoProduct");
                }

                using (EntitiesConnection EC = new EntitiesConnection())
                {
                    Product temp_p = EC.Products.FirstOrDefault(u => u.P_ID == productID);
                    if (temp_p == null)
                    {
                        return View("NoProduct");
                    }
                    ScoreManagement SM = new ScoreManagement();
                    int TotalScore = SM.TotalUserScore(WebSecurity.CurrentUserId) ?? default(int);
                    if (TotalScore < temp_p.P_ScoreCost)
                    {
                        return View("LowScore");
                    }

                    using (UsersContext db = new UsersContext())
                    {
                        UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                        if (user == null)
                        {
                            return RedirectToAction("Login", "Account", new { ReturnUrl = "products/buyproduct?product=" + product });
                        }

                        if (string.IsNullOrEmpty(user.U_Address))
                        {
                            return RedirectToAction("CompleteProfile", "Account", new { returnurl = "products/buyproduct?product=" + product });
                        }


                        if (model.HaveNewAddress == false)
                        {
                            ProductUsed Pused = new ProductUsed();
                            Pused.F_P_ID = temp_p.P_ID;
                            Pused.F_UserID = WebSecurity.CurrentUserId;
                            Pused.PU_DateBuyed = DateTime.Now;
                            Pused.PU_IsNewAddress = false;
                            Pused.PU_ScoreUsed = temp_p.P_Cost;
                            EC.ProductUseds.Add(Pused);
                        }
                        else
                        {
                            ProductUsed Pused = new ProductUsed();
                            Pused.F_P_ID = temp_p.P_ID;
                            Pused.F_UserID = WebSecurity.CurrentUserId;
                            Pused.PU_DateBuyed = DateTime.Now;
                            Pused.PU_IsNewAddress = false;
                            Pused.PU_NewState = model.PU_NewState;
                            Pused.PU_NewCity = model.PU_NewCity;
                            Pused.PU_NewAddress = model.PU_NewAddress;
                            Pused.PU_NewPostalCode = model.PU_NewPostalCode;
                            Pused.PU_ScoreUsed = temp_p.P_Cost;
                            EC.ProductUseds.Add(Pused);

                        }

                        int temp_p_scorCost = temp_p.P_Cost ?? default(int);
                        int ScoreAfterBuy = TotalScore - temp_p_scorCost;
                        user.U_Score = ScoreAfterBuy;
                        EC.SaveChanges();
                        db.SaveChanges();
                    }

                }
                return View("ProductSumbited");
            }
            else
            {
                return View(model);
            }


        }

    }
}
