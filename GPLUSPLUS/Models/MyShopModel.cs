using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPLUSPLUS.Models
{
    public class MyShopModel
    {
    }
    public class ShopCustomPending {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public string ShopTell { get; set; }
        public string ShopState { get; set; }
        public string ShopCity { get; set; }
        public string ShopAddress { get; set; }
        public Nullable<System.DateTime> DateJoined { get; set; }
        public string F_ShopType_name { get; set; }
        public bool AcceptCode { get; set; }
        public bool AcceptBanner { get; set; }
        public bool AcceptSite { get; set; }
        public bool ShopRegisterStatus { get; set; }
        public string ShopQuote { get; set; }
        public string IntagramAddress { get; set; }
        public string PersonalSiteAddress { get; set; }
 
        


        public string ImageThumb { get; set; }



        public HttpPostedFileBase S_ImageOf { get; set; }
    
    }


    public class ShopAdvertiseCustomPending
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public string ShopTell { get; set; }
        public string ShopAddress { get; set; }
        public Nullable<System.DateTime> DateRequested { get; set; }
        public Nullable<System.DateTime> DateRequestAccepted { get; set; }
        public bool RequestStatus { get; set; }
    }
}