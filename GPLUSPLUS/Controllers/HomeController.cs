using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPLUSPLUS.Models.Bussiness;
using GPLUSPLUS.Models;
using WebMatrix.WebData;
using GPLUSPLUS.Filters;
using System.Data.Entity.Validation;
using System.Web.Hosting;
using System.Globalization;
using System.IO;

namespace GPLUSPLUS.Controllers
{
    [AllowAnonymous]
    //[InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public const int RecordsPerPage = 5;

        public void SMSReciver(string SecureString, string msg, string from)
        {
            if (SecureString == "PARSADORSA")
            {
                SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                BLL.Receive_SMS(msg, from);
            }
        }

        public ActionResult GetFile(int FileName)
        {
            string FullPath = "";
            using (EntitiesConnection EC = new EntitiesConnection())
            {
                var Product = EC.ProductImages.FirstOrDefault(u => u.PI_ID == FileName);
                if (Product != null)
                {
                    byte[] filedata = Product.PI_Image;
                    string contentType = Path.GetExtension(FullPath);
                    return File(filedata, ".png");
                }
                else
                    return null;
            }
        }
        public string GetScoreForApp(string price)
        {
            try
            {
                using (EntitiesConnection EC_DB = new EntitiesConnection())
                {
                    var code = EC_DB.ScoreCards.FirstOrDefault(u => u.CardStatus == true && u.UsedDate == null && u.CodeScore == 1);
                    if (code != null)
                    {
                        code.CardStatus = false;
                        EC_DB.SaveChanges();
                        return code.TrackingCode;
                    }
                    return "خطا در تولید کد";
                }
            }
            catch
            {
                return "متاسفانه در فرآیند تولید کد خطایی رخ داده است";
            }
        }


        public HomeController()
        {
            ViewBag.RecordsPerPage = RecordsPerPage;
        }

