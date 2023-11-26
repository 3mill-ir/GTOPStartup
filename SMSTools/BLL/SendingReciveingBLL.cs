using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;
using SMSTools.DAL;
using System.Net;


namespace SMSTools.BLL
{
    public class SendingReciveingBLL
    {
        string IP = "http://193.104.22.14:2055/CPSMSService/Access";
        string Number = "10006020";
        string UserName = "ATSIGNCO9";
        string Password = "m@hfye4@5";
        string Company = "ATSIGNCO";

        /// <summary>
        /// ersale taki
        /// </summary>
        /// <param name="msg">matne payam</param>
        /// <param name="dest">adrese magsad</param>
        /// <returns>vaziyate payamake ersal shode ke dars surate ersale movafag true mibashad</returns>
        public string[] Send_single(string msg, string dest)
        {
            string[] status = new string[2];
            if (string.IsNullOrEmpty(msg))
            {
                status[0] = "message is empty";
                return status;
            }

            if (string.IsNullOrEmpty(dest))
            {
                status[0] = "destination is empty";
                return status;
            }
            ManagementToolsBLL BLL = new ManagementToolsBLL();
            int msg_part = (int)Math.Ceiling((double)msg.Length / 70);
            int sms_amount;
            if (int.TryParse(BLL.SMS_Amount(), out sms_amount))
            {
                if (sms_amount < msg_part)
                    status[0] = "SMS amount is insufficient";
            }

            Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
            status = sms_Single.SendSMS_Single(msg, dest, Number, UserName, Password, IP, Company, false);
            SMSDAL DAL = new SMSDAL();
            DAL.SaveOutbox(msg, dest, status[0], status[1]);
            return status;

        }



        /// <summary>
        /// ersale guruhi
        /// </summary>
        /// <param name="msg">matne payam</param>
        /// <param name="dest">adrese magsadha</param>
        /// <returns>vaziyate payamake ersal shode ke dars surate ersale movafag true mibashad</returns>
        public string[] Send_batch(string msg, string dest)
        {
            string[] status = new string[2];
            if (string.IsNullOrEmpty(msg))
            {
                status[0] = "message is empty";
                return status;
            }

            if (string.IsNullOrEmpty(dest))
            {
                status[0] = "destination is empty";
                return status;
            }

            List<string> receivers_List = new List<string>();
            receivers_List = dest.Split(',').ToList();
            string[] DestAdd = new string[receivers_List.Count];
            DestAdd = receivers_List.ToArray();

            ManagementToolsBLL BLL = new ManagementToolsBLL();
            int msg_part = (int)Math.Ceiling((double)msg.Length / 70);
            int msg_need = msg_part * DestAdd.Count();
            int sms_amount;
            if (int.TryParse(BLL.SMS_Amount(), out sms_amount))
            {
                if (sms_amount < msg_need)
                    status[0] = "SMS amount is insufficient";
            }


            Cls_SMS.ClsSend sms_Batch = new Cls_SMS.ClsSend();
            status = sms_Batch.SendSMS_Batch(msg, DestAdd, Number, UserName, Password, IP, Company, false);
            SMSDAL DAL = new SMSDAL();

            DAL.SaveOutbox(msg, dest, status[0], status[1]);
            return status;
        }


        /// <summary>
        /// ersale nazir be nazir
        /// </summary>
        /// <param name="msg">matne payamha</param>
        /// <param name="dest">adrese magsadha</param>
        /// <returns>vaziyate payamake ersal shode ke dars surate ersale movafag true mibashad</returns>
        public string[] Send_LikeToLike(string msg, string dest)
        {
            string[] status = new string[2];
            if (string.IsNullOrEmpty(msg))
            {
                status[0] = "message is empty";
                return status;
            }

            if (string.IsNullOrEmpty(dest))
            {
                status[0] = "destination is empty";
                return status;
            }
            List<string> receivers_List = new List<string>();
            receivers_List = dest.Split(',').ToList();
            string[] DestAdd = new string[receivers_List.Count];
            DestAdd = receivers_List.ToArray();

            List<string> Msg_List = new List<string>();
            Msg_List = msg.Split(',').ToList();
            string[] Msg = new string[Msg_List.Count];
            Msg = Msg_List.ToArray();

            if (DestAdd.Count() != Msg.Count())
            {
                status[0] = "message count and destination count is not equal";
                return status;
            }

            ManagementToolsBLL BLL = new ManagementToolsBLL();
            int msg_part = 0;
            foreach (var m in Msg)
            {
                msg_part = msg_part + (int)Math.Ceiling((double)m.Length / 70);
            }
            int msg_need = msg_part * DestAdd.Count();
            int sms_amount;
            if (int.TryParse(BLL.SMS_Amount(), out sms_amount))
            {
                if (sms_amount < msg_need)
                    status[0] = "SMS amount is insufficient";
            }
            Cls_SMS.ClsSend sms = new Cls_SMS.ClsSend();

            status = sms.SendSMS_LikeToLike(Msg, DestAdd, Number, UserName, Password, IP, Company);
            SMSDAL DAL = new SMSDAL();

            DAL.SaveOutbox(msg, dest, status[0], status[1]);
            return status;
        }






