using GPLUSPLUS.Models.Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace GPLUSPLUS.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        //newly added
        public DbSet<webpages_Membership> webpages_Memberships { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string U_FirstName { get; set; }
        public string U_LastName { get; set; }
        public string U_Gender { get; set; }
        public DateTime? U_BirthDate { get; set; }
        public string U_CodeMelli { get; set; }
        public string U_Tell { get; set; }
        public string U_Email { get; set; }
        public string U_Address { get; set; }
        public string U_Degree { get; set; }
        public string U_DegreeField { get; set; }
        public DateTime? U_RegisterDate { get; set; }
        public bool? U_Status { get; set; }
        public int? U_Score { get; set; }
        public string U_State { get; set; }
        public string U_City { get; set; }
        public bool? FreeCodeGTOP { get; set; }

        public int? U_LotteryScore { get; set; }
    }

    [Table("webpages_Membership")]
    public class webpages_Membership
    {
        [Key]
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ConfirmationToken { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime LastPasswordFailureDate { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        public string Password { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordVerificationToken { get; set; }
        public DateTime PasswordVerificationTokenExpirationDate { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور فعلی")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "کلمه عبور حداقل باید 6 کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید کلمه عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "تایید کلمه عبور نا معتبر است.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "لطفاً شماره همراه را وارد فرمایید.")]
        public string UserName { get; set; }

           [Required(ErrorMessage = "لطفاً کلمه عبور را وارد فرمایید.")]
        public string Password { get; set; }

        //[Display(Name = "مرا بخاطر بسپار")]
           public Boolean RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "لطفاً شماره همراه را وارد فرمایید.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "شماره همراه نامعتبر است.")]
        public string Register_UserName { get; set; }

           [Required(ErrorMessage = "لطفاً کلمه عبور را تعیین فرمایید.")]
           [DataType(DataType.Password)]
        public string Register_Password { get; set; }

         [Required(ErrorMessage = "لطفاً نام را وارد فرمایید.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفاً نام خانوادگی را وارد فرمایید.")]
        public string LastName { get; set; }

       [Required(ErrorMessage = "لطفاً جنسیت را تعیین کنید.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "لطفاً استان را تعیین کنید.")]
        public string State { get; set; }

        [Required(ErrorMessage = "لطفاً شهرستان را تعیین کنید.")]
        public string City { get; set; }
        public string JobTitle { get; set; }
        public string JobField { get; set; }
        public string RandomCode { get; set; }
        public int? TrySendCode { get; set; }
    }

    public class ProfileModel
    {

        [Required(ErrorMessage = "لطفاً نام را وارد فرمایید.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفاً نام خانوادگی را وارد فرمایید.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفاً جنسیت را تعیین کنید.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "لطفاً استان را تعیین کنید.")]
        public string State { get; set; }

        [Required(ErrorMessage = "لطفاً شهرستان را تعیین کنید.")]
        public string City { get; set; }

        [Required(ErrorMessage = "لطفاً آدرس پستی را به صورت دقیق وارد کنید.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "لطفاً کدپستی را وارد کنید.")]
        public string PostalCode { get; set; }

    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

    public class TempUserRegister
    {

        public string[] Register(RegisterModel model) {
                            string[] result = new string[4];
            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                const string chars = "ABCDEFGHJKMNPQRSTUVWXYZ";
                var random = new Random();
                string SecureCode = new string(Enumerable.Repeat(chars, 4)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
                string[] sms_result = new string[2];
                string IP = "http://193.104.22.14:2055/CPSMSService/Access";
                Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
                string SMS_Message = "شما در دهکده GTOP ثبت نام نموده اید. جهت تایید کد چهار رقمی زیر را به سامانه 10006020 ارسال نمایید.\n کد: " + SecureCode;
                sms_result = sms_Single.SendSMS_Single(SMS_Message, model.Register_UserName, "10006020", "ATSIGNCO9", "m@hfye4@5", IP, "ATSIGNCO", false);
                TempUserProfile TempU = new TempUserProfile();
                TempU.U_UserName = model.Register_UserName;
                TempU.U_FirstName = model.FirstName;
                TempU.U_LastName = model.LastName;
                TempU.U_Gender = model.Gender;
                TempU.U_State = model.State;
                TempU.U_City = model.City;
                TempU.U_PW = model.Register_Password;
                TempU.U_SMS_FisrtStstus = sms_result[0];
                TempU.U_SMS_SecondStatus = sms_result[1];
                TempU.U_RandomCode = SecureCode;
                TempU.Try_Send_Code = 1;
                TempU.U_JobTitle = model.JobTitle;
                TempU.U_JobField = model.JobField;
                model.RandomCode = SecureCode;
                result[0] = sms_result[0];
                result[1] = sms_result[1];
                result[2] = SecureCode;
                try
                {
                    EC_DB.TempUserProfiles.Add(TempU);
                    EC_DB.SaveChanges();
                    result[3] = "OK";
                }
                catch (Exception)
                {

                    result[3] = "NOK";
                }
            }
            return result;
        }


        public string[] ReRegister(TempUserProfile tempuser, RegisterModel model)
        {
            string[] result = new string[4];
            using (EntitiesConnection EC_DB = new EntitiesConnection())
            {
                string[] sms_result = new string[2];
                string IP = "http://193.104.22.14:2055/CPSMSService/Access";
                Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
                string SMS_Message = "شما در دهکده GTOP ثبت نام نموده اید. جهت تایید کد چهار رقمی زیر را به سامانه 10006020 ارسال نمایید.\n کد: '" + tempuser.U_RandomCode;
                sms_result = sms_Single.SendSMS_Single(SMS_Message, model.Register_UserName, "10006020", "ATSIGNCO9", "m@hfye4@5", IP, "ATSIGNCO", false);

                tempuser.U_FirstName = model.FirstName;
                tempuser.U_LastName = model.LastName;
                tempuser.U_Gender = model.Gender;
                tempuser.U_State = model.State;
                tempuser.U_City = model.City;
                tempuser.U_PW = model.Register_Password;
                tempuser.U_SMS_FisrtStstus = sms_result[0];
                tempuser.U_SMS_SecondStatus = sms_result[1];
                tempuser.Try_Send_Code = tempuser.Try_Send_Code + 1;
                tempuser.U_JobField = model.JobField;
                tempuser.U_JobTitle = model.JobTitle;
                model.RandomCode = tempuser.U_RandomCode;
                result[0] = sms_result[0];
                result[1] = sms_result[1];
                result[2] = tempuser.U_RandomCode;
                try
                {
                    EC_DB.SaveChanges();
                    result[3] = "OK";
                }
                catch (Exception)
                {

                    result[3] = "NOK";
                }
            }
            return result;
        }


    }

    public class ForgetPasswordModel {

        [Required]
        public string UserName { get; set; }

        public int UserID { get; set; }

        public string Token { get; set; }

        public string NewPassword { get; set; }
        
    
    }


  
}