        [AllowAnonymous]
        public ActionResult Index(int? pageNum, int? AjaM, int? AjaY)
        {
            ScoreManagement SM = new ScoreManagement();
            int? BonyadKoodak = SM.TotalBonyadKoodakScore();
            if (BonyadKoodak == null)
            {
                @ViewBag.BonyadKoodakScore = "صفر ";
            }
            else
            {
                @ViewBag.BonyadKoodakScore = BonyadKoodak;
            }


            if (User.Identity.IsAuthenticated)
            {
                double MaxScore = 12000000;
                double MinScore = 0;
                @ViewBag.MaxScore = MaxScore;
                @ViewBag.MinScore = MinScore;
                @ViewBag.Range = MaxScore - MinScore;

                //****part
                double MaxScore_part = 10000;
                double MinScore_part = 0;
                @ViewBag.MaxScore_part = MaxScore_part;
                @ViewBag.MinScore_part = MinScore_part;
                @ViewBag.Range_part = MaxScore_part - MinScore_part;



                int? TotalScore = SM.TotalUserScore(WebSecurity.CurrentUserId);

                double? PercentageScore = (TotalScore - MinScore) / (MaxScore - MinScore);
                @ViewBag.TotalScore = TotalScore;
                @ViewBag.PercentageScore = PercentageScore;

                //****part
                double? PercentageScore_part = (TotalScore - MinScore_part) / (MaxScore_part - MinScore_part);
                @ViewBag.PercentageScore_part = PercentageScore_part;
                string lotteryScore = SM.TotalUserLotteryScore(WebSecurity.CurrentUserId).ToString();
                if (string.IsNullOrEmpty(lotteryScore))
                {
                    @ViewBag.LotteryScore = "0";
                }
                else
                {
                    @ViewBag.LotteryScore = lotteryScore;
                }


                //using (EntitiesConnection EC_DB = new EntitiesConnection())
                //{
                //    var RandomProducts = EC_DB.ProductImages.OrderBy(x => Guid.NewGuid()).Take(25).Select(u => u.PI_Thumb).ToList();
                //    List<byte[]> ListofProducts_row1 = new List<byte[]>();
                //    List<byte[]> ListofProducts_row2 = new List<byte[]>();
                //    List<byte[]> ListofProducts_row3 = new List<byte[]>();
                //    List<byte[]> ListofProducts_row4 = new List<byte[]>();
                //    List<byte[]> ListofProducts_row5 = new List<byte[]>();
                //    int counter = 0;
                //    foreach (var q in RandomProducts)
                //    {
                //        if (counter < 5)
                //            ListofProducts_row1.Add(q);
                //        else if (counter < 10)
                //            ListofProducts_row2.Add(q);
                //        else if (counter < 15)
                //            ListofProducts_row3.Add(q);
                //        else if (counter < 20)
                //            ListofProducts_row4.Add(q);
                //        else if (counter < 25)
                //            ListofProducts_row5.Add(q);
                //        counter++;
                //    }
                //    ViewBag.ListofProducts_row1 = ListofProducts_row1;
                //    ViewBag.ListofProducts_row2 = ListofProducts_row2;
                //    ViewBag.ListofProducts_row3 = ListofProducts_row3;
                //    ViewBag.ListofProducts_row4 = ListofProducts_row4;
                //    ViewBag.ListofProducts_row5 = ListofProducts_row5;


                //    var RandomShops = EC_DB.ShopImages.OrderBy(x => Guid.NewGuid()).Take(25).Select(u => u.ShopThumbnail).ToList();
                //    List<byte[]> ListofShops_row1 = new List<byte[]>();
                //    List<byte[]> ListofShops_row2 = new List<byte[]>();
                //    List<byte[]> ListofShops_row3 = new List<byte[]>();
                //    List<byte[]> ListofShops_row4 = new List<byte[]>();
                //    List<byte[]> ListofShops_row5 = new List<byte[]>();
                //    counter = 0;
                //    foreach (var q in RandomShops)
                //    {
                //        if (counter < 5)
                //            ListofShops_row1.Add(q);
                //        else if (counter < 10)
                //            ListofShops_row2.Add(q);
                //        else if (counter < 15)
                //            ListofShops_row3.Add(q);
                //        else if (counter < 20)
                //            ListofShops_row4.Add(q);
                //        else if (counter < 25)
                //            ListofShops_row5.Add(q);
                //        counter++;
                //    }
                //    ViewBag.ListofShops_row1 = ListofShops_row1;
                //    ViewBag.ListofShops_row2 = ListofShops_row2;
                //    ViewBag.ListofShops_row3 = ListofShops_row3;
                //    ViewBag.ListofShops_row4 = ListofShops_row4;
                //    ViewBag.ListofShops_row5 = ListofShops_row5;
                //}

                pageNum = pageNum ?? 0;
                ViewBag.IsEndOfRecords = false;

                if (Request.IsAjaxRequest())
                {
                    var customers = GetRecordsForPage(pageNum.Value);
                    ViewBag.IsEndOfRecords = (customers.Any()) && ((pageNum.Value * RecordsPerPage) >= customers.Last().Key);
                    ViewBag.lastAjaxDate_year = AjaY.Value.ToString();
                    ViewBag.lastAjaxDate_month = AjaM.Value.ToString();
                    return PartialView("_CustomerRow", customers);
                }
                else
                {
                    LoadAllCustomersToSession();
                    ViewBag.Customers = GetRecordsForPage(pageNum.Value);


                    using (EntitiesConnection EC = new EntitiesConnection())
                    {
                        if (User.Identity.IsAuthenticated)
                        {

                            using (UsersContext db = new UsersContext())
                            {
                                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                                if (user != null)
                                {
                                    var ScoreHighproducts = (from c in EC.ProductImages where c.Product.P_ScoreCost > user.U_Score orderby c.Product.P_ScoreCost ascending select c.PI_Image).Take(10);
                                    ViewBag.ScoreHighproducts = ScoreHighproducts.ToList();

                                    var ScoreLowproducts = (from c in EC.ProductImages where c.Product.P_ScoreCost <= user.U_Score orderby c.Product.P_ScoreCost ascending select c.PI_Image).Take(10);
                                    ViewBag.ScoreLowproducts = ScoreLowproducts.ToList();
                                    if (ScoreLowproducts.Count() != 0)
                                    {
                                        ViewBag.ScoreLowproducts = ScoreLowproducts.ToList();
                                    }
                                    else
                                    {
                                        var ScoreRandomproducts = EC.ProductImages.Select(x => x.PI_Image).OrderBy(x => Guid.NewGuid()).Take(10);
                                        ViewBag.ScoreRandomproducts = ScoreRandomproducts.ToList();
                                    }
                                }
                                string welcome = "جناب ";
                                if (user.U_Gender.Equals("مرد"))
                                    welcome = welcome + "آقای ";
                                else
                                    welcome = welcome + "خانم ";
                                @ViewBag.UserFullName = user.U_FirstName + " " + user.U_LastName;
                                @ViewBag.Userwelcome = welcome;
                                if (user.FreeCodeGTOP == false)
                                {
                                    string freeCodeNotify = "شما هنوز از کد رایگان 2000 امتیازی خرید استفاده نکرده اید. جهت دریافت 2000 امتیاز رایگان عبارت \"gtopco\" را وارد نمایید.";
                                    @ViewBag.freeCodeNotify = freeCodeNotify;
                                }
                            }
                        }
                    }
                    return View("Index");
                }
            }
            else
            {
                using (EntitiesConnection EC_DB = new EntitiesConnection())
                {
                    var RandomProducts = EC_DB.ProductImages.OrderBy(x => Guid.NewGuid()).Take(25).Select(u => u.PI_Thumb).ToList();
                    List<byte[]> ListofProducts_row1 = new List<byte[]>();
                    List<byte[]> ListofProducts_row2 = new List<byte[]>();
                    List<byte[]> ListofProducts_row3 = new List<byte[]>();
                    List<byte[]> ListofProducts_row4 = new List<byte[]>();
                    List<byte[]> ListofProducts_row5 = new List<byte[]>();
                    int counter = 0;
                    foreach (var q in RandomProducts)
                    {
                        if (counter < 5)
                            ListofProducts_row1.Add(q);
                        else if (counter < 10)
                            ListofProducts_row2.Add(q);
                        else if (counter < 15)
                            ListofProducts_row3.Add(q);
                        else if (counter < 20)
                            ListofProducts_row4.Add(q);
                        else if (counter < 25)
                            ListofProducts_row5.Add(q);
                        counter++;
                    }
                    ViewBag.ListofProducts_row1 = ListofProducts_row1;
                    ViewBag.ListofProducts_row2 = ListofProducts_row2;
                    ViewBag.ListofProducts_row3 = ListofProducts_row3;
                    ViewBag.ListofProducts_row4 = ListofProducts_row4;
                    ViewBag.ListofProducts_row5 = ListofProducts_row5;


                    var RandomShops = EC_DB.ShopImages.OrderBy(x => Guid.NewGuid()).Take(25).Select(u => u.ShopThumbnail).ToList();
                    List<byte[]> ListofShops_row1 = new List<byte[]>();
                    List<byte[]> ListofShops_row2 = new List<byte[]>();
                    List<byte[]> ListofShops_row3 = new List<byte[]>();
                    List<byte[]> ListofShops_row4 = new List<byte[]>();
                    List<byte[]> ListofShops_row5 = new List<byte[]>();
                    counter = 0;
                    foreach (var q in RandomShops)
                    {
                        if (counter < 5)
                            ListofShops_row1.Add(q);
                        else if (counter < 10)
                            ListofShops_row2.Add(q);
                        else if (counter < 15)
                            ListofShops_row3.Add(q);
                        else if (counter < 20)
                            ListofShops_row4.Add(q);
                        else if (counter < 25)
                            ListofShops_row5.Add(q);
                        counter++;
                    }
                    ViewBag.ListofShops_row1 = ListofShops_row1;
                    ViewBag.ListofShops_row2 = ListofShops_row2;
                    ViewBag.ListofShops_row3 = ListofShops_row3;
                    ViewBag.ListofShops_row4 = ListofShops_row4;
                    ViewBag.ListofShops_row5 = ListofShops_row5;
                }
                return View("welcome");
            }
        }

