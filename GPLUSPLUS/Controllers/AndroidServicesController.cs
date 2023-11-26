using GPLUSPLUS.Filters;
using GPLUSPLUS.Models;
using GPLUSPLUS.Models.Bussiness;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;
using GPLUSPLUS.Models;

namespace GPLUSPLUS.Controllers
{
    [Authorize]
    public class AndroidServicesController : ApiController
    {
        #region Accounting
        [HttpPost]
        [AllowAnonymous]
        public string AndroidRegister(string Register_UserName, string Register_Password, string FirstName, string LastName, string Gender, string State, string City, string JobTitle, string JobField)
        {
            //
            RegisterModel model = new RegisterModel();
            model.City = City; model.FirstName = FirstName; model.Gender = Gender; model.LastName = LastName; model.Register_Password = Register_Password; model.Register_UserName = Register_UserName; model.State = State; model.JobField = JobField; model.JobTitle = JobTitle;
            if (string.IsNullOrEmpty(model.Register_UserName))
            {
                if (model.Register_UserName.Length != 11 || !model.Register_UserName.StartsWith("0"))
                {
                    return AndroidJson.GenerateJsonResponse("NOK", "فرمت شماره همراه نا معتبر است.");
                }
            }
            if (ModelState.IsValid)
            {
                using (EntitiesConnection EC_DB = new EntitiesConnection())
                {
                    TempUserProfile tempuser = EC_DB.TempUserProfiles.FirstOrDefault(u => u.U_UserName.ToLower() == model.Register_UserName.ToLower());
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
                                        return AndroidJson.GenerateJsonResponse("OK", "اطلاعات شما به صورت موقت در سیستم ثبت گردید. جهت تکمیل فرایند ثبت نام کد چهار رقمی ارسال شده به شماره همراهتان را به سامانه 10006020 ارسال نمایید");
                                    }
                                    else
                                    {
                                        if (TempRegisterResult[1].Equals("CDATA[BLACKLISTED_DESTINATION"))
                                        {
                                            return AndroidJson.GenerateJsonResponse("NOK", "اطلاعات شما به صورت موقت در سیستم ثبت گردید. متاسفانه ما قادر به ارسال کد تایید برای شماره همراه مورد نظرتان نشدیم. لطفاً جهت تکمیل ثبت نام کد تایید چهار رقمی  زیر را به سامانه 10006020 ارسال نمایید" + "\n" + "کد چهار رقمی : " + model.RandomCode);
                                        }
                                        else
                                        {
                                            return AndroidJson.GenerateJsonResponse("OK", "اطلاعات شما به صورت موقت در سیستم ثبت گردید. جهت تکمیل فرایند ثبت نام کد چهار رقمی ارسال شده به شماره همراهتان را به سامانه 10006020 ارسال نمایید");
                                        }

                                    }
                                }
                                else
                                    return AndroidJson.GenerateJsonResponse("NOK", "خطا در عملیات ثبت نام. لطفاً بعداً جهت ثبت نام اقدام فرمایید. در صورت حل نشدن اشکال با اپراتور تماس حاصل فرمایید.");
                            }
                            else
                                return AndroidJson.GenerateJsonResponse("Duplicate", "نام کاربری قبلاً در سیستم ثبت شده است.");
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
                                        return AndroidJson.GenerateJsonResponse("NOK", "نام کاربری قبلاً در سیستم به صورت موقت ثبت شده است. کد تایید برای بار دوم برای شما ارسال شد. لطفاً کد تایید را برای سامانه 10006020 ارسال نمایید.");
                                    else if (try_number == 3)
                                        return AndroidJson.GenerateJsonResponse("NOK", "نام کاربری قبلاً در سیستم به صورت موقت ثبت شده است. کد تایید برای بار سوم و آخرین بار برای شما ارسال شد. لطفاً کد تایید را برای سامانه 10006020 ارسال نمایید.");
                                    return AndroidJson.GenerateJsonResponse("NOK", "اطلاعات شما به صورت موقت در سیستم ثبت گردید. جهت تکمیل فرایند ثبت نام کد چهار رقمی ارسال شده به شماره همراهتان را به سامانه 10006020 ارسال نمایید");
                                }
                                else
                                {
                                    if (TempReRegisterResult[1].Equals("CDATA[BLACKLISTED_DESTINATION"))
                                        return AndroidJson.GenerateJsonResponse("NOK", "اطلاعات شما به صورت موقت در سیستم ثبت گردید. متاسفانه ما قادر به ارسال کد تایید برای شماره همراه مورد نظرتان نشدیم. لطفاً جهت تکمیل ثبت نام کد تایید چهار رقمی  زیر را به سامانه 10006020 ارسال نمایید" + "\n" + "کد چهار رقمی : " + model.RandomCode);
                                    else
                                        return AndroidJson.GenerateJsonResponse("OK", " *اطلاعات شما به صورت موقت در سیستم ثبت گردید. متاسفانه ما قادر با ارسال کد تایید برای شماره همراه مورد نظرتان نشدیم. لطفاً جهت تکمیل ثبت نام کد تایید چهار رقمی  زیر را به سامانه 10006020 ارسال نمایید" + "\n" + "کد چهار رقمی : " + model.RandomCode);
                                }
                            }
                            else
                                return AndroidJson.GenerateJsonResponse("OK", " *اطلاعات شما به صورت موقت در سیستم ثبت گردید. متاسفانه ما قادر با ارسال کد تایید برای شماره همراه مورد نظرتان نشدیم. لطفاً جهت تکمیل ثبت نام کد تایید چهار رقمی  زیر را به سامانه 10006020 ارسال نمایید" + "\n" + "کد چهار رقمی : " + model.RandomCode);
                        }
                    }
                }
            }
            return AndroidJson.GenerateJsonResponse("OK", "لطفاً تمامی نکات را رعایت فرمایید.");
        }

        [HttpPost]
        [AllowAnonymous]
        public string AndroidLogin(string Tell, string Password)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(Tell, Password, persistCookie: false))
                {
                    using (EntitiesConnection EC_DB = new EntitiesConnection())
                    {
                        TempUserProfile tempuser = EC_DB.TempUserProfiles.FirstOrDefault(u => u.U_UserName == Tell);
                        if (tempuser != null)
                        {
                            return AndroidJson.GenerateJsonResponse("NOK", "نام کاربری به صورت موقت ثبت شده است. جهت تایید حساب کاربری لطفاً کد تایید را به سامانه 10006020 ارسال نمایید. ");
                        }
                        return AndroidJson.GenerateJsonResponse("OK", "شما با موفقیت وارد سیستم شدید");
                    }
                }
                return AndroidJson.GenerateJsonResponse("NOK", "نام کاربری یا کلمه عبور اشتباه است.");
            }
            return AndroidJson.GenerateJsonResponse("NOK", "لطفاً تمامی نکات را رعایت فرمایید.");
        }

        [HttpPost]
        public string AndroidChangePassword(string OldPassword, string NewPassword)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, OldPassword, NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return AndroidJson.GenerateJsonResponse("OK", "رمز عبور شما با موفقیت تغییر یافت");
                    }
                    else
                    {
                        return AndroidJson.GenerateJsonResponse("NOK", "کلمه عبور وارد شده صحیح نیست و یا اینکه کلمه عبور جدید نامعتبر است.");
                    }
                }
            }
            return AndroidJson.GenerateJsonResponse("NOK", "خطا در انجام فرآیند احراز هویت. لطفا مجددا تلاش کنید");
        }

        [HttpPost]
        [AllowAnonymous]
        public string AndroidForgetPassword(string Tell)
        {
            using (UsersContext db = new UsersContext())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == Tell);
                if (user != null)
                {
                    var token = WebSecurity.GeneratePasswordResetToken(user.UserName);
                    string NewPass = Tools.GeneratePassWord(8);
                    WebSecurity.ResetPassword(token, NewPass);
                    string IP = "http://193.104.22.14:2055/CPSMSService/Access";
                    Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
                    string SMS_Message = "رمز عبور جدید شما : \n" + NewPass + "\n" + "از طرف مجموعه GTOP";
                    sms_Single.SendSMS_Single(SMS_Message, Tell, "10006020", "ATSIGNCO9", "m@hfye4@5", IP, "ATSIGNCO", false);
                    return AndroidJson.GenerateJsonResponse("OK", "رمز عبور جدید به شماره تماس شما ارسال خواهد شد");
                }
                else
                    return AndroidJson.GenerateJsonResponse("NOK", "فرآیند بازیابی رمز عبور با خطا مواجه شده. لطفا مجددا تلاش کنید");
            }
        }

        [HttpPost]
        public string AndroidLogOut()
        {
            try
            { WebSecurity.Logout(); return AndroidJson.GenerateJsonResponse("OK", "شما با موفقیت از حساب کاربری فعلی خارج شدید"); }
            catch { return AndroidJson.GenerateJsonResponse("NOK", "خطا در پردازش درخواست"); }
        }

        #endregion


        #region Services

        [HttpPost]
        [AllowAnonymous]
        public string AndroidListStates()
        {
            var States = AddressManagement.LoadState();
            dynamic collectionWrapper = new { Root = States };
            return Newtonsoft.Json.JsonConvert.SerializeObject(collectionWrapper);
        }

        [HttpPost]
        [AllowAnonymous]
        public string AndroidListCitys(string StateName)
        {
            var Citys = AddressManagement.GetCity(StateName);
            dynamic collectionWrapper = new { Root = Citys };
            return Newtonsoft.Json.JsonConvert.SerializeObject(collectionWrapper);
        }

        [HttpPost]
        public string AndroidEnterScoreCode(string ScoreCode)
        {
            ScoreManagement SM = new ScoreManagement();
            string scale = SM.NewScore(Convert.ToInt32(Membership.GetUser().ProviderUserKey), ScoreCode);
            if (scale == "Code Used Before")
                return AndroidJson.GenerateJsonResponse("NOK", "کد مورد نظر قبلا استفاده شده است");
            else if (scale == "Code Not Found")
                return AndroidJson.GenerateJsonResponse("NOK", "کد مورد نظر در سیستم وجود ندارد");
            else if (scale == "Free Code Used Befor")
                return AndroidJson.GenerateJsonResponse("NOK", "کد امتیاز رایگان قبلا استفاده شده");
            else if (scale == "User Blocked BrutForced Score")
                return AndroidJson.GenerateJsonResponse("NOK", "خطای امنیتی");
            else
                return AndroidJson.GenerateJsonResponse("OK", "کد امتیازی شما با موفقیت ثبت شد . جمع امتیاز شما : " + scale);
        }

        [HttpPost]
        public string AndroidMainPage()
        {
            ScoreManagement SM = new ScoreManagement();
            AndroidMainJsonModel Result = new AndroidMainJsonModel();
            Result.TotalRialiScore = SM.TotalUserScore(Convert.ToInt32(Membership.GetUser().ProviderUserKey)).ToString();
            Result.TotalGhoreKeshiScore = (SM.TotalUserLotteryScore(Convert.ToInt32(Membership.GetUser().ProviderUserKey)) ?? default(int)).ToString();
            var s1 = new SliderJsonModel(); s1.ImgUrl = "http://emtiaz-gtopmarketing.3mill.ir/Content/HomeContentUnAuthNews/img/1.jpg"; s1.Title = "GTOPCo ...";
            var s2 = new SliderJsonModel(); s2.ImgUrl = "http://emtiaz-gtopmarketing.3mill.ir/Content/HomeContentUnAuthNews/img/2.jpg"; s2.Title = "GTOPCo ...";
            var s3 = new SliderJsonModel(); s3.ImgUrl = "http://emtiaz-gtopmarketing.3mill.ir/Content/HomeContentUnAuthNews/img/3.jpg"; s3.Title = "GTOPCo ...";
            var s4 = new SliderJsonModel(); s4.ImgUrl = "http://emtiaz-gtopmarketing.3mill.ir/Content/HomeContentUnAuthNews/img/4.jpg"; s4.Title = "GTOPCo ...";
            Result.Slider.Add(s1); Result.Slider.Add(s2); Result.Slider.Add(s3); Result.Slider.Add(s4);
            ProductManagement PROD = new ProductManagement();
            Result.PishnehadHa = PROD.AndroidPishnehadHa();
            dynamic collectionWrapper = new { Root = new List<AndroidMainJsonModel> { Result } };
            return Newtonsoft.Json.JsonConvert.SerializeObject(collectionWrapper);
        }

        [AllowAnonymous]
        [HttpPost]
        public string AndroidProductDetail(string ProductID)
        {
            int myid = int.Parse(ProductID);
            AndroidProductDetailModel Result = new AndroidProductDetailModel();
            using (EntitiesConnection EC = new EntitiesConnection())
            {
                Product temp_p = EC.Products.FirstOrDefault(u => u.P_ID == myid);
                if (temp_p != null)
                {
                    Result.ProductName = temp_p.P_Name;
                    Result.ProductBrand = temp_p.P_Brand;
                    Result.ProductModelName = temp_p.P_ModelName;
                    Result.ProductDescription = temp_p.P_Description;
                    Result.ProductScoreCost = temp_p.P_ScoreCost.ToString();
                    var query = from c in EC.ProductImages where c.F_P_ID == temp_p.P_ID select c;
                    foreach (var q in query)
                        Result.ProductImges.Add("http://emtiaz-gtopmarketing.3mill.ir/Home/GetFile?FileName=" + q.PI_ID);
                }
                dynamic collectionWrapper = new { Root = new List<AndroidProductDetailModel> { Result } };
                return Newtonsoft.Json.JsonConvert.SerializeObject(collectionWrapper);
            }

        }
        #endregion

    }
}