        public void Reply(string msg, string dest, int? operationId, int? inboxId)
        {

            SendingReciveingBLL sendingtools = new SendingReciveingBLL();
            SMSDAL DAL = new SMSDAL();

            Cls_SMS.ClsSend sms_Single = new Cls_SMS.ClsSend();
            string[] status = new string[2];
            status = sms_Single.SendSMS_Single(msg, dest, Number, UserName, Password, IP, Company, false);

            if (operationId == null && inboxId == null)
                DAL.SaveOutbox(msg, dest, status[0], status[1]);
            else if (operationId == null && inboxId != null)
                DAL.SaveOutbox(msg, dest, status[0], status[1], inboxId ?? default(int));
            else if (operationId != null && inboxId != null)
                DAL.SaveOutbox(msg, dest, status[0], status[1], operationId ?? default(int), inboxId ?? default(int));
        }




        public void Receive_SMS(string msg, string from)
        {

            int inboxId;
            int? patternId = null;
            Collection<Patterns> patternList = new Collection<Patterns>();
            SMSDAL DAL = new SMSDAL();
            patternList = DAL.GetPatternList();
            foreach (var p in patternList)
            {
                Match match = Regex.Match(msg, p.pattern);
                if (match.Success)
                {
                    patternId = p.patternId;
                    break;
                }
            }

            if (patternId == null)
            {
                inboxId = DAL.SaveInbox(msg, from);
                string UnFormat_Reply = "قالب پیغام نامعتبر است. جهت دریافت راهنمایی * را ارسال نمایید./n مجموعه GTOP";
                Reply(UnFormat_Reply, from, null, null);
            }
            else
            {

                int patternId_value = patternId ?? default(int);
                inboxId = DAL.SaveInbox(msg, from, patternId_value);
                Collection<OperationsAndTemplates> operationList = new Collection<OperationsAndTemplates>();
                operationList = DAL.GetOperationAndTemplate(patternId_value);

                bool isfirst = true;
                foreach (var p in operationList)
                {
                    switch (p.operation)
                    {
                        case "FORWARD":
                            {
                                if (isfirst)
                                {
                                    var pattern = DAL.GetPatternList().FirstOrDefault(u => u.patternId == patternId_value);
                                    Reply(pattern.replyText, from, p.operationId, inboxId);
                                    isfirst = false;
                                }
                                var forwarder_list = DAL.GetOperationAndTemplate(patternId_value);

                                if (!string.IsNullOrEmpty(p.operation_template_destination))
                                {
                                    string temp = p.operation_template_text.Replace("[from]", from);
                                    Send_single(temp, p.operation_template_destination);
                                }


                                break;
                            }

                        case "SUBMITCODE":
                            {
                                string prefix = @"\*";
                                string temp_msg = Regex.Replace(msg, prefix, "");
                                temp_msg = temp_msg.Replace("#", "");
                                var req = (HttpWebRequest)WebRequest.Create("http://www.emtiaz.gtopmarketing.ir/GTOPSMS/SubmitCodeViaSMS?SecureString=PARSADORSA&From=" + from + "&Code=" + temp_msg + "&operationID=" + p.operationId + "&inboxID=" + inboxId);
                                req.GetResponse();
                                break;
                            }
                        case "CODETRACKING":
                            {
                                string prefix = @"\*";
                                string temp_msg = Regex.Replace(msg, prefix, "");
                                temp_msg = temp_msg.Replace("#", "");
                                var req = (HttpWebRequest)WebRequest.Create("http://www.emtiaz.gtopmarketing.ir/GTOPSMS/TrackingCodeViaSMS?SecureString=PARSADORSA&From=" + from + "&TrackingCode=" + temp_msg + "&operationID=" + p.operationId + "&inboxID=" + inboxId);
                                req.GetResponse();
                                break;
                            }
                        case "PASSWORDRESET":
                            {
                                string token = msg.Substring(0, msg.IndexOf("*"));
                                string newpass = msg.Substring(msg.IndexOf("*") + 1, msg.Count() - msg.IndexOf("*")-1 );

                                var req = (HttpWebRequest)WebRequest.Create("http://www.emtiaz.gtopmarketing.ir/Account/ForgetPasswordTokenReciver?SecureString=DORSA&ID=" + from + "&Token=" + token + "&NewPassword=" + newpass + "&operationID=" + p.operationId + "&inboxID=" + inboxId);
                                    req.GetResponse();
                                break;
                            }
                        case "USERCONFIRMATION":
                            {
                                 
                                var req = (HttpWebRequest)WebRequest.Create("http://www.emtiaz.gtopmarketing.ir/Account/RegisterTempedUsers?SecureString=PARSA&ID=" + from + "&SecureCode=" + msg + "&operationID=" + p.operationId + "&inboxID=" + inboxId);
                                req.GetResponse();

                                break;
                            }
                        case "USERSCORE":
                            {

                                var req = (HttpWebRequest)WebRequest.Create("http://www.emtiaz.gtopmarketing.ir/GTOPSMS/UserScoreViaSMS?SecureString=PARSADORSA&From=" + from + "&operationID=" + p.operationId + "&inboxID=" + inboxId);
                                req.GetResponse();

                                break;
                            }
                        case "GUIDE":
                            {
                              
                                    var pattern = DAL.GetPatternList().FirstOrDefault(u => u.patternId == patternId_value);
                                    Reply(pattern.replyText, from, p.operationId, inboxId);
                        
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }

    }
}