        public void LoadAllCustomersToSession()
        {


            var customerRepo = new CustomerRepository();
            var customers = customerRepo.ListCustomers();
            int custIndex = 1;
            Session["Customers"] = customers.ToDictionary(x => custIndex++, x => x);
            ViewBag.TotalNumberCustomers = customers.Count();
        }
        public Dictionary<int, Customer> GetRecordsForPage(int pageNum)
        {
            Dictionary<int, Customer> customers = (Session["Customers"] as Dictionary<int, Customer>);

            int from = (pageNum * RecordsPerPage);
            int to = from + RecordsPerPage;


            return customers
                .Where(x => x.Key > from && x.Key <= to)
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
        }





        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult HomeEshopSliderUnAuth()
        {

            return PartialView();
        }



        [ChildActionOnly]
        public ActionResult HomeNearestProducts()
        {
            ProductsModelNew model = new ProductsModelNew();
            using (EntitiesConnection EC = new EntitiesConnection())
            {
                if (User.Identity.IsAuthenticated)
                {

                    using (UsersContext db = new UsersContext())
                    {
                        UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                        if (user != null)
                        {
                            var ScoreHighproducts = (from c in EC.ProductImages where c.Product.P_ScoreCost > user.U_Score orderby c.Product.P_ScoreCost ascending select c.PI_Thumb).Take(10);
                            ViewBag.ScoreHighproducts = ScoreHighproducts.ToList();

                            var ScoreLowproducts = (from c in EC.ProductImages where c.Product.P_ScoreCost < user.U_Score orderby c.Product.P_ScoreCost ascending select c.PI_Thumb).Take(10);
                            ViewBag.ScoreLowproducts = ScoreLowproducts.ToList();
                        }
                    }
                }
            }
            return PartialView(model);
        }


        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult GPlusNews()
        {

            if (User.Identity.IsAuthenticated)
            {
                return PartialView();
            }
            else
            {
                return PartialView("UnAuthGPlusNews");
            }

        }

        [AllowAnonymous]
        public JsonResult GetCity(string id)
        {

            return Json(new SelectList(AddressManagement.GetCity(id), "Value", "Text"));
        }
        public PartialViewResult NewProductsSlider()
        {

            return PartialView("_NewProductsSliderPartial");
        }

