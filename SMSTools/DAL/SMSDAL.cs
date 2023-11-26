using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSTools.DAL
{
    class SMSDAL
    {

        public void SaveOutbox(string text, string to, string status1, string status2)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {

                SMSOutbox outbox = new SMSOutbox();
                outbox.Text = text;
                outbox.SMS_To = to;
                outbox.SendDate = DateTime.Now;
                outbox.StatusOne = status1;
                outbox.StatusOne = status2;
                smsdb.SMSOutboxes.Add(outbox);

                smsdb.SaveChanges();
            }
        }


        public void SaveOutbox(string text, string to, string status1, string status2, int inboxId)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {

                SMSOutbox outbox = new SMSOutbox();
                outbox.Text = text;
                outbox.SMS_To = to;
                outbox.SendDate = DateTime.Now;
                outbox.StatusOne = status1;
                outbox.StatusOne = status2;
                smsdb.SMSOutboxes.Add(outbox);



                SMS_Inbox_Outbox_Mapping iom = new SMS_Inbox_Outbox_Mapping();
                iom.InboxId = inboxId;
                iom.OutboxId = outbox.Id;
                smsdb.SMS_Inbox_Outbox_Mapping.Add(iom);
                smsdb.SaveChanges();
            }
        }

        /// <summary>
        /// zakhireye sms e ersali
        /// </summary>
        /// <param name="text">matne sms</param>
        /// <param name="to">shomareye girande</param>
        /// <param name="status1">gozareshe ersal 1</param>
        /// <param name="status2">gpzareshe ersal 2</param>
        /// <param name="operationId"> Id ie Operation dar soorati ke az ghabl operation e khasi tarif shode bashad, meghdare null baraye halate kolli</param>
        /// <param name="inboxId"> Id ie peighame daryafti, dar soorati ke sms dar jawabe sms e daryaftie khasi bashad, meghdare null baraye halate kolli</param>
        public void SaveOutbox(string text, string to, string status1, string status2, int operationId, int inboxId)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {

                SMSOutbox outbox = new SMSOutbox();
                outbox.Text = text;
                outbox.SMS_To = to;
                outbox.SendDate = DateTime.Now;
                outbox.StatusOne = status1;
                outbox.StatusOne = status2;
                smsdb.SMSOutboxes.Add(outbox);
                smsdb.SaveChanges();

                SMS_Outbox_Operation_Mapping oom = new SMS_Outbox_Operation_Mapping();
                oom.OperationId = operationId;
                oom.OutboxId = outbox.Id;
                smsdb.SMS_Outbox_Operation_Mapping.Add(oom);
                smsdb.SaveChanges();


                SMS_Inbox_Outbox_Mapping iom = new SMS_Inbox_Outbox_Mapping();
                iom.InboxId = inboxId;
                iom.OutboxId = outbox.Id;
                smsdb.SMS_Inbox_Outbox_Mapping.Add(iom);
                smsdb.SaveChanges();
            }
        }


        public int SaveInbox(string text, string from)
        {
            int inboxId;
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSInbox inbox = new SMSInbox();
                inbox.RecievedDate = DateTime.Now;
                inbox.SMS_From = from;
                inbox.Text = text;
                smsdb.SMSInboxes.Add(inbox);

                smsdb.SaveChanges();
                inboxId = inbox.Id;
            }
            return inboxId;
        }

        /// <summary>
        /// zakhireye sms daryafti
        /// </summary>
        /// <param name="text">matne sms</param>
        /// <param name="from">shomareye ferestandeye sms</param>
        /// <param name="patternId">Id ie pattern sms .dar soorati ke az hich ologooee peiravi nakonad null mibashad.</param>
        public int SaveInbox(string text, string from, int patternId)
        {
            int inboxId;
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSInbox inbox = new SMSInbox();
                inbox.RecievedDate = DateTime.Now;
                inbox.SMS_From = from;
                inbox.Text = text;
                smsdb.SMSInboxes.Add(inbox);

                smsdb.SaveChanges();
                inboxId = inbox.Id;

                SMS_Inbox_Pattern_Mapping ipm = new SMS_Inbox_Pattern_Mapping();
                ipm.InboxId = inbox.Id;
                ipm.PatternId = patternId;
                smsdb.SMS_Inbox_Pattern_Mapping.Add(ipm);

                smsdb.SaveChanges();

            }
            return inboxId;
        }



        public void SaveNewPattern_Operation_Forward(string sms_pattern, string pattern_description, string replytext, List<string> operation_description, List<string> forward_destination, List<string> forward_template)
        {

            int patternId = SaveNewPattern_NoOperation(sms_pattern, pattern_description, replytext);
            AddOperation_Forward(patternId, operation_description, forward_destination, forward_template);

        }


        public void SaveNewPattern_Operation_(string sms_pattern, string pattern_description, string replytext,string opeartion, string operation_description)
        {

            int patternId = SaveNewPattern_NoOperation(sms_pattern, pattern_description, replytext);
            AddOperation(patternId,opeartion, operation_description);

        }


        public void EditPattern_ForwardOperation(int patternId, string sms_pattern, string pattern_description, string replytext, Collection<OperationsAndTemplates> operationlist)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSPattern pattern = smsdb.SMSPatterns.FirstOrDefault(u => u.Id == patternId);
                if (pattern != null)
                {
                    pattern.Pattern = sms_pattern;
                    pattern.Description = pattern_description;
                    pattern.ReplyText = replytext;


                    foreach (var op in operationlist)
                    {

                        SMSOperation operation = smsdb.SMSOperations.FirstOrDefault(u => u.Id == op.operationId);
                        if (operation != null)
                        {
                            operation.Operation = op.operation;
                            operation.Description = op.operation_description;
                            if (!string.IsNullOrEmpty(op.operation_template_destination))
                            {

                                SMSTemplate template = smsdb.SMSTemplates.FirstOrDefault(u => u.Id == op.templateId);
                                template.DestinationNumber = op.operation_template_destination;
                                template.Text = op.operation_template_text;

                            }

                        }

                    }
                    smsdb.SaveChanges();
                }
            }
        }

        public void DeletePattern(int patternId)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSPattern pattern = smsdb.SMSPatterns.FirstOrDefault(u => u.Id == patternId);
                if (pattern != null)
                {
                    pattern.IsDeleted = true;

                    foreach (var op in pattern.SMSOperations)
                    {
                        op.IsDeleted = true;
                        foreach (var tem in op.SMSTemplates)
                        {
                            tem.IsDeleted = true;
                        }
                    }
                    smsdb.SaveChanges();
                }
            }
        }

        public void DeleteOperation(int operationId)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSOperation operation = smsdb.SMSOperations.FirstOrDefault(u => u.Id == operationId);
                if (operation != null)
                {
                    operation.IsDeleted = true;
                    foreach (var tem in operation.SMSTemplates)
                    {
                        tem.IsDeleted = true;
                    }
                    smsdb.SaveChanges();
                }
            }
        }



        public int SaveNewPattern_NoOperation(string sms_pattern, string pattern_description, string replytext)
        {

            int patternId = 0;
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSPattern pattern = new SMSPattern();
                pattern.CreateDate = DateTime.Now;
                pattern.Description = pattern_description;
                pattern.Pattern = sms_pattern;
                pattern.ReplyText = replytext;
                pattern.IsDeleted = false;
                smsdb.SMSPatterns.Add(pattern);

                smsdb.SaveChanges();
                patternId = pattern.Id;

            }

            return patternId;

        }

        public void AddOperation(int patternId, string operation, string operation_description) {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSOperation Operation = new SMSOperation();
                Operation.CreateDate = DateTime.Now;
                Operation.Description = operation_description;
                Operation.Operation = operation;
                Operation.PatternId = patternId;
                Operation.IsDeleted = false;
                smsdb.SMSOperations.Add(Operation);
                smsdb.SaveChanges();
            }
        }


        public void AddOperation_Forward(int patternId, List<string> operation_description, List<string> forward_destination, List<string> forward_template)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {

                var forward_template_list = operation_description.Zip(forward_destination.Zip(forward_template, (b, c) => new { b, c }),
                         (a, b) => new { description = a, forward = b.b, template = b.c })
                    .ToList();


                foreach (var ftl in forward_template_list)
                {
                    SMSOperation operation = new SMSOperation();
                    operation.CreateDate = DateTime.Now;
                    operation.Description = ftl.description;
                    operation.Operation = "FORWARD";
                    operation.PatternId = patternId;
                    operation.IsDeleted = false;
                    smsdb.SMSOperations.Add(operation);
                    smsdb.SaveChanges();

                    SMSTemplate template = new SMSTemplate();
                    template.CreateDate = DateTime.Now;
                    template.Text = ftl.template;
                    template.DestinationNumber = ftl.forward;
                    template.OperationId = operation.Id;
                    template.IsDeleted = false;
                    smsdb.SMSTemplates.Add(template);
                    smsdb.SaveChanges();
                }
              
            }
        }




        /// <summary>
        /// liste pattern haye movjood ra be hamrahe Idie har pattern bar migardanad
        /// </summary>
        /// <returns>liste pattern va id ie marboot be har pattern</returns>
        public Collection<Patterns> GetPatternList()
        {
            Collection<Patterns> patternlist = new Collection<Patterns>();
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                var query = from c in smsdb.SMSPatterns where (c.IsDeleted == false) select c;
                foreach (var q in query)
                {
                    Patterns p = new Patterns();
                    p.patternId = q.Id;
                    p.pattern = q.Pattern;
                    p.patternDescription = q.Description;
                    p.replyText = q.ReplyText;
                    patternlist.Add(p);
                }
            }
            return patternlist;
        }

        public void EditPattern(int patternId, string sms_pattern, string pattern_description,string pattern_reply)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSPattern pattern = smsdb.SMSPatterns.FirstOrDefault(u => u.Id == patternId);

                if (!string.IsNullOrEmpty(sms_pattern))
                    pattern.Pattern = sms_pattern;


                if (!string.IsNullOrEmpty(pattern_description))
                    pattern.Description = pattern_description;


                if (!string.IsNullOrEmpty(pattern_reply))
                    pattern.ReplyText = pattern_reply;


                smsdb.SaveChanges();

            }
        }


        public void EditOperation(int operationId, string operation_description)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSOperation operation = smsdb.SMSOperations.FirstOrDefault(u => u.Id == operationId);
                operation.Description
                    = operation_description;
                smsdb.SaveChanges();

            }
        }



        public void EditTemplate(int templateId, string template_text, string template_destination)
        {
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                SMSTemplate template = smsdb.SMSTemplates.FirstOrDefault(u => u.Id == templateId);
                template.Text = template_text;
                template.DestinationNumber = template_destination;
                smsdb.SaveChanges();

            }
        }









        public Collection<OperationsAndTemplates> GetOperationAndTemplate(int patternId)
        {
            Collection<OperationsAndTemplates> operationlist = new Collection<OperationsAndTemplates>();
            using (SMSDBEntities smsdb = new SMSDBEntities())
            {
                var query = from c in smsdb.SMSOperations where (c.PatternId == patternId && c.IsDeleted == false) select c;
                foreach (var q in query)
                {
                    OperationsAndTemplates operationandtemplate = new OperationsAndTemplates();
                    operationandtemplate.operation = q.Operation;
                    operationandtemplate.operationId = q.Id;
                    operationandtemplate.operation_description = q.Description;
                    var template = (from c in smsdb.SMSTemplates where (c.OperationId == q.Id && c.IsDeleted == false) select new { c.Text, c.DestinationNumber, c.Id }).FirstOrDefault();
                    if (template != null)
                    {
                        operationandtemplate.operation_template_text = template.Text;
                        operationandtemplate.operation_template_destination = template.DestinationNumber;
                        operationandtemplate.templateId = template.Id;
                    }
                    operationlist.Add(operationandtemplate);
                }
            }
            return operationlist;
        }



    }



    public class Patterns
    {
        public int patternId { get; set; }
        public string pattern { get; set; }
        public string replyText { get; set; }
        public string patternDescription { get; set; }
    }

    public class OperationsAndTemplates
    {
        public int operationId { get; set; }
        public string operation { get; set; }
        public int templateId { get; set; }
        public string operation_description { get; set; }
        public string operation_template_destination { get; set; }
        public string operation_template_text { get; set; }
    }
}
