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
    
    public partial class ProductHistory
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> OperationDate { get; set; }
        public string OperationHistory { get; set; }
        public Nullable<int> F_P_ID { get; set; }
        public Nullable<int> F_UserID { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