        //public PartialViewResult QVisitosMessage(HomeModel  model) {
        //if (string.IsNullOrEmpty(model.Q_name))
        //{
        //    ModelState.AddModelError("Q_name", "");
        //    @ViewBag.MyErrorNoQName = "لطفاً نام را وارد نمایید.";
        //}
        //if (string.IsNullOrEmpty(model.Q_Email))
        //{
        //    ModelState.AddModelError("Q_Email", "");
        //    @ViewBag.MyErrorNoQEmail = "جهت مکاتبات بیشتر لطفاً ایمیل را وارد نمایید.";
        //}
        //if (string.IsNullOrEmpty(model.Q_Message))
        //{
        //    ModelState.AddModelError("Q_Message", "");
        //    @ViewBag.MyErrorNoQMessage = "لطفاً متن پیغام را وارد نمایید.";
        //}
        //if (ModelState.IsValid)
        //{
        //    using (EntitiesConnection EC_DB = new EntitiesConnection())
        //    {
        //        VisitorsMessage VM = new VisitorsMessage();
        //        VM.Name = model.Q_name;
        //        VM.Email = model.Q_Email;
        //        VM.MessageText = model.Q_Message;
        //        VM.MessageDate = DateTime.Now;
        //        EC_DB.VisitorsMessages.Add(VM);
        //        EC_DB.SaveChanges();
        //        return PartialView("_QThankYou");

        //    }

        //}
        //return PartialView("_QVisitorsMessage",model);

        //}

        public PartialViewResult ScoreIncrease(string CodeNumber)
        {
            double MaxScore = 12000000;
            double MinScore = 0;
            @ViewBag.MaxScore = MaxScore;
            @ViewBag.MinScore = MinScore;
            @ViewBag.Range = MaxScore - MinScore;
            ScoreManagement SM = new ScoreManagement();
            int? TotalScore = SM.TotalUserScore(WebSecurity.CurrentUserId);
            double? PercentageScore = (TotalScore - MinScore) / (MaxScore - MinScore);

            @ViewBag.TotalScore = TotalScore;
            @ViewBag.PercentageScore = PercentageScore;

            //*****part
            double MaxScore_part = 100;
            double MinScore_part = 0;
            @ViewBag.MaxScore_part = MaxScore_part;
            @ViewBag.MinScore_part = MinScore_part;
            @ViewBag.Range_part = MaxScore_part - MinScore_part;
            double? PercentageScore_part = (TotalScore - MinScore_part) / (MaxScore_part - MinScore_part);
            @ViewBag.PercentageScore_part = PercentageScore_part;



            if (string.IsNullOrEmpty(CodeNumber))
            {

                @ViewBag.MyCodeNumberResult = "لطفاً کد امتیاز را وارد نمایید.";

            }
            else
            {
                string result;
                result = SM.NewScore(WebSecurity.CurrentUserId, CodeNumber);

                if (result.Equals("Code Used Before"))
                {
                    @ViewBag.MyCodeNumberResult = "کد امتیاز قبلاً استفاده شده است.";

                }
                else if (result.Equals("Free Code Used Befor"))
                {
                    @ViewBag.MyCodeNumberResult = "شما قبلاً از کد رایگان استفاده نموده اید.";

                }
                else if (result.Equals("User Blocked BrutForced Score"))
                {
                    @ViewBag.MyCodeNumberResult = "شما قادر به انجام این عملیات نمی باشید زیرا بیشتر از تعداد مجاز، کد امتیاز را به صورت اشتباه وارد نموده اید. جهت اطلاعات بیشتر با مرکز GTOP تماس حاصل فرمایید.";

                }
                else if (result.Equals("Code Not Found"))
                {
                    @ViewBag.MyCodeNumberResult = "کد امتیاز نا معتبر است.";

                }
                else
                {
                    @ViewBag.MyCodeNumberResult = "کد امتیاز با موفقیت ثبت شد.";

                }
            }
            int? NewTotalScore = SM.TotalUserScore(WebSecurity.CurrentUserId);
            double? NewPercentageScore = (NewTotalScore - MinScore) / (MaxScore - MinScore);
            @ViewBag.NewTotalScore = NewTotalScore;
            @ViewBag.NewPercentageScore = NewPercentageScore;

            string lotteryScore = SM.TotalUserLotteryScore(WebSecurity.CurrentUserId).ToString();
            if (string.IsNullOrEmpty(lotteryScore))
            {
                @ViewBag.LotteryScore = "0";
            }
            else
            {
                @ViewBag.LotteryScore = lotteryScore;
            }

            int? BonyadKoodak = SM.TotalBonyadKoodakScore();
            if (BonyadKoodak == null)
            {
                @ViewBag.BonyadKoodakScore = "صفر ";
            }
            else
            {
                @ViewBag.BonyadKoodakScore = BonyadKoodak;
            }

            //******part
            double? NewPercentageScore_part = (NewTotalScore - MinScore_part) / (MaxScore_part - MinScore_part);
            @ViewBag.NewPercentageScore_part = NewPercentageScore_part;
            return PartialView("_ScoreIncrease");


        }


        //public ActionResult Indexold()
        //{

        //    if (User.Identity.IsAuthenticated) {
        //        double MaxScore = 12000000;
        //        double MinScore = 0;
        //        @ViewBag.MaxScore = MaxScore;
        //        @ViewBag.MinScore = MinScore;
        //        @ViewBag.Range = MaxScore - MinScore;

        //        //****part
        //        double MaxScore_part = 100;
        //        double MinScore_part = 0;
        //        @ViewBag.MaxScore_part = MaxScore_part;
        //        @ViewBag.MinScore_part = MinScore_part;
        //        @ViewBag.Range_part = MaxScore_part - MinScore_part;


