using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Models
{
    public class HomeModel
    {
       public  HomeModel() {
           myShopTypes = new List<string[]>();
           myShops = new List<string[]>();
           myProducts = new List<string[]>();

       }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string Register_UserName { get; set; }
        public string Register_Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CodeMelli { get; set; }
        public string Tell { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Degree { get; set; }
        public string DegreeField { get; set; }
        public DateTime? RegisterDate { get; set; }
        public bool? Status { get; set; }
        public int? Score { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public string RandomCode { get; set; }

        public int? TrySendCode { get; set; }

        public string CodeNumber { get; set; }

        public int? UserTotalScore { get; set; }


        public string myCity { get; set; }
        public List<string[]> myShopTypes { get; set; }


        public List<string[]> myShops { get; set; }




        public List<string[]> myProducts { get; set; }





        public string Q_name { get; set; }
        public string Q_Email { get; set; }
        public string Q_Message { get; set; }




    }

   


}