using GPLUSPLUS.Filters;
using GPLUSPLUS.Models;
using GPLUSPLUS.Models.Bussiness;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using CaptchaMvc.HtmlHelpers;
using System.Globalization;



namespace GPLUSPLUS.Controllers
{
    [Authorize]
    public class AgentsController : Controller
    {
        [AllowAnonymous]
        public JsonResult GetAgentDesc(string id)
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
                    return Json(result, JsonRequestBehavior.AllowGet);
                    
                }
                else
                    return Json(id, JsonRequestBehavior.AllowGet);
            }

        }


        [AllowAnonymous]
        public JsonResult GetCity(string id)
        {

            return Json(new SelectList(AddressManagement.GetCity(id), "Value", "Text"));
        }
        //
        // GET: /Agents/





        //[AllowAnonymous]
        //public PartialViewResult ShopTypesList(string myCity)
        //{
        //    HomeModel model = new HomeModel();

        //    using (EntitiesConnection EC = new EntitiesConnection())
        //    {


        //        var query = from c in EC.Shops where (c.ShopRegisterStatus == true & c.ShopCity == myCity) || (c.ShopRegisterStatus == false & c.ShopCity == myCity & EntityFunctions.DiffDays(c.DateJoined, DateTime.Now) <= 7) group c by c.ShopType into newgroup select newgroup;

        //        foreach (var q in query)
        //        {
        //            string[] shoptype = new string[3];
        //            shoptype[0] = q.Key.Id.ToString();
        //            shoptype[1] = q.Key.Name;
        //            shoptype[2] = (q.Key.ImageOf != null) ? string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.Key.ImageOf)) : string.Format("data:image/png;base64,{0}", "");
        //            model.myShopTypes.Add(shoptype);
        //        }

        //        var imagequery = from c in EC.ShopImages where c.Shop.ShopRegisterStatus == true & c.Shop.ShopCity == myCity select c;
        //        foreach (var q in imagequery)
        //        {
        //            string[] image = new string[3];
        //            image[0] = q.Shop.Id.ToString();
        //            image[1] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.ShopImage1));
        //            image[2] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.ShopThumbnail));
        //            model.myShops.Add(image);
        //        }
        //        AgentManagement AM = new AgentManagement();
        //        var tempShops = from c in EC.ShopImages where (c.Shop.ShopRegisterStatus == false & c.Shop.ShopCity == myCity & EntityFunctions.DiffDays(c.Shop.DateJoined, DateTime.Now) <= 2) select c;
        //        foreach (var q in tempShops)
        //        {

        //            string[] image = new string[3];
        //            image[0] = q.Shop.Id.ToString();
        //            image[1] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.ShopImage1));
        //            image[2] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.ShopThumbnail));
        //            model.myShops.Add(image);
        //        }

        //    }
        //    @ViewBag.myShopCity = myCity;
        //    return PartialView("_ShopTypesList", model);

        //}

        //[AllowAnonymous]
        //public PartialViewResult AgentsList(string Category, string mycity)
        //{
        //    HomeModel model = new HomeModel();
        //    if (!string.IsNullOrEmpty(Category))
        //    {


        //        using (EntitiesConnection EC = new EntitiesConnection())
        //        {
        //            int cateId = int.Parse(Category);

        //            var query = from c in EC.ShopImages where (c.Shop.ShopType.Id == cateId & c.Shop.ShopRegisterStatus == true & c.Shop.ShopCity == mycity) || (c.Shop.ShopType.Id == cateId & c.Shop.ShopRegisterStatus == false & c.Shop.ShopCity == mycity & EntityFunctions.DiffDays(c.Shop.DateJoined, DateTime.Now) <= 2) select c;
        //            foreach (var q in query)
        //            {
        //                string[] image = new string[3];
        //                image[0] = q.Shop.Id.ToString();
        //                image[1] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.ShopImage1));
        //                image[2] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.ShopThumbnail));
        //                model.myShops.Add(image);

        //            }

        //        }

        //    }
        //    //HomeModel model = new HomeModel();

        //    //using (EntitiesConnection EC = new EntitiesConnection())
        //    //{

        //    //    string[] shoptype = new string[2];
        //    //    var query = from c in EC.ShopTypes select c;
        //    //    foreach (var q in query)
        //    //    {
        //    //        shoptype[0] = q.Name;
        //    //        shoptype[1] = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(q.ImageOf));
        //    //        model.myShopTypes.Add(shoptype);
        //    //        int i;
        //    //    }


        //    return PartialView("_AgentsListPartial", model);


        //}



        public ActionResult AgentRegister()
        {
            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                Shop temp_Shop = EC_DB.Shops.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
                if (temp_Shop != null)
                {
                    return View("HaveRegisterd");
                }
            }
            ViewData["StateName"] = AddressManagement.LoadState();

            ViewData["ShopType"] = ShopManagement.LoadShopType();
            AgentRegister model = new AgentRegister();
            using (UsersContext db = new UsersContext())
            {

                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                if (user != null)
                {

                    model.FirstName = user.U_FirstName;
                    model.LastName = user.U_LastName;
                    ViewData["CityName"] = AddressManagement.GetCity(user.U_State);
                    model.ShopCity = user.U_City;
                    model.ShopState = user.U_State;
                    model.Mobile = user.UserName;
                    model.AcceptGPLUSCode = true;

                }

            }

            return View(model);
        }





        public PartialViewResult AgentRegisterPoster(AgentRegister model)
        {

            string shopQ = "";
            if (model.ShopQuotes == "FirstRadio")
                shopQ = "خرید کنید امتیاز بگیرید";
            else if (model.ShopQuotes == "SecondRadio")
                shopQ = "به ازای " + model.ShopQuotes_firstbox + " تومان خرید " + model.ShopQuotes_secondbox + " امتیاز بگیرید";
            else
                shopQ = model.ShopOptionalQoutes;

            model.ShopQuotesfinal = shopQ;
            AgentManagement AM = new AgentManagement();
            //byte[] ImageByte = AM.AgentPoster(model.ShopName, model.ShopQuotesfinal);
            byte[] ImageByte = AM.DrawText(model.ShopName, model.ShopQuotesfinal);

            //string imageBase64Data = Convert.ToBase64String(ImageByte);
            //string temp_imgURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            model.ShopPoster = ImageByte;

            return PartialView("_AgentRegisterPoster", model);
        }

        //public PartialViewResult AgentRegisterContract(AgentRegister model)
        //{


        //    return PartialView("_AgentRegisterContract", model);
        //}

        public PartialViewResult AgentRegisterSubmit(AgentRegister model)
        {

            EntitiesConnection EC_DB = new EntitiesConnection();

            Shop temp_Shop = EC_DB.Shops.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
            if (temp_Shop == null)
            {
                Shop new_Shop = new Shop();
                new_Shop.Id = WebSecurity.CurrentUserId;
                new_Shop.FirstName = model.FirstName;
                new_Shop.LastName = model.LastName;
                new_Shop.ShopName = model.ShopName;
                new_Shop.Mobile = model.Mobile;
                new_Shop.ShopState = model.ShopState;
                new_Shop.ShopCity = model.ShopCity;
                new_Shop.ShopAddress = model.ShopAddress;
                new_Shop.ShopDescription = model.ShopDescription;
                new_Shop.ShopTell = model.ShopTell;
                new_Shop.ShopFax = model.ShopFax;
                new_Shop.ShopEmail = model.ShopEmail;
                new_Shop.AcceptCode = model.AcceptGPLUSCode;
                new_Shop.AcceptBanner = model.AcceptBannerDesign;
                new_Shop.AcceptSite = model.AcceptWebDesign;
                new_Shop.ShopRegisterStatus = false;
                new_Shop.PosterQuotes = model.ShopQuotesfinal;
                new_Shop.DateJoined = DateTime.Now;
                if (model.AcceptInstagram)
                {
                    new_Shop.InstagramAddress = model.InstagramAddress;
                }
                if (model.AcceptPersonalSite)
                {
                    new_Shop.PersonalSiteAddress = model.PersonalSiteAddress;
                }


                ShopType SType = EC_DB.ShopTypes.FirstOrDefault(u => u.Name == model.ShopType);
                new_Shop.F_ShopType_ID = SType.Id;
                EC_DB.Shops.Add(new_Shop);

                EC_DB.SaveChanges();

                ShopImage new_shopImage = new ShopImage();
                new_shopImage.F_Shop_ID = WebSecurity.CurrentUserId;
                byte[] imageBytes = model.ShopPoster;
                new_shopImage.ShopImage1 = imageBytes;
                new_shopImage.ShopThumbnail = (Byte[])new ImageConverter().ConvertTo(ProductManagement.CreateThumbnail(imageBytes, 229, 179, false), typeof(Byte[]));

                EC_DB.ShopImages.Add(new_shopImage);


                EC_DB.SaveChanges();

                string IP = "http://193.104.22.14:2055/CPSMSService/Access";
                Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
                string SMS_Message = "مرکز شما به صورت موقت به عنوان مرکز طرف قرارداد GTOP ثبت شد. منتظر تماس کارشناسان ما جهت تکمیل عملیات ثبت و تنظیم قرارداد باشید." + " \n www.gplusplus.ir";
                sms_Single.SendSMS_Single(SMS_Message, WebSecurity.CurrentUserName, "10006020", "ATSIGNCO9", "m@hfye4@5", IP, "ATSIGNCO", false);
                //string Admin_Aware_msg = "مرکز طرف قرار داد جدید در تاریخ " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " ثبت نام نمود. \nشماره قرارداد GPLUS: " + WebSecurity.CurrentUserId;
                //sms_Single.SendSMS_Single(Admin_Aware_msg, "09141406020", "10006020", "ATSIGNCO9", "m@hfye4@5", IP, "ATSIGNCO", false);
                return PartialView("_AgentRegisterSubmitOnSuccess", model);
            }

            return PartialView("_AgentRegisterSubmitOnFail", model);
        }




        [AllowAnonymous]
        public PartialViewResult AcceptCode()
        {

            return PartialView("_AcceptCodePartial");
        }

        [AllowAnonymous]
        public PartialViewResult AcceptBanner()
        {

            return PartialView("_AcceptBannerPartial");
        }

        [AllowAnonymous]
        public PartialViewResult AcceptSite()
        {

            return PartialView("_AcceptSitePartial");
        }
        [AllowAnonymous]
        public PartialViewResult AcceptInstagram()
        {

            return PartialView("_AcceptInstagramPartial");
        }
        [AllowAnonymous]
        public PartialViewResult AcceptInstagramAddress()
        {

            return PartialView("_AcceptInstagramAddressPartial");
        }
        [AllowAnonymous]
        public PartialViewResult AcceptPersonalSite()
        {

            return PartialView("_AcceptPersonalSitePartial");
        }
        [AllowAnonymous]
        public PartialViewResult AcceptPersonalSiteAddress()
        {

            return PartialView("_AcceptPersonalSiteAddressPartial");
        }




        //[Authorize]
        //public ActionResult LocalAdvertiseRequest()
        //{  

        //    using (EntitiesConnection EC_DB = new EntitiesConnection())
        //    {
        //           PersianCalendar PC = new PersianCalendar();
        //                int currentmonthinPC = PC.GetMonth(DateTime.Now);
        //                int currentyerinPC = PC.GetYear(DateTime.Now);
        //                 DateTime Translated_LastOfMonth;
        //                if (currentmonthinPC <= 6)
        //                {
        //                     Translated_LastOfMonth =PC.ToDateTime(currentyerinPC, currentmonthinPC, 31,23,59,59,0);
        //                }
        //                else {
        //                     Translated_LastOfMonth = new DateTime(currentyerinPC, currentmonthinPC, 30, 23, 59, 59, 0);
        //                }
        //                if (User.Identity.IsAuthenticated)
        //                {

        //                    var ShopReq = from c in EC_DB.ShopAdvertisments where c.ShopRequestDate.Value.Year == Translated_LastOfMonth.Year & c.ShopRequestDate.Value.Month == Translated_LastOfMonth.Month & c.ShopId == WebSecurity.CurrentUserId select c;

        //                    if (ShopReq.Any())
        //                    {

        //                        ViewBag.isShopReq = "true";
        //                        return View("AddRequested");
        //                    }
        //                }
        //                Shop tempshop = EC_DB.Shops.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
        //                if (tempshop == null)
        //                {
        //                    return View("NoContract");

        //                }




        //        ShopAdvertisment temp_SA = EC_DB.ShopAdvertisments.Where(u => u.ShopId == WebSecurity.CurrentUserId).OrderByDescending(u => u.ShopRequestDate).FirstOrDefault();
        //        if (temp_SA != null)
        //        {
        //            AgnetAdvetise model = new AgnetAdvetise();
        //            model.ShopAddress = temp_SA.ShopAddress;
        //            model.ShopDescription = temp_SA.ShopDescription;
        //            model.ShopName = temp_SA.ShopName;
        //            model.ShopTell = temp_SA.ShopTell;
        //            return View(model);
        //        }
        //    }
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult LocalAdvertiseRequest(AgnetAdvetise model)
        //{
        //    if (string.IsNullOrEmpty(model.ShopName))
        //    {
        //        ModelState.AddModelError("ShopName", "");
        //        @ViewBag.MyErrorNoQName = "لطفاً نام را وارد نمایید.";
        //    }
        //    if (string.IsNullOrEmpty(model.ShopDescription))
        //    {
        //        ModelState.AddModelError("ShopDescription", "");
        //        @ViewBag.MyErrorNoQEmail = "لطفاً زمینه فعالیتتان را  وارد نمایید.";
        //    }
        //    if (string.IsNullOrEmpty(model.ShopTell))
        //    {
        //        ModelState.AddModelError("ShopTell", "");
        //        @ViewBag.MyErrorNoQMessage = "لطفاً شماره تماس را وارد نمایید";
        //    }
        //    if (string.IsNullOrEmpty(model.ShopAddress))
        //    {
        //        ModelState.AddModelError("ShopAddress", "");
        //        @ViewBag.MyErrorNoQMessage = "لطفاً آدرس مرکز را وارد نمایید";
        //    }
        //    if (ModelState.IsValid)
        //    {


        //            using (EntitiesConnection EC_DB = new EntitiesConnection())
        //            {
        //                PersianCalendar PC = new PersianCalendar();
        //                int currentmonthinPC = PC.GetMonth(DateTime.Now);
        //                int currentyerinPC = PC.GetYear(DateTime.Now);
        //                 DateTime Translated_LastOfMonth;
        //                if (currentmonthinPC <= 6)
        //                {
        //                     Translated_LastOfMonth =PC.ToDateTime(currentyerinPC, currentmonthinPC, 31,23,59,59,0);
        //                }
        //                else {
        //                     Translated_LastOfMonth = new DateTime(currentyerinPC, currentmonthinPC, 30, 23, 59, 59, 0);
        //                }
        //                if (User.Identity.IsAuthenticated)
        //                {
                         
        //                    var ShopReq = from c in EC_DB.ShopAdvertisments where c.ShopRequestDate.Value.Year ==Translated_LastOfMonth.Year & c.ShopRequestDate.Value.Month == Translated_LastOfMonth.Month & c.ShopId == WebSecurity.CurrentUserId select c;

        //                    if (ShopReq.Any())
        //                    {

        //                        ViewBag.isShopReq = "true";
        //                        return View("AddRequested");
        //                    }

        //                }



        //                ShopAdvertisment SA = new ShopAdvertisment();
        //                SA.ShopId = WebSecurity.CurrentUserId;
        //                SA.ShopName = model.ShopName;
        //                SA.ShopDescription = model.ShopDescription;
        //                SA.ShopTell = model.ShopTell;
        //                SA.ShopAddress = model.ShopAddress;
        //                SA.ShopRequestStatus = false;
        //                SA.ShopRequestDate = DateTime.Now;

        //                Shop tempshop = EC_DB.Shops.FirstOrDefault(u => u.Id == WebSecurity.CurrentUserId);
        //                if (tempshop != null)
        //                {
        //                    if (tempshop.AcceptCode == true)
        //                    { SA.ShopAD_FirstClass = true; }
        //                    else
        //                    { SA.ShopAD_FirstClass = false; }

        //                }
        //                else {
        //                    return View("NoContract");
                        
        //                }
                           

        //                EC_DB.ShopAdvertisments.Add(SA);
        //                EC_DB.SaveChanges();
        //            }
        //            return RedirectToAction("myLocalAdvertise");


            

        //    }
        //    return View(model);

        //}

    

        //[AllowAnonymous]
        //public ActionResult LocalAdvertise() {
        //    AgnetAdvetise model = new AgnetAdvetise();

        //    DateTime DT=new DateTime(2015,6,22);
        //PersianCalendar PC=new PersianCalendar();
        //    int day=PC.GetDayOfMonth(DT);
        //      int month=PC.GetMonth(DT);
        //      int year=PC.GetYear(DT);
            
        //    string monthName;
        //    switch (month){

        //        case 1:
        //            monthName="فروردین";
        //            break;
        //              case 2:
        //            monthName="اردیبهشت";
        //            break;
        //              case 3:
        //            monthName="خرداد";
        //            break;
        //              case 4:
        //            monthName="تیر";
        //            break;
        //              case 5:
        //            monthName="مرداد";
        //            break;
        //              case 6:
        //            monthName="شهریور";
        //            break;
        //              case 7:
        //            monthName="مهر";
        //            break;
        //              case 8:
        //            monthName="آبان";
        //            break;
        //              case 9:
        //            monthName="اذر";
        //            break;
        //              case 10:
        //            monthName="دی";
        //            break;
        //              case 11:
        //            monthName="بهمن";
        //            break;
        //         default:
        //            monthName="اسفند";
        //            break;    
        //    }

        //    ViewBag.DoreAgahi =  monthName + " ماه " + year;


        //    int currentmonthinPC = PC.GetMonth(DateTime.Now);
        //    int currentyerinPC = PC.GetYear(DateTime.Now);
        //    DateTime Translated_LastOfMonth;
        //    if (currentmonthinPC <= 6)
        //    {
        //        Translated_LastOfMonth = PC.ToDateTime(currentyerinPC, currentmonthinPC, 31, 23, 59, 59, 0);
        //    }
        //    else
        //    {
        //        Translated_LastOfMonth = new DateTime(currentyerinPC, currentmonthinPC, 30, 23, 59, 59, 0);
        //    }

            
        //    using (EntitiesConnection EC_DB = new EntitiesConnection())
        //    {
                
        //        if (User.Identity.IsAuthenticated)
        //        {

        //            var ShopReq = from c in EC_DB.ShopAdvertisments where c.ShopRequestDate.Value.Year == Translated_LastOfMonth.Year & c.ShopRequestDate.Value.Month == Translated_LastOfMonth.Month & c.ShopId == WebSecurity.CurrentUserId select c;

        //            if (ShopReq.Any())
        //            {

        //                ViewBag.isShopReq = "true";
        //            }
        //            else
        //            {

        //                ViewBag.isShopReq = "false";
        //            }

        //        }
        //        else{
        //            ViewBag.isShopReq = "false";
        //        }


        //        var query = from c in EC_DB.ShopAdvertisments where c.ShopRequestDate.Value.Year == Translated_LastOfMonth.Year & c.ShopRequestDate.Value.Month == Translated_LastOfMonth.Month & c.ShopAD_FirstClass == true select c;
        //        foreach (var q in query)
        //        {
        //            string[] Info = new string[5];
        //            Info[0] = q.Id.ToString();
        //            Info[1] = q.ShopName;
        //            Info[2] = q.ShopDescription;
        //            Info[3] = q.ShopAddress;
        //            Info[4] = q.ShopTell;
        //            model.ShopInfoFirstClass.Add(Info);
        //        }

        //        var querySecond = from c in EC_DB.ShopAdvertisments where c.ShopRequestDate.Value.Year == Translated_LastOfMonth.Year & c.ShopRequestDate.Value.Month == Translated_LastOfMonth.Month & c.ShopAD_FirstClass == false select c;
        //        foreach (var q in querySecond)
        //        {
        //            string[] Info = new string[5];
        //            Info[0] = q.Id.ToString();
        //            Info[1] = q.ShopName;
        //            Info[2] = q.ShopDescription;
        //            Info[3] = q.ShopAddress;
        //            Info[4] = q.ShopTell;
        //            model.ShopInfoSecondClass.Add(Info);
        //        }

        //    }

        //    return View(model);
        //}


    }
}