        //        ScoreManagement SM = new ScoreManagement();
        //        int? TotalScore = SM.TotalUserScore(WebSecurity.CurrentUserId);

        //        double? PercentageScore = (TotalScore - MinScore) / (MaxScore - MinScore);
        //        @ViewBag.TotalScore = TotalScore;
        //        @ViewBag.PercentageScore = PercentageScore;

        //        //****part
        //        double? PercentageScore_part = (TotalScore - MinScore_part) / (MaxScore_part - MinScore_part);
        //        @ViewBag.PercentageScore_part = PercentageScore_part;


        //        using (UsersContext db = new UsersContext())
        //        {
        //            UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
        //            if (user != null)
        //            {
        //                string welcome = "جناب ";
        //                if (user.U_Gender.Equals("مرد"))
        //                    welcome = welcome + "آقای ";
        //                else
        //                    welcome = welcome + "خانم ";
        //                @ViewBag.UserFullName = user.U_FirstName + " " + user.U_LastName;
        //                @ViewBag.Userwelcome = welcome;
        //                if (user.FreeCodeGTOP == false)
        //                {
        //                    string freeCodeNotify = "شما هنوز از کد رایگان 5 امتیازی استفاده نکرده اید. جهت دریافت 5 امتیاز رایگان عبارت \"www.gtop.ir\" را وارد نمایید.";
        //                    @ViewBag.freeCodeNotify = freeCodeNotify;
        //                }


        //            }
        //        }
        //    }
        //    else
        //    {
        //        ViewData["StateName"] = AddressManagement.LoadState();
        //    }

        //    List<string[]> Products_Normal = new List<string[]>();
        //    List<string[]> Products_Boronz = new List<string[]>();
        //    List<string[]> Products_Silver = new List<string[]>();
        //    List<string[]> Products_Gold = new List<string[]>();
        //    using (EntitiesConnection EC = new EntitiesConnection())
        //    {
        //        var query_normal = (from c in EC.ProductImages where c.Product.P_ScoreCost <=1000  select c).OrderByDescending(c=>c.Product.P_DateAdded).Take(5);
        //        foreach (var q in query_normal)
        //        {
        //            string[] product = new string[3];
        //            product[0] = q.Product.P_ID.ToString();
        //            product[1] = q.Product.P_Brand + "-" + q.Product.P_ModelName;
        //            product[2]=string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.PI_Thumb));
        //            Products_Normal.Add(product);
        //        }

        //        var query_boronz = (from c in EC.ProductImages where c.Product.P_ScoreCost > 1000 & c.Product.P_ScoreCost <= 5000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(5);
        //        foreach (var q in query_boronz)
        //        {
        //            string[] product = new string[3];
        //            product[0] = q.Product.P_ID.ToString();
        //            product[1] = q.Product.P_Brand + "-" + q.Product.P_ModelName;
        //            product[2] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.PI_Thumb));
        //            Products_Boronz.Add(product);
        //        }

        //        var query_silver = (from c in EC.ProductImages where c.Product.P_ScoreCost > 5000 & c.Product.P_ScoreCost <= 15000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(5);
        //        foreach (var q in query_silver)
        //        {
        //            string[] product = new string[3];
        //            product[0] = q.Product.P_ID.ToString();
        //            product[1] = q.Product.P_Brand + "-" + q.Product.P_ModelName;
        //            product[2] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.PI_Thumb));
        //            Products_Silver.Add(product);
        //        }


        //        var query_gold = (from c in EC.ProductImages where c.Product.P_ScoreCost > 15000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(5);
        //        foreach (var q in query_gold)
        //        {
        //            string[] product = new string[3];
        //            product[0] = q.Product.P_ID.ToString();
        //            product[1] = q.Product.P_Brand + "-" + q.Product.P_ModelName;
        //            product[2] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.PI_Thumb));
        //            Products_Gold.Add(product);
        //        }
        //    }

        //    ViewData["myNormalProducts"] = Products_Normal;
        //    ViewData["myboronzProducts"] = Products_Boronz;
        //    ViewData["mySilverProducts"] = Products_Silver;
        //    ViewData["myGoldProducts"] = Products_Gold; 

        //    return View();
        //}

