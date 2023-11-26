using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPLUSPLUS.Models;
using GPLUSPLUS.Models.Bussiness;
using WebMatrix.WebData;
namespace GPLUSPLUS.Controllers
{
     
    public class GTOPSMSController : Controller
    {
        string Message_Signature = "\nاز طرف مجموعه GTOP";        //
        // GET: /GTOPSMS/



        //public string test()
        //{
        //    SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
        //    BLL.Receive_SMS("Twxx", "09146319548");
        //    return "OK";

        //}

    

        public void SubmitCodeViaSMS(string SecureString, string From, string Code, string operationID, string inboxID)
        {
            int temp_operationID;
            if (!int.TryParse(operationID, out temp_operationID))
            {
                return;
            }
            int temp_inboxID;
            if (!int.TryParse(inboxID, out temp_inboxID))
            {
                return;
            }
            if (SecureString == "PARSADORSA")
            {
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName == From);
                    if (user != null)
                    {
                        ScoreManagement SM = new ScoreManagement();
                        string result;
                        result = SM.NewScore(user.UserId, Code);
                        SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();

                        if (result.Equals("Code Used Before"))
                        {
                            string Reply_MSG = "کد امتیاز قبلاً استفاده شده است." + Message_Signature;
                            BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);

                        }
                        else if (result.Equals("Free Code Used Befor"))
                        {
                            string Reply_MSG = "شما قبلاً از کد رایگان استفاده نموده اید." + Message_Signature;
                            BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);

                        }
                        else if (result.Equals("User Blocked BrutForced Score"))
                        {
                            string Reply_MSG = "شما  بیشتر از تعداد مجاز، کد امتیاز را به صورت اشتباه وارد نموده اید. جهت اطلاعات بیشتر با مرکز GTOP تماس حاصل فرمایید.";
                            BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);

                        }
                        else if (result.Equals("Code Not Found"))
                        {
                            string Reply_MSG = "کد امتیاز نا معتبر است." + Message_Signature;
                            BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);

                        }
                        else
                        {
                            string Reply_MSG = "کد امتیاز با موفقیت ثبت شد.امتیاز شما " + result + "می باشد." + Message_Signature;
                            BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);

                        }
                    }
                    else
                    {
                        ScoreManagement SM = new ScoreManagement();
                        if (SM.isCodeValid(Code))
                        {
                            string tempPass = Code.Substring(0, 5);
                            WebSecurity.CreateUserAndAccount(From, tempPass, new { U_FirstName = DBNull.Value, U_LastName = DBNull.Value, U_Gender = DBNull.Value, U_BirthDate = DBNull.Value, U_CodeMelli = DBNull.Value, U_Tell = DBNull.Value, U_Email = DBNull.Value, U_Address = DBNull.Value, U_Degree = DBNull.Value, U_DegreeField = DBNull.Value, U_RegisterDate = DateTime.Now, U_Status = true, U_Score = 0, U_State = DBNull.Value, U_City = DBNull.Value, FreeCodeGTOP = false });

                            SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                            string Reply_MSG = "تبریک می گوییم ثبت نام شما در دهکده GTOP با موفقیت انجام گرفت.\n نام کاربری: " + From + "\nکلمه عبور: " + tempPass + Message_Signature;
                            BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);

                            UserProfile new_User = db.UserProfiles.FirstOrDefault(u => u.UserName == From);
                            string result = SM.NewScore(new_User.UserId, Code);
                            string Reply_MSG_Second = "کد امتیاز با موفقیت ثبت شد.امتیاز شما " + result + "می باشد." + Message_Signature;
                            BLL.Reply(Reply_MSG_Second, From, temp_operationID, temp_inboxID);
                        }
                        else
                        {
                            SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                            string Reply_MSG = "کد امتیاز نا معتبر و یا قبلاً استفاده شده است." + Message_Signature;
                            BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);
                        }
                    }
                }
            }
        }

        public void TrackingCodeViaSMS(string SecureString, string From, string TrackingCode, string operationID, string inboxID)
        {
            int temp_operationID;
            if (!int.TryParse(operationID, out temp_operationID))
            {
                return;
            }
            int temp_inboxID;
            if (!int.TryParse(inboxID, out temp_inboxID))
            {
                return;
            }
            if (SecureString == "PARSADORSA")
            {
                using (UsersContext db = new UsersContext())
                {

                    ScoreManagement SM = new ScoreManagement();
                    string result;
                    result = SM.TrackCode(TrackingCode);
                    SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                    if (result.Equals("Code is not exist"))
                    {
                        string Reply_MSG = "شماره پیگیری نامعتبر است." + Message_Signature;
                        BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);

                    }
                    else if (result.Equals("Code used before"))
                    {
                        string Reply_MSG = "کد امتیاز قبلاً استفاده شده است." + Message_Signature;
                        BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);

                    }
                    else
                    {
                        string Reply_MSG = "امتیاز کد " + result + " می باشد." + Message_Signature;
                        BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);

                    }

                }
            }
        }


        public void UserScoreViaSMS(string SecureString, string From, string operationID, string inboxID)
        {
            int temp_operationID;
            if (!int.TryParse(operationID, out temp_operationID))
            {
                return;
            }
            int temp_inboxID;
            if (!int.TryParse(inboxID, out temp_inboxID))
            {
                return;
            }
            if (SecureString == "PARSADORSA")
            {
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName == From);
                    if (user != null)
                    {
                        ScoreManagement SM = new ScoreManagement();
                        int? result;
                        result = SM.TotalUserScore(user.UserId);

                        int Score = result ?? default(int);


                        int? result_lottery;
                        result_lottery = SM.TotalUserLotteryScore(user.UserId);

                        int lottery = result_lottery ?? default(int);
                        SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                        string Reply_MSG = "امتیاز شما در سایت GPLUPLUS " + Score + " می باشد. همچنین امتیاز شما در قرعه کشی G&G "+lottery+" می باشد." + Message_Signature;
                        BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);
                    }
                    else {
                        SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                        string Reply_MSG = "شما هنوز در سایت G&G عضو نشده اید." + Message_Signature;
                        BLL.Reply(Reply_MSG, From, temp_operationID, temp_inboxID);
                    }

                }
            }
        }



        [Authorize(Users = "09144390137,09141863449")]
        public ActionResult Index()
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();

            ViewBag.SMSCharge = BLL.SMS_Amount();


            return View();
        }
           [Authorize(Users = "09141863449")]
        public ActionResult AddNewPattern()
        {
            return View();
        }

           [Authorize(Users = "09141863449")]
        [HttpPost]
        public ActionResult AddNewPattern(New_Pattern_Model model)
        {

            if (string.IsNullOrEmpty(model.pattern))
                ModelState.AddModelError("pattern", "pattern field is empty");

            if (string.IsNullOrEmpty(model.pattern_description))
                ModelState.AddModelError("pattern_description", "pattern description field is empty");
            if (string.IsNullOrEmpty(model.reply_template))
                ModelState.AddModelError("reply_template", "reply_template field is empty");
            if (string.IsNullOrEmpty(model.operation_description))
                ModelState.AddModelError("operation_description", "operation description field is empty");
            if (string.IsNullOrEmpty(model.operation))
                ModelState.AddModelError("operation", "operation field is empty");
            else
            {
                if (model.operation == "FORWARD")
                {
                    if (string.IsNullOrEmpty(model.template_text))
                        ModelState.AddModelError("template_text", "template text field is empty");

                    if (string.IsNullOrEmpty(model.template_destination))
                        ModelState.AddModelError("template_destination", "template destination field is empty");

                }
                else
                {
                    model.template_text = "";
                    model.template_destination = "";
                }
            }

            if (!ModelState.IsValid)
                return View(model);
            else
            {
                string result;
                SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
                result = BLL.AddNewPattern(model.pattern, model.pattern_description, model.operation, model.operation_description, model.reply_template, model.template_text, model.template_destination);
                if (result == "OK")
                    return RedirectToAction("ShowPattern");
                else if (result == "Pattern is already exist")
                {
                    @ViewBag.ErrorResult = "الگو قبلاً استفاده شده است.";
                    return View(model);
                }
                else if (result == "number of Forward list and template list is not match")
                {
                    @ViewBag.ErrorResult = "تعداد گیرندگان با تعداد پیغام ها برابر نیست.";
                    return View(model);
                }
                else if (result == "number of Descrption list and template list is not match")
                {
                    @ViewBag.ErrorResult = "تعداد توضیحات دستورات با تعداد پیغام ها برابر نیست.";
                    return View(model);
                }
                else
                {
                    @ViewBag.ErrorResult = "دستور اجرایی نا مشخص است.";
                    return View(model);
                }

            }



        }

           [Authorize(Users = "09141863449")]
        public ActionResult ShowPattern()
        {
            Show_Pattern_Model model = new Show_Pattern_Model();
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            model.patternList = BLL.ShowPattern();
            return View(model);
        }

           [Authorize(Users = "09141863449")]
        public ActionResult ShowPatternOperation(int Id)
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            Show_Pattern_Detailed_Model model = new Show_Pattern_Detailed_Model();
            model.patternDetailed = BLL.ShowPatternDetailed(Id);
            @ViewBag.PatternId = Id;
            return View(model);
        }

           [Authorize(Users = "09141863449")]
        public ActionResult ShowPatternDetailed(int Id)
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            Pattern_Model model = new Pattern_Model();
            var pattern = BLL.ShowPattern().FirstOrDefault(u => u.patternId == Id);

            model.pattern = pattern.pattern;
            model.patternId = pattern.patternId;
            model.reply_template = pattern.replyText;
            model.pattern_description = pattern.patternDescription;
            return View(model);
        }


           [Authorize(Users = "09141863449")]
        public ActionResult DeletePattern(int Id)
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            Pattern_Model model = new Pattern_Model();
            var pattern = BLL.ShowPattern().FirstOrDefault(u => u.patternId == Id);

            if (pattern == null)
                return HttpNotFound();
            model.pattern = pattern.pattern;
            model.patternId = pattern.patternId;
            model.reply_template = pattern.replyText;
            model.pattern_description = pattern.patternDescription;
            return View(model);
        }

           [Authorize(Users = "09141863449")]
        [HttpPost]
        public ActionResult DeletePattern(FormCollection fcNotUsed, int Id)
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            int result = BLL.DeletePattern(Id);
            if (result == 0)
            {
                @ViewBag.ErrorResult = "الگویی پیدا نشد.";
                return View();
            }
            else
            {
                return RedirectToAction("ShowPattern");
            }
        }

           [Authorize(Users = "09141863449")]
        public ActionResult DeleteOperation(int PatternId, int OperationId)
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            Operation_Template_Model model = new Operation_Template_Model();
            var operation = BLL.ShowPatternDetailed(PatternId).FirstOrDefault(u => u.operationId == OperationId);

            if (operation == null)
                return HttpNotFound();
            model.patternId = PatternId;
            model.operation = operation.operation;
            model.operationId = operation.operationId;
            model.operation_description = operation.operation_description;
            model.operation_template_destination = operation.operation_template_destination;
            model.operation_template_text = operation.operation_template_text;
            model.templateId = operation.templateId;
            return View(model);
        }

           [Authorize(Users = "09141863449")]
        [HttpPost]
        public ActionResult DeleteOperation(FormCollection fcNotUsed, int PatternId, int OperationId)
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            int result = BLL.DeleteOperation(PatternId, OperationId);
            if (result == 0)
            {
                @ViewBag.ErrorResult = "عملیاتی پیدا نشد.";
                return View();
            }
            else
            {
                return RedirectToAction("ShowPatternOperation", new { Id = PatternId });
            }
        }

           [Authorize(Users = "09141863449")]
        public ActionResult EditPattern(int Id)
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            Pattern_Model model = new Pattern_Model();
            var pattern = BLL.ShowPattern().FirstOrDefault(u=>u.patternId==Id);
            if (pattern == null)
                return View("kkk");
            model.pattern = pattern.pattern;
            model.reply_template = pattern.replyText;
            model.pattern_description = pattern.patternDescription;
            model.patternId = pattern.patternId;
            return View(model);
        }

           [Authorize(Users = "09141863449")]
        [HttpPost]
        public ActionResult EditPattern(Pattern_Model model, int Id)
        {

            if (string.IsNullOrEmpty(model.pattern))
                ModelState.AddModelError("pattern", "pattern field is empty");
            if (string.IsNullOrEmpty(model.pattern_description))
                ModelState.AddModelError("pattern_description", "pattern description field is empty");
            if (string.IsNullOrEmpty(model.reply_template))
                ModelState.AddModelError("reply_template", "pattern reply field is empty");
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
                string result = BLL.EditPattern(Id, model.pattern, model.pattern_description, model.reply_template);
                if (result == "Pattern Not Exist")
                {
                    @ViewBag.ErrorResult = "الگو وجود ندارد";
                    return View(model);
                }
                else if (result == "Pattern Is Used By Another")
                {
                    @ViewBag.ErrorResult = "این الگو قبلاً استفاده شده است.";
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ShowPatternDetailed", new { Id = Id });
                }
            }

        }

           [Authorize(Users = "09141863449")]
        public ActionResult EditOperationForward(int PatternId, int OperationId, int TemplateId)
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            Operation_Template_Model model = new Operation_Template_Model();
            var operation = BLL.ShowPatternDetailed(PatternId).FirstOrDefault(u => u.templateId == TemplateId);

            if (operation == null)
                return HttpNotFound();
            model.patternId = PatternId;
            model.operation = operation.operation;
            model.operationId = operation.operationId;
            model.operation_description = operation.operation_description;
            model.operation_template_destination = operation.operation_template_destination;
            model.operation_template_text = operation.operation_template_text;
            model.templateId = operation.templateId;
            return View(model);
        }

           [Authorize(Users = "09141863449")]
        [HttpPost]
        public ActionResult EditOperationForward(Operation_Template_Model model, int PatternId, int OperationId, int TemplateId)
        {
            if (string.IsNullOrEmpty(model.operation_description))
                ModelState.AddModelError("operation_description", "operation description field is empty");
            if (string.IsNullOrEmpty(model.operation_template_destination))
                ModelState.AddModelError("operation_template_destination", "template destination field is empty");
            if (string.IsNullOrEmpty(model.operation_template_text))
                ModelState.AddModelError("operation_template_text", "template text  field is empty");
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
                string result = BLL.Edit_Operation_Template(PatternId, OperationId, model.operation_description, TemplateId, model.operation_template_text, model.operation_template_destination);
                if (result == "Operation Not Exist")
                {
                    @ViewBag.ErrorResult = "عملیات وجود ندارد";
                    return View(model);
                }
                else if (result == "Template Not Exist")
                {
                    @ViewBag.ErrorResult = "قالب وجود ندارد";
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ShowPatternOperation", new { Id = PatternId });
                }
            }
        }

           [Authorize(Users = "09141863449")]
        public ActionResult EditOperation(int PatternId, int OperationId)
        {
            SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
            Operation_Template_Model model = new Operation_Template_Model();
            var operation = BLL.ShowPatternDetailed(PatternId).FirstOrDefault(u => u.operationId == OperationId);

            if (operation == null)
                return HttpNotFound();
            model.patternId = PatternId;
            model.operation = operation.operation;
            model.operationId = operation.operationId;
            model.operation_description = operation.operation_description;
            return View(model);
        }

           [Authorize(Users = "09141863449")]
        [HttpPost]
        public ActionResult EditOperation(Operation_Template_Model model, int PatternId, int OperationId)
        {
            if (string.IsNullOrEmpty(model.operation_description))
                ModelState.AddModelError("operation_description", "operation description field is empty");
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                SMSTools.BLL.ManagementToolsBLL BLL = new SMSTools.BLL.ManagementToolsBLL();
                string result = BLL.Edit_Operation(PatternId, OperationId, model.operation_description);
                if (result == "Operation Not Exist")
                {
                    @ViewBag.ErrorResult = "عملیات وجود ندارد";
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ShowPatternOperation", new { Id = PatternId });
                }
            }
        }

           [Authorize(Users = "09141863449")]
        public ActionResult SingleSMS()
        {
            return View();
        }

           [Authorize(Users = "09141863449")]
        [HttpPost]
        public ActionResult SingleSMS(Send_SMS_Model model)
        {

            if (string.IsNullOrEmpty(model.message))
                ModelState.AddModelError("message", "متن پیام خالی است");
            if (string.IsNullOrEmpty(model.dest))
                ModelState.AddModelError("dest", "شماره گیرنده خالی است");

            if (!ModelState.IsValid)
                return View(model);
            else
            {
                SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                string[] result = BLL.Send_single(model.message, model.dest);

                if (result[0] == "SMS amount is insufficient")
                {
                    @ViewBag.SMSResult = "اعتبار کافی نیست";
                    return View(model);
                }
                else
                {
                    @ViewBag.SMSResult = "با موفقیت ارسال شد.";
                    return View(model);
                }
            }

        }

           [Authorize(Users = "09141863449")]
        public ActionResult BatchSMS()
        {
            return View();
        }

           [Authorize(Users = "09141863449")]
        [HttpPost]
        public ActionResult BatchSMS(Send_SMS_Model model)
        {
            if (string.IsNullOrEmpty(model.message))
                ModelState.AddModelError("message", "متن پیام خالی است");
            if (string.IsNullOrEmpty(model.dest))
                ModelState.AddModelError("dest", "شماره گیرنده خالی است");
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                string[] result = BLL.Send_batch(model.message, model.dest);
                if (result[0] == "SMS amount is insufficient")
                {
                    @ViewBag.SMSResult = "اعتبار کافی نیست";
                    return View(model);
                }
                else
                {
                    @ViewBag.SMSResult = "با موفقیت ارسال شد.";
                    return View(model);
                }
            }
        }

           [Authorize(Users = "09141863449")]
        public ActionResult Like_To_Like()
        {
            return View();
        }

           [Authorize(Users = "09141863449")]
        [HttpPost]
        public ActionResult Like_To_Like(Send_SMS_Model model)
        {
            if (string.IsNullOrEmpty(model.message))
                ModelState.AddModelError("message", "متن پیام خالی است");
            if (string.IsNullOrEmpty(model.dest))
                ModelState.AddModelError("dest", "شماره گیرنده خالی است");
            if (!ModelState.IsValid)
                return View(model);
            else
            {
                SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                string[] result = BLL.Send_LikeToLike(model.message, model.dest);

                if (result[0] == "message count and destination count is not equal")
                {
                    @ViewBag.SMSResult = "تعداد پیغام ها و شماره گیرنده ها برابر نیست";
                    return View(model);
                }
                else if (result[0] == "SMS amount is insufficient")
                {
                    @ViewBag.SMSResult = "اعتبار کافی نیست";
                    return View(model);
                }
                else
                {
                    @ViewBag.SMSResult = "با موفقیت ارسال شد.";
                    return View(model);
                }
            }

        }


                [Authorize(Users = "09144390137,09141863449")]
           public ActionResult SendToAllUsers()
           {
               return View();
           }

           [Authorize(Users = "09144390137,09141863449")]
           [HttpPost]
           public ActionResult SendToAllUsers(Send_SMS_Model model)
           {
               if (string.IsNullOrEmpty(model.message))
                   ModelState.AddModelError("message", "متن پیام خالی است");
               if (!ModelState.IsValid)
                   return View(model);
               else
               {
                   string destinations = "";
                  using (UsersContext db = new UsersContext())
                   {
                       var query = from c in db.UserProfiles where c.UserId > 187 select c.UserName;
                       bool firstTime = true;
                       foreach (var q in query) {
                           if (firstTime)
                           {
                               destinations = q;
                               firstTime = false;
                           }
                           else
                               destinations = destinations + "," + q;    
                       }
                   
                   }
                   SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                   string[] result = BLL.Send_batch(model.message, destinations);
                   if (result[0] == "SMS amount is insufficient")
                   {
                       @ViewBag.SMSResult = "اعتبار کافی نیست";
                       return View(model);
                   }
                   else
                   {
                       @ViewBag.SMSResult = "با موفقیت ارسال شد.";
                       return View(model);
                   }
               }
           }


           [Authorize(Users = "09144390137,09141863449")]
           public ActionResult SendToAllShops()
           {
               return View();
           }

           [Authorize(Users = "09144390137,09141863449")]
           [HttpPost]
           public ActionResult SendToAllShops(Send_SMS_Model model)
           {
               if (string.IsNullOrEmpty(model.message))
                   ModelState.AddModelError("message", "متن پیام خالی است");
               if (!ModelState.IsValid)
                   return View(model);
               else
               {
                   string destinations = "";
                   using (EntitiesConnection EC = new EntitiesConnection())
                   {
                       var query = from c in EC.Shops   select c.Mobile;
                       bool firstTime = true;
                       foreach (var q in query)
                       {
                           if (firstTime)
                           {
                               destinations = q;
                               firstTime = false;
                           }
                           else
                               destinations = destinations + "," + q;
                       }
                   }
                   SMSTools.BLL.SendingReciveingBLL BLL = new SMSTools.BLL.SendingReciveingBLL();
                   string[] result = BLL.Send_batch(model.message, destinations);
                   if (result[0] == "SMS amount is insufficient")
                   {
                       @ViewBag.SMSResult = "اعتبار کافی نیست";
                       return View(model);
                   }
                   else
                   {
                       @ViewBag.SMSResult = "با موفقیت ارسال شد.";
                       return View(model);
                   }
               }
           }



    }
}
