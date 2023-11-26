using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Models
{
    public class AgentModel
    {
        
    }

    public class AgentRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public string ShopTell { get; set; }
        public string ShopState { get; set; }
        public string ShopCity { get; set; }
        public string ShopAddress { get; set; }
        public string ShopType { get; set; }
        public string ShopQuotes { get; set; }
        public string ShopQuotes_firstbox { get; set; }
        public string ShopQuotes_secondbox { get; set; }
        public string ShopQuotesfinal { get; set; }
        public string ShopOptionalQoutes { get; set; }
        public bool AcceptInstagram { get; set; }
        public string InstagramAddress { get; set; }
        public bool AcceptPersonalSite { get; set; }
        public string PersonalSiteAddress { get; set; }
        public string ShopEmail { get; set; }
        public string ShopFax { get; set; }


       
        public bool AcceptGPLUSCode { get; set; }
        public bool AcceptBannerDesign { get; set; }
        public bool AcceptWebDesign { get; set; }

        public double Price { get; set; }


        public byte[] ShopPoster { get; set; }

        
        

    }


    public class AgnetAdvetise {

        public AgnetAdvetise() {
            ShopInfoFirstClass = new List<string[]>();
            ShopInfoSecondClass = new List<string[]>();
        }

        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public string ShopTell { get; set; }
        public string ShopAddress { get; set; }
        public List<string[]> ShopInfoFirstClass { get; set; }
        public List<string[]> ShopInfoSecondClass { get; set; }
    }
}