        /*     [HttpPost]
             [AllowAnonymous]
             [ValidateAntiForgeryToken]
             public ActionResult Index(HomeModel model, string submitValue)
             { List<string[]> Products_Normal = new List<string[]>();
                 List<string[]> Products_Boronz = new List<string[]>();
                 List<string[]> Products_Silver = new List<string[]>();
                 List<string[]> Products_Gold = new List<string[]>();
                 using (EntitiesConnection EC = new EntitiesConnection())
                 {
                     var query_normal = (from c in EC.ProductImages where c.Product.P_ScoreCost <= 1000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(5);
                     foreach (var q in query_normal)
                     {
                         string[] product = new string[3];
                         product[0] = q.Product.P_ID.ToString();
                         product[1] = q.Product.P_Brand + "-" + q.Product.P_ModelName;
                         product[2]=string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.PI_Thumb));
                         Products_Normal.Add(product);
                     }

                     var query_boronz = (from c in EC.ProductImages where c.Product.P_ScoreCost > 1000 & c.Product.P_ScoreCost <= 5000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(5);
                     foreach (var q in query_boronz)
                     {
                         string[] product = new string[3];
                         product[0] = q.Product.P_ID.ToString();
                         product[1] = q.Product.P_Brand + "-" + q.Product.P_ModelName;
                         product[2] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.PI_Thumb));
                         Products_Boronz.Add(product);
                     }

                     var query_silver = (from c in EC.ProductImages where c.Product.P_ScoreCost > 5000 & c.Product.P_ScoreCost <= 15000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(5);
                     foreach (var q in query_silver)
                     {
                         string[] product = new string[3];
                         product[0] = q.Product.P_ID.ToString();
                         product[1] = q.Product.P_Brand + "-" + q.Product.P_ModelName;
                         product[2] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.PI_Thumb));
                         Products_Silver.Add(product);
                     }


                     var query_gold = (from c in EC.ProductImages where c.Product.P_ScoreCost > 15000 select c).OrderByDescending(c => c.Product.P_DateAdded).Take(5);
                     foreach (var q in query_gold)
                     {
                         string[] product = new string[3];
                         product[0] = q.Product.P_ID.ToString();
                         product[1] = q.Product.P_Brand + "-" + q.Product.P_ModelName;
                         product[2] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.PI_Thumb));
                         Products_Gold.Add(product);
                     }
                 }

                 ViewData["myNormalProducts"] = Products_Normal;
                 ViewData["myboronzProducts"] = Products_Boronz;
                 ViewData["mySilverProducts"] = Products_Silver;
                 ViewData["myGoldProducts"] = Products_Gold; 
                 ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
                 ViewData["StateName"] = AddressManagement.LoadState();
                 if (submitValue.Equals("ورود"))
                 {
                     if (string.IsNullOrEmpty(model.UserName))
                     {
                         ModelState.AddModelError("UserName", "");
                     }
                     if (string.IsNullOrEmpty(model.Password))
                     {
                         ModelState.AddModelError("Password", "");
                     }
                     if (ModelState.IsValid)
                     {
                         if (WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                         {
                     
                             return RedirectToAction("Index", "Home");
                         }
                         else
                         {
                             using (EntitiesConnection EC_DB = new EntitiesConnection())
                             {
                                 TempUserProfile tempuser = EC_DB.TempUserProfiles.FirstOrDefault(u => u.U_UserName.ToLower() == model.UserName.ToLower());
                                 // EC_DB.TempUserProfiles.f
                                 if (tempuser != null)
                                 {
                                     @ViewBag.MyErrorLogin = "نام کاربری به صورت موقت ثبت شده است. جهت تایید حساب کاربری لطفاً کد تایید را به سامانه 10006020 ارسال نمایید. ";
                                     return View(model);
                                 }
                             }
                             @ViewBag.MyErrorLogin = "نام کاربری اشتباه است.";
                             return View(model);
                         }
                     }
                     @ViewBag.MyErrorLogin = "لطفاً تمامی نکات را رعایت فرمایید.";
                     return View(model);
                 }
                 else if (submitValue.Equals("ثبت نام"))
                 {
                     if (string.IsNullOrEmpty(model.Register_UserName))
                     {
                         ModelState.AddModelError("Register_UserName", "");
                         @ViewBag.MyErrorNoUsername = "لطفاً شماره همراه را وارد فرمایید.";
                     }
                     else
                     {
                         if (model.Register_UserName.Length != 11 || !model.Register_UserName.StartsWith("0"))
                         {
                             @ViewBag.MyErrorNumberFormat = "فرمت شماره همراه نا معتبر است. همانند مثال وارد فرمایید.";
                         }
                     }
                     if (string.IsNullOrEmpty(model.Register_Password))
                     {
                         ModelState.AddModelError("Register_Password", "");
                         @ViewBag.MyErrorNoPassword = "لطفاً کلمه عبور را تعیین فرمایید.";
                     }
                     if (string.IsNullOrEmpty(model.FirstName))
                     {
                         ModelState.AddModelError("FirstName", "");
                         @ViewBag.MyErrorNoFirstName = "لطفاً نام را وارد فرمایید.";
                     }
                     if (string.IsNullOrEmpty(model.LastName))
                     {
                         ModelState.AddModelError("LastName", "");
                         @ViewBag.MyErrorNoLastName = "لطفاً نام خانوادگی را وارد فرمایید.";
                     }
                     if (string.IsNullOrEmpty(model.Gender))
                     {
                         ModelState.AddModelError("Gender", "");
                         @ViewBag.MyErrorGender = "لطفاً جنسیت را تعیین کنید.";
                     }
                     if (string.IsNullOrEmpty(model.State))
                     {
                         ModelState.AddModelError("State", "");
                         @ViewBag.MyErrorState = "لطفاً استان را تعیین کنید.";
                     }
                     if (string.IsNullOrEmpty(model.City))
                     {
                         ModelState.AddModelError("City", "");
                         @ViewBag.MyErrorCity = "لطفاً شهرستان را تعیین کنید.";
                     }
                     if (!string.IsNullOrEmpty(model.Gender))
                     {
                         if (model.Gender.Equals("انتخاب کنید"))
                         {
                             ModelState.AddModelError("Gender", "");
                             @ViewBag.MyErrorGender = "لطفاً جنسیت را تعیین کنید.";
                         }
                     }
                     if (!string.IsNullOrEmpty(model.State))
                     {
                         if (model.State.Equals("انتخاب کنید"))
                         {
                             ModelState.AddModelError("State", "");
                             @ViewBag.MyErrorState = "لطفاً استان را تعیین کنید.";
                         }
                     }
                     if (!string.IsNullOrEmpty(model.City))
                     {
                         if (model.City.Equals("انتخاب کنید"))
                         {
                             ModelState.AddModelError("City", "");
                             @ViewBag.MyErrorCity = "لطفاً شهرستان را تعیین کنید.";
                         }
                     }

                     if (ModelState.IsValid)
                     {
                         // Attempt to register the user
                    
                         using (EntitiesConnection EC_DB = new EntitiesConnection())
                         {
                             TempUserProfile tempuser = EC_DB.TempUserProfiles.FirstOrDefault(u => u.U_UserName.ToLower() == model.Register_UserName.ToLower());
                             // EC_DB.TempUserProfiles.f
                             if (tempuser == null)
                             {
                                 using (UsersContext db = new UsersContext())
                                 {
                                     UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.Register_UserName.ToLower());
                                     if (user == null)
                                     {
                                         string[] TempRegisterResult = new string[4];
                                         TempUserRegister TempRegister = new TempUserRegister();
                                         TempRegisterResult = TempRegister.Register(model);

                                         if (TempRegisterResult[3].Equals("OK"))
                                         {
                                             if (TempRegisterResult[0].Equals("CHECK_OK"))
                                             {
                                                 return View("../Account/TempAccount", model);
                                             }
                                             else
                                             {
                                                 if (TempRegisterResult[1].Equals("CDATA[BLACKLISTED_DESTINATION"))
                                                 {
                                                   //  TempData["SMSBlockDest_RandomCode"] = TempRegisterResult[2];
                                                     return View("../Account/SMSBlocked", model);
                                                 }
                                                 else
                                                 {
                                                   //  TempData["SMSInvalidDest_RandomCode"] = TempRegisterResult[2];
                                                     return View("../Account/SMSNotSend", model);
                                                 }

                                             }
                                         }
                                         else
                                         {
                                             @ViewBag.MyErrorRegister = "خطا در عملیات ثبت نام. لطفاً بعداً جهت ثبت نام اقدام فرمایید. در صورت حل نشدن اشکال با اپراتور تماس حاصل فرمایید.";
                                             return View(model);
                                         }
                                     }
                                     else
                                     {
                                         @ViewBag.MyErrorRegister = "نام کاربری قبلاً در سیستم ثبت شده است.";
                                         return View(model);
                                     }
                                 }
                             }
                             else
                             { 
                            
                                 string[] TempReRegisterResult = new string[4];
                                 TempUserRegister TempReRegister = new TempUserRegister();
                                 TempReRegisterResult = TempReRegister.ReRegister(tempuser, model);
                                 EC_DB.SaveChanges();
                          
                                 if (TempReRegisterResult[3].Equals("OK"))
                                 {

                                     if (tempuser.Try_Send_Code <= 3)
                                     {


                                         if (TempReRegisterResult[0].Equals("CHECK_OK"))
                                         {

                                             int? try_number = tempuser.Try_Send_Code;
                                             if (try_number == 2)
                                             {
                                                 @ViewBag.MyErrorRegister = "نام کاربری قبلاً در سیستم به صورت موقت ثبت شده است. کد تایید برای بار دوم برای شما ارسال شد. لطفاً کد تایید را برای سامانه 10006020 ارسال نمایید.";
                                             }
                                             else if (try_number == 3)
                                             {
                                                 @ViewBag.MyErrorRegister = "نام کاربری قبلاً در سیستم به صورت موقت ثبت شده است. کد تایید برای بار سوم و آخرین بار برای شما ارسال شد. لطفاً کد تایید را برای سامانه 10006020 ارسال نمایید.";
                                             }
                                             model.TrySendCode = try_number;
                                             return View("../Account/TempAccount", model);
                                         }
                                         else
                                         {
                                             if (TempReRegisterResult[1].Equals("CDATA[BLACKLISTED_DESTINATION"))
                                             {

                                                // TempData["SMSBlockDest_RandomCode"] = tempuser.U_RandomCode;
                                                 return View("../Account/SMSBlocked", model);
                                             }
                                             else
                                             {
                                               //  TempData["SMSInvalidDest_RandomCode"] = tempuser.U_RandomCode;
                                                 return View("../Account/SMSNotSend", model);
                                             }

                                         }
                                     }
                                     else
                                     {
                                        // TempData["SMSInvalidDest_RandomCode"] = tempuser.U_RandomCode; ;
                                         return View("../Account/SMSNotSend", model);
                                     }
                                 }
                             }
                         }

                     }

                     // If we got this far, something failed, redisplay form
                     @ViewBag.MyErrorRegister = "لطفاً تمامی نکات زیر را رعایت فرمایید.";
                     return View(model);

                 }
                 //else if (submitValue.Equals("ارسال"))
                 //{
                 //    if (string.IsNullOrEmpty(model.Q_name))
                 //    {
                 //        ModelState.AddModelError("Q_name", "");
                 //        @ViewBag.MyErrorNoQName = "لطفاً نام را وارد نمایید.";
                 //    }
                 //    if (string.IsNullOrEmpty(model.Q_Email))
                 //    {
                 //        ModelState.AddModelError("Q_Email", "");
                 //        @ViewBag.MyErrorNoQEmail = "جهت مکاتبات بیشتر لطفاً ایمیل را وارد نمایید.";
                 //    }
                 //    if (string.IsNullOrEmpty(model.Q_Message))
                 //    {
                 //        ModelState.AddModelError("Q_Message", "");
                 //        @ViewBag.MyErrorNoQMessage = "لطفاً متن پیغام را وارد نمایید.";
                 //    }
                 //    if (ModelState.IsValid)
                 //    {
                 //        using (EntitiesConnection EC_DB = new EntitiesConnection())
                 //        {
                 //            VisitorsMessage VM = new VisitorsMessage();
                 //            VM.Name = model.Q_name;
                 //            VM.Email = model.Q_Email;
                 //            VM.MessageText = model.Q_Message;
                 //            VM.MessageDate = DateTime.Now;
                 //            EC_DB.VisitorsMessages.Add(VM);
                 //            EC_DB.SaveChanges();
                 //            return View();

                 //        }

                 //    }
                 //    return View(model);
                 //}
                 //else if (submitValue.Equals("ثبت کد"))
                 //{
                 //    return RedirectToAction("ScoreIncrease", routeValues: new { CodeNumber = model.CodeNumber });
                 //    //if(string.IsNullOrEmpty(model.CodeNumber) ){
                 //    //    ModelState.AddModelError("CodeNumber", "");
                 //    //    @ViewBag.MyErrorNoCodeNumber = "لطفاً کد امتیاز را وارد نمایید.";
                 //    //}
                 //    //if (ModelState.IsValid)
                 //    //{
                 //    //    string result;
                 //    //    ScoreManagement SM = new ScoreManagement();
                 //    //    result=SM.NewScore(WebSecurity.CurrentUserId, model.CodeNumber);
                 //    //    if (result.Equals("OK")) {
                 //    //        @ViewBag.MyCodeNumberResult = "کد امتیاز با موفقیت ثبت شد.";
                 //    //        return RedirectToAction("ScoreIncrease");
                 //    //    }
                 //    //    else if(result.Equals("Code Used Before")){
                 //    //        @ViewBag.MyCodeNumberResult = "کد امتیاز قبلاً استفاده شده است.";
                 //    //        return View();
                 //    //    }
                 //    //    else {
                 //    //        @ViewBag.MyCodeNumberResult = "کد امتیاز نا معتبر است.";
                 //    //        return View();
                 //    //    }
                 //    //}
                 //    //else {
                 //    //    return View(model);
                 //    //}

            //}
                 else
                 {
                     return View(model);
                 }
             }
             */
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your app description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
    public class Customer
    {
        public string date_Year { get; set; }
        public string date_Month { get; set; }
        public string date_Day { get; set; }
        public string date_time { get; set; }
        public string name { get; set; }
        public byte[] thumbimg { get; set; }
    }
    public class CustomerRepository
    {
        public IEnumerable<Customer> ListCustomers()
        {
            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                var timelineQuery = (from c in EC_DB.ShopImages
                                     select new
                                     {
                                         date = c.Shop.DateJoined,
                                         name = c.Shop.ShopName,
                                         thumbimg = c.ShopThumbnail

                                     }).OrderByDescending(m => m.date);



                foreach (var q in timelineQuery)
                {
                    DateTime DT = q.date.Value;
                    PersianCalendar PC = new PersianCalendar();
                    int day = PC.GetDayOfMonth(DT);
                    int month = PC.GetMonth(DT);
                    int year = PC.GetYear(DT);
                    yield return new Customer
                    {
                        date_Year = year.ToString(),
                        date_Month = month.ToString(),
                        date_Day = day.ToString(),
                        name = q.name,
                        thumbimg = q.thumbimg,
                        date_time = q.date.Value.ToShortTimeString()
                    };
                }
            }
            //string customerFile = HostingEnvironment.MapPath("~/App_Data/Customers.csv");

            //foreach (string line in System.IO.File.ReadAllLines(customerFile))
            //{
            //    var parts = line.Split('|');
            //    yield return new Customer
            //    {
            //        name = parts[0],
            //        date=DateTime.Now
            //    };
            //}

        }
    }
}
