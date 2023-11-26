using SMSTools.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SMSTools.BLL
{
    public class ManagementToolsBLL
    {
        string IP = "http://193.104.22.14:2055/CPSMSService/Access";
        string Number = "10006020";
        string UserName = "ATSIGNCO9";
        string Password = "m@hfye4@5";
        string Company = "ATSIGNCO";
        /// <summary>
        /// bagimandeye payamak
        /// </summary>
        /// <returns>tedade payamhaye bagimande</returns>
        public string SMS_Amount()
        {
            Cls_SMS.ClsGetRemain sms = new Cls_SMS.ClsGetRemain();
            string amount = sms.GetRemainCredit(UserName, Password, Company, IP);
            //lblRemain.Text = rem + " عدد پيامك  ";
            return amount;
        }
        /// <summary>
        /// vaziyate payamak
        /// </summary>
        /// <param name="batchId">shomareye guruh</param>
        /// <returns>vaziyate payame ersal shode</returns>
        public ArrayList SMS_Status(string batchId)
        {
            Cls_SMS.ClsStatus sms = new Cls_SMS.ClsStatus();
            ArrayList Arr_Res = new ArrayList();
            Arr_Res = sms.StatusSMS(UserName, Password, IP, Company, Company + "+" + batchId);
            return Arr_Res;
        }



        public string AddNewPattern(string pattern, string pattern_description, string operation, string operation_description, string reply_template, string template_text, string template_destination)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return "Pattern is empty";
            }
            else
            {
                    SMSDAL DAL = new SMSDAL();
                    var Temppattern = DAL.GetPatternList().FirstOrDefault(u => u.pattern == pattern);
                    if (Temppattern!=null)
                        return "Pattern is already exist";
                
                    switch (operation)
                    {
                        case "FORWARD":
                            {
                                if (string.IsNullOrEmpty(template_text) || string.IsNullOrEmpty(template_destination))
                                {
                                    return "Forward Template or Destination is empty";
                                }
                                else
                                {
                                    List<string> forward_description = new List<string>();
                                    forward_description = (operation_description).Split(',').ToList();

                                    List<string> forward_list = new List<string>();
                                    forward_list = (template_destination).Split(',').ToList();

                                    List<string> template_list = new List<string>();
                                    template_list = (template_text).Split(',').ToList();

                                    if (template_list.Count != forward_list.Count)
                                    {
                                        return "number of Forward list and template list is not match";
                                    }
                                    if (forward_description.Count != forward_list.Count)
                                    {
                                        return "number of Descrption list and template list is not match";
                                    }
                                    else
                                    {
                                        DAL.SaveNewPattern_Operation_Forward(pattern, pattern_description, reply_template, forward_description, forward_list, template_list);
                                        return "OK";
                                    }
                                }

                            }
                        case "SUBMITCODE":
                            {

                                    DAL.SaveNewPattern_Operation_(pattern, pattern_description, reply_template, operation, operation_description);
                                        return "OK";

                            }
                        case "CODETRACKING":
                            {

                                DAL.SaveNewPattern_Operation_(pattern, pattern_description, reply_template, operation, operation_description);
                                return "OK";

                            }
                        case "PASSWORDRESET":
                            {

                                DAL.SaveNewPattern_Operation_(pattern, pattern_description, reply_template, operation, operation_description);
                                return "OK";

                            }
                        case "USERCONFIRMATION":
                            {

                                DAL.SaveNewPattern_Operation_(pattern, pattern_description, reply_template, operation, operation_description);
                                return "OK";

                            }
                        case "USERSCORE":
                            {

                                DAL.SaveNewPattern_Operation_(pattern, pattern_description, reply_template, operation, operation_description);
                                return "OK";

                            }
                        case "GUIDE":
                            {

                                DAL.SaveNewPattern_Operation_(pattern, pattern_description, reply_template, operation, operation_description);
                                return "OK";

                            }
                        default:
                            {
                                return "No Operation Selected";
                            }
                    }
                }
            

        }

        public int DeletePattern(int patternId)
        {
            SMSDAL DAL = new SMSDAL();

          var  pattern = DAL.GetPatternList().FirstOrDefault(u=>u.patternId==patternId);

          if (pattern!=null)
                {
                    DAL.DeletePattern(pattern.patternId);
                    return pattern.patternId;
                }

            return 0;
        }


        public Collection<Patterns> ShowPattern()
        {
            SMSDAL DAL = new SMSDAL();
            Collection<Patterns> patternList = new Collection<Patterns>();
            patternList = DAL.GetPatternList();
            return patternList;
        }




        public Collection<OperationsAndTemplates> ShowPatternDetailed(int Id)
        {
            SMSDAL DAL = new SMSDAL();
            Collection<OperationsAndTemplates> patternDetailed = new Collection<OperationsAndTemplates>();
            patternDetailed = DAL.GetOperationAndTemplate(Id);
            return patternDetailed;
        }

        public int DeleteOperation(int patternId,int operationId)
        {
            SMSDAL DAL = new SMSDAL();

            var operation = DAL.GetOperationAndTemplate(patternId).FirstOrDefault(u => u.operationId == operationId);

            if (operation != null)
            {
                DAL.DeleteOperation(operation.operationId);
                return operation.operationId;
            }

            return 0;
        }

        public string EditPattern(int patternId, string pattern, string pattern_description, string pattern_reply)
        {
            SMSDAL DAL = new SMSDAL();
            var Temppattern = DAL.GetPatternList().FirstOrDefault(u => u.patternId == patternId);
            if (Temppattern == null)
                return "Pattern Not Exist";

            var Temppattern_Check = DAL.GetPatternList().FirstOrDefault(u => u.pattern == pattern);
            if (Temppattern_Check != null)
                return "Pattern Is Used By Another";

            if (!string.IsNullOrEmpty(pattern))
            Temppattern.pattern = pattern;


            if (!string.IsNullOrEmpty(pattern_description))
            Temppattern.patternDescription = pattern_description;


            if (!string.IsNullOrEmpty(pattern_reply))
            Temppattern.replyText = pattern_reply;


            DAL.EditPattern(patternId, pattern, pattern_description, pattern_reply);
            return "OK";
        }

        public string Edit_Operation_Template(int patternId,int operationId, string operation_description, int templateId, string template_text, string template_destination)
        {
            SMSDAL DAL = new SMSDAL();
            var Temppattern = DAL.GetOperationAndTemplate(patternId).FirstOrDefault(u => u.operationId == operationId);
            if (Temppattern == null)
                return "Operation Not Exist";

            var Temppattern_Check = DAL.GetOperationAndTemplate(patternId).FirstOrDefault(u => u.templateId == templateId);
            if (Temppattern_Check== null)
                return "Template Not Exist";

            if (!string.IsNullOrEmpty(operation_description))
                Temppattern.operation_description = operation_description;


            if (!string.IsNullOrEmpty(template_text))
                Temppattern.operation_template_text = template_text;


            if (!string.IsNullOrEmpty(template_destination))
                Temppattern.operation_template_destination = template_destination;

            DAL.EditOperation(operationId, operation_description);
            DAL.EditTemplate(templateId, template_text, template_destination);
            return "OK";
        }

        public string Edit_Operation(int patternId, int operationId, string operation_description)
        {
            SMSDAL DAL = new SMSDAL();
            var Temppattern = DAL.GetOperationAndTemplate(patternId).FirstOrDefault(u => u.operationId == operationId);
            if (Temppattern == null)
                return "Operation Not Exist";

            if (!string.IsNullOrEmpty(operation_description))
                Temppattern.operation_description = operation_description;

            DAL.EditOperation(operationId, operation_description);
            return "OK";
        }
   

    }

}

