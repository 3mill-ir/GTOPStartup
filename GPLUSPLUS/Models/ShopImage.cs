//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GPLUSPLUS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShopImage
    {
        public int Id { get; set; }
        public byte[] ShopImage1 { get; set; }
        public byte[] ShopThumbnail { get; set; }
        public Nullable<System.DateTime> DateUploaded { get; set; }
        public Nullable<System.DateTime> DateAccepted { get; set; }
        public Nullable<bool> ImageStatus { get; set; }
        public Nullable<int> F_Shop_ID { get; set; }
    
        public virtual Shop Shop { get; set; }
    }
}
