//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMSTools
{
    using System;
    using System.Collections.Generic;
    
    public partial class SMSPattern
    {
        public SMSPattern()
        {
            this.SMS_Inbox_Pattern_Mapping = new HashSet<SMS_Inbox_Pattern_Mapping>();
            this.SMSOperations = new HashSet<SMSOperation>();
        }
    
        public int Id { get; set; }
        public string Pattern { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string ReplyText { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<SMS_Inbox_Pattern_Mapping> SMS_Inbox_Pattern_Mapping { get; set; }
        public virtual ICollection<SMSOperation> SMSOperations { get; set; }
    }
}