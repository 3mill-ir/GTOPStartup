using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using GPLUSPLUS.Filters;
using GPLUSPLUS.Models;
using GPLUSPLUS.Models.Bussiness;

namespace GPLUSPLUS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        string Message_Signature = "/nاز طرف مجموعه GTOP"; 



        [AllowAnonymous]
        public string ForgetPasswordTokenReciver(string SecureString, string ID, string Token, string NewPassword, string operationID, string inboxID)
        {
            int temp_operationID;
            if (!int.TryParse(operationID, out temp_operationID))
            {
                return "NOK";
            }
            int temp_inboxID;
            if (!int.TryParse(inboxID, out temp_inboxID))
            {
                return "NOK";
            }
            if (SecureString == "DORSA")
            {
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == ID.ToLower());
                    if (user != null)
                    {
                        var any = (from j in db.webpages_Memberships
                                   where (j.UserId == user.UserId)
                   && (j.PasswordVerificationToken.Substring(0, 5).ToLower() == Token.ToLower())
                                   //&& (j.PasswordVerificationTokenExpirationDate < DateTime.Now)
                                   select j.PasswordVerificationToken);
                        if (any.Any())
                        {
                            bool st = WebSecurity.ResetPassword(any.FirstOrDefault(), NewPassword);
                            if (st)
                            {
                                SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                                string Reply_MSG = "تبریک می گوییم تغییر رمز عبور شما شما در دهکده GTOP با موفقیت انجام گرفت.\n نام کاربری: " + ID + "\nکلمه عبور: " + NewPassword + Message_Signature;
                                BLL.Reply(Reply_MSG, ID, temp_operationID, temp_inboxID);
                                return "ok";
                            }

                            return "zok";
                        }
                        else {
                            SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                            string Reply_MSG = "درخواستی در سیستم برای تغییر کلمه عبور شما ثبت نشده است." + Message_Signature;
                            BLL.Reply(Reply_MSG, ID, temp_operationID, temp_inboxID);
                        }
                        return "noke";
                    }
                    else {
                        SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                        string Reply_MSG = "شما در دهکده GTOP ثبت نام نکرده اید."+ Message_Signature;
                        BLL.Reply(Reply_MSG, ID, temp_operationID, temp_inboxID);
                    }
                    return "pok";
                }
            } return "dok";
        }


        [AllowAnonymous]
        public string RegisterTempedUsers(string SecureString, string ID, string SecureCode, string operationID, string inboxID)
        {
            int temp_operationID;
            if (!int.TryParse(operationID, out temp_operationID))
            {
                return "NOK";
            }
            int temp_inboxID;
            if (!int.TryParse(inboxID, out temp_inboxID))
            {
                return "NOK";
            }
            string SMS_Message = "NO";

            if (SecureString == "PARSA")
            {
                using (EntitiesConnection MY_EC = new EntitiesConnection())
                {
                    TempUserProfile user = MY_EC.TempUserProfiles.FirstOrDefault(u => u.U_UserName.ToLower() == ID.ToLower());
                    // Check if user already exists
                    if (user != null)
                    {
                        if (user.U_RandomCode.ToLower() == SecureCode.ToLower())
                        {
                            WebSecurity.CreateUserAndAccount(user.U_UserName, user.U_PW, new { U_FirstName = user.U_FirstName, U_LastName = user.U_LastName, U_Gender = user.U_Gender, U_BirthDate = DBNull.Value, U_CodeMelli = user.U_CodeMelli, U_Tell = user.U_Tell, U_Email = user.U_Email, U_Address = user.U_Address, U_Degree = user.U_Degree, U_DegreeField = user.U_DegreeField, U_RegisterDate = DateTime.Now, U_Status = true, U_Score = 0, U_State = user.U_State, U_City = user.U_City, FreeCodeGTOP = false });
                            MY_EC.TempUserProfiles.Remove(user);
                            MY_EC.SaveChanges();

                             SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                             string Reply_MSG = "تبریک می گوییم ثبت نام شما در دهکده GTOP با موفقیت انجام گرفت./n نام کاربری: " + user.U_UserName + "\nکلمه عبور: " + user.U_PW + Message_Signature;
                             BLL.Reply(Reply_MSG, ID, temp_operationID, temp_inboxID);
                        }
                        else
                        {
                            SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                            string Reply_MSG = "کد وارد شده جهت تکمیل عملیات ثبت نام در دهکده GTOP نامعتبر می باشد." + Message_Signature;
                            BLL.Reply(Reply_MSG, ID, temp_operationID, temp_inboxID);
                        }
                    }
                    else
                    {
                        SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                        string Reply_MSG = "شما بایستی در دهکده GTOP پیش ثبت نام نمایید. جهت ثبت نام به آدرس www.emtiaz.gtopmarketing.ir مراجعه فرمایید."  + Message_Signature;
                        BLL.Reply(Reply_MSG, ID, temp_operationID, temp_inboxID);
                    }
                }

            }
            return SMS_Message;
        }



        [Authorize]
        public ActionResult CompleteProfile(string returnurl)
        {
            ViewData["StateName"] = AddressManagement.LoadState();
            if (returnurl.Contains("buyproduct"))
            {
                @ViewBag.TempAttention = "buyproduct";

            }

            ProfileModel model = new ProfileModel();

            using (UsersContext db = new UsersContext())
            {
                UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                if (user != null)
                {
                    model.City = user.U_City;
                    model.FirstName = user.U_FirstName;
                    model.Gender = user.U_Gender;
                    model.LastName = user.U_LastName;
                    model.State = user.U_State;
                    return View(model);
                }
            }
            return View("NOuser");
        }


        [Authorize]
        [HttpPost]
        public ActionResult CompleteProfile(ProfileModel model, string returnurl)
        {

            ViewData["StateName"] = AddressManagement.LoadState();

            if (ModelState.IsValid)
            {

                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserId == WebSecurity.CurrentUserId);
                    if (user != null)
                    {

                        user.U_Address = model.Address;
                        user.U_City = model.City;
                        user.U_FirstName = model.FirstName;
                        user.U_Gender = model.Gender;
                        user.U_LastName = model.LastName;
                        user.U_State = model.State;
                        user.U_CodeMelli = model.PostalCode;
                        db.SaveChanges();
                        return Redirect("~/" + returnurl);
                    }
                }
            }


            return View(model);
        }



        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    if (user != null)
                    {
                        TempData["UserName_PW_Forget"] = user.UserName;
                        return RedirectToAction("ForgetPasswordToken");
                    }
                    else
                    {
                        ModelState.AddModelError("", "نام کاربری در سیستم موجود نمی باشد.");
                    }
                }

            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult ForgetPasswordToken()
        {
            @ViewBag.Token = "dd";
            @ViewBag.UserName_PW = "dd";
            @ViewBag.UserID_PW = "dd";
            if (TempData["UserName_PW_Forget"] != null)
            {
                string username = TempData["UserName_PW_Forget"].ToString();
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
                    if (user != null)
                    {
                        var token = WebSecurity.GeneratePasswordResetToken(user.UserName);
                        ForgetPasswordModel FP_Model = new ForgetPasswordModel();
                        FP_Model.Token = token.Substring(0, 5).ToLower() ;

                        FP_Model.UserName = user.UserName;
                        FP_Model.UserID = user.UserId;
                        return View(FP_Model);

                    }
                    else
                    {
                        return RedirectToAction("ForgetPassword");
                    }
                }

            }
            else
            {
                return RedirectToAction("ForgetPassword");
            }
        }



        [AllowAnonymous]
        public ActionResult PasswordChanged()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SMSNotSend()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult SMSBlocked()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult TempAccount()
        {
            return View();
        }
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                {
                    return RedirectToLocal(returnUrl);
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
                    @ViewBag.MyErrorLogin = "نام کاربری یا کلمه عبور اشتباه است.";
                    return View(model);
                }
            }
            @ViewBag.MyErrorLogin = "لطفاً تمامی نکات را رعایت فرمایید.";
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewData["StateName"] = AddressManagement.LoadState();
            return View();
        }
        [AllowAnonymous]
        public JsonResult GetCity(string id)
        {

            return Json(new SelectList(AddressManagement.GetCity(id), "Value", "Text"));
        }
        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            ViewData["StateName"] = AddressManagement.LoadState();

            if (string.IsNullOrEmpty(model.Register_UserName))
            {
                ModelState.AddModelError("Register_UserName", "لطفاً شماره همراه را وارد فرمایید.");
                @ViewBag.MyErrorNoUsername = "لطفاً شماره همراه را وارد فرمایید.";
            }
            else
            {
                if (model.Register_UserName.Length != 11 || !model.Register_UserName.StartsWith("0"))
                {
                    ModelState.AddModelError("Register_UserName", "لطفاً شماره همراه را وارد فرمایید.");
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
            ModelState.AddModelError("", "لطفاً تمامی نکات زیر را رعایت فرمایید.");
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        //By Himan
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Disassociate(string provider, string providerUserId)
        //{
        //    string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
        //    ManageMessageId? message = null;

        //    // Only disassociate the account if the currently logged in user is the owner
        //    if (ownerAccount == User.Identity.Name)
        //    {
        //        // Use a transaction to prevent the user from deleting their last login credential
        //        using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
        //        {
        //            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
        //            if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
        //            {
        //                OAuthWebSecurity.DeleteAccount(provider, providerUserId);
        //                scope.Complete();
        //                message = ManageMessageId.RemoveLoginSuccess;
        //            }
        //        }
        //    }

        //    return RedirectToAction("Manage", new { Message = message });
        //}

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "کلمه عبور شما تغییر یافت."
                : message == ManageMessageId.SetPasswordSuccess ? ""
                : message == ManageMessageId.RemoveLoginSuccess ? ""
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "کلمه عبور وارد شده صحیح نیست و یا اینکه کلمه عبور جدید نامعتبر است.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        // By Himan
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        //}

        //
        // GET: /Account/ExternalLoginCallback
        //By Himan
        //[AllowAnonymous]
        //public ActionResult ExternalLoginCallback(string returnUrl)
        //{
        //    AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        //    if (!result.IsSuccessful)
        //    {
        //        return RedirectToAction("ExternalLoginFailure");
        //    }

        //    if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
        //    {
        //        return RedirectToLocal(returnUrl);
        //    }

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        // If the current user is logged in add the new account
        //        OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
        //        return RedirectToLocal(returnUrl);
        //    }
        //    else
        //    {
        //        // User is new, ask for their desired membership name
        //        string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
        //        ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
        //        ViewBag.ReturnUrl = returnUrl;
        //        return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
        //    }
        //}

        //
        // POST: /Account/ExternalLoginConfirmation
        //By Himan
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        //{
        //    string provider = null;
        //    string providerUserId = null;

        //    if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
        //    {
        //        return RedirectToAction("Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Insert a new user into the database
        //        using (UsersContext db = new UsersContext())
        //        {
        //            UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
        //            // Check if user already exists
        //            if (user == null)
        //            {
        //                // Insert name into the profile table
        //                db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
        //                db.SaveChanges();

        //                OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
        //                OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

        //                return RedirectToLocal(returnUrl);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("UserName", "نام کاربری قبلاً ثبت شده است");
        //            }
        //        }
        //    }

        //    ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        ////
        //// GET: /Account/ExternalLoginFailure

        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        //[AllowAnonymous]
        //[ChildActionOnly]
        //public ActionResult ExternalLoginsList(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        //}

        //[ChildActionOnly]
        //public ActionResult RemoveExternalLogins()
        //{
        //    ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
        //    List<ExternalLogin> externalLogins = new List<ExternalLogin>();
        //    foreach (OAuthAccount account in accounts)
        //    {
        //        AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

        //        externalLogins.Add(new ExternalLogin
        //        {
        //            Provider = account.Provider,
        //            ProviderDisplayName = clientData.DisplayName,
        //            ProviderUserId = account.ProviderUserId,
        //        });
        //    }

        //    ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
        //    return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        //}

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
