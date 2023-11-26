using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;


namespace GPLUSPLUS.Models
{
    public class SMSModel
    {

    }
    public class Send_SMS_Model
    {
        public string message { get; set; }
        public string dest { get; set; }

    }



    public class New_Pattern_Model
    {
        public string pattern { get; set; }
        public string pattern_description { get; set; }
        public string operation { get; set; }
        public string operation_description { get; set; }
        public string reply_template { get; set; }
        public string template_text { get; set; }
        public string template_destination { get; set; }
        public string result { get; set; }
    }


    public class Pattern_Model
    {

        public int patternId { get; set; }
        public string pattern { get; set; }
        public string pattern_description { get; set; }
        public string reply_template { get; set; }

      
    }
    public class Operation_Template_Model
    {

        public int patternId { get; set; }
        public int operationId { get; set; }
        public string operation { get; set; }
        public int templateId { get; set; }
        public string operation_description { get; set; }
        public string operation_template_destination { get; set; }
        public string operation_template_text { get; set; }

    }
  

    public class Delete_Pattern_Model
    {
        public int patternId { get; set; }
    }
    
    public class Show_Pattern_Model
    {
        public Show_Pattern_Model()
        {
            //ProductTypes = new Collection<ProductTypesList>();
            patternList = new Collection<SMSTools.DAL.Patterns>();
        }
        public Collection<SMSTools.DAL.Patterns> patternList { get; set; }
    }

    public class Show_Pattern_Detailed_Model
    {
        public Show_Pattern_Detailed_Model()
        {
            //ProductTypes = new Collection<ProductTypesList>();
            patternDetailed = new Collection<SMSTools.DAL.OperationsAndTemplates>();
        }
        public Collection<SMSTools.DAL.OperationsAndTemplates> patternDetailed { get; set; }
    }

    public class Delete_Operation_Model
    {
        public int operationId { get; set; }
    }

}