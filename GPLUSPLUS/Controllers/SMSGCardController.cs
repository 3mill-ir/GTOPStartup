using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;

namespace GTOPCard.Controllers
{
    [AllowAnonymous]
    public class SMSGCardController : Controller
    {
        //
        // GET: /SMSGCard/

        //public ActionResult Index(string from, string to, string message)
        //{
        //    Operation_SMS_BLL BLL = new Operation_SMS_BLL();
        //    ViewBag.SMS_result = BLL.SMS_Recievier(message, from, to);
        //    return View();
        //}
        //public ActionResult pIndex()
        //{
        //    var req = (HttpWebRequest)WebRequest.Create("http://www.emtiaz.gtopmarketing.ir/Account/RegisterTempedUsers?SecureString=PARSA&ID=" + "09141863449" + "&SecureCode=" + "5858");
        //    req.GetResponse();
        //    return View();
        //}

        //public ActionResult pindex()
        //{
        //    var req = (HttpWebRequest)WebRequest.Create("http://www.emtiaz.gtopmarketing.ir/SMSGCard/");
        //    string data = "something you need to send";
        //    byte[] postBytes = Encoding.ASCII.GetBytes(data);
        //    req.ContentLength = 0;
        //    req.Method = "POST";
        //    req.GetResponse();
        //    return View();
        //}
        //public string pipo()
        //{
        //    return "ko";
        //}
 
    

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index()
        {
            ////return RedirectToAction("SMSReciver", "Home", new { SecureString = "PARSADORSA", msg = "*", from = "09141863449" });

            //var req = (HttpWebRequest)WebRequest.Create("http://www.emtiaz.gtopmarketing.ir/home/SMSReciver?SecureString=PARSADORSA&msg=1&from=09141863449");
            //req.GetResponse();
            //return View();
            bool blnLang = false;
            string strIncomingMessage = "";
            string element = string.Empty;
            string strTo = "";
            string strFrom = "";
            string strMessage = "";
            if (Request.InputStream != null)
            {

                try
                {
                    StreamReader MyStreamReader = new StreamReader(Request.InputStream);
                    strIncomingMessage = MyStreamReader.ReadToEnd();
                    // Create an XmlReader
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.XmlResolver = null;
                    settings.ProhibitDtd = false;
                    XmlReader objXMLReader = XmlReader.Create(new StringReader(strIncomingMessage), settings);
                    try
                    {
                        try
                        {
                            while (objXMLReader.Read())
                            {
                                switch (objXMLReader.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        element = objXMLReader.Name;
                                        if (element == "destAddrNbr")
                                        {
                                            strTo = objXMLReader.Value;
                                        }
                                        if (element == "origAddr")
                                        {
                                            strFrom = objXMLReader.Value;
                                        }
                                        if (element == "message")
                                        {
                                            strMessage = objXMLReader.Value;
                                        }

                                        break;
                                    case XmlNodeType.Text:
                                        if (element == "destAddrNbr")
                                        {
                                            strTo = objXMLReader.Value;
                                            break;
                                        }
                                        if (element == "origAddr")
                                        {
                                            strFrom = objXMLReader.Value;
                                            break;
                                        }
                                        if (element == "message")
                                        {
                                            strMessage = objXMLReader.Value;
                                            break;
                                        }
                                        break;
                                    case XmlNodeType.CDATA:
                                        if (element == "origAddr")
                                        {
                                            strFrom = objXMLReader.Value;
                                        }
                                        if (element == "message")
                                        {
                                            strMessage = objXMLReader.Value;
                                        }
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //You can log error!
                            return View();
                        }

                        try
                        {
                            if (strTo.Trim() != string.Empty)
                            {
                                if (strTo.Substring(0, 2) == "98")
                                {
                                    strTo = strTo.Remove(0, 2);
                                }
                                if (strTo.Substring(0, 3) == "+98")
                                {
                                    strTo = strTo.Remove(0, 3);
                                }
                                if (strTo.Substring(0, 3) == "098")
                                {
                                    strTo = strTo.Remove(0, 3);
                                }
                                if (strTo.Substring(0, 4) == "0098")
                                {
                                    strTo = strTo.Remove(0, 4);
                                }
                            }
                            else
                            {
                                //You can log error!

                                ViewBag.lblError = "فيلد شماره گيرنده خالي است";
                                return View();
                            }
                        }
                        catch (Exception ex)
                        {

                            //You can log error!
                            ViewBag.lblError = ex.Message;
                            return View();
                        }

                        try
                        {

                            if (strFrom.Trim() != string.Empty)
                            {
                                if (strFrom.Substring(0, 2) == "98")
                                {
                                    strFrom = "0" + strFrom.Remove(0, 2);
                                }
                                if (strFrom.Substring(0, 3) == "+98")
                                {
                                    strFrom = "0" + strFrom.Remove(0, 3);
                                }
                                if (strFrom.Substring(0, 3) == "098")
                                {
                                    strFrom = "0" + strFrom.Remove(0, 3);
                                }
                                if (strFrom.Substring(0, 4) == "0098")
                                {

                                    strFrom = "0" + strFrom.Remove(0, 4);
                                }
                            }
                            else
                            {
                                //You can log error!
                                ViewBag.lblError = "فيلد شماره فرستنده خالي است";
                            }
                        }
                        catch (Exception ex)
                        {
                            //You can log error!
                            ViewBag.lblError = ex.Message;
                            return View();
                        }

                        try
                        {
                            if (strIncomingMessage.IndexOf("binary=") != -1)
                            {
                                int index = strIncomingMessage.IndexOf("binary=") + 8;
                                string strLang = strIncomingMessage.Substring(index, 1);
                                if (strLang == "f")
                                {
                                    blnLang = false;
                                }
                                else if (strLang == "t")
                                {
                                    blnLang = true;
                                }
                                ViewBag.txtsendernumber = strFrom;
                            }
                            if (blnLang == true)
                            {
                                strMessage = DecodeUCS2(strMessage);
                            }


                            if (strMessage != "" && strMessage != " " && strMessage != "null" && strMessage.Trim() != String.Empty && strMessage != "(empty)" && strMessage != "⡥浰瑹)")
                            {
                                ViewBag.txtMessage = strMessage;
                            }
                            else
                                return View();
                        }

                        catch (Exception ex)
                        {
                            //You can log error!
                            ViewBag.lblError = ex.Message;
                            return View();
                        }
                    }

                    catch (Exception ex)
                    {
                        //You can log error!
                        ViewBag.lblError = ex.Message;
                        return View();
                    }
                    //Operation_SMS_BLL BLL = new Operation_SMS_BLL();
                    //ViewBag.SMS_result = BLL.SMS_Recievier(strMessage, strFrom, strTo);

                    //Himan New Comment for new year
                    strMessage.Replace("#", "%23");
                    var req = (HttpWebRequest)WebRequest.Create("http://www.emtiaz.gtopmarketing.ir/home/SMSReciver?SecureString=PARSADORSA&msg=" + Server.UrlEncode(strMessage) + "&from=" + strFrom);
                    req.GetResponse();

                 //return   RedirectToAction("SMSReciver", "Home", new { SecureString = "PARSADORSA", msg = strMessage, from = strFrom });
                }
                catch (Exception ex)
                {
                    //You can log error!
                    ViewBag.lblError = ex.Message;
                    return View();
                }
            }

            return View();
        }
        public string DecodeUCS2(string Content)
        {
            string hextext = Content;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hextext.Length; i += 4)
            {
                string hs = hextext.Substring(i, 4);
                sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
            }
            string ascii = sb.ToString();
            return ascii;
        }

        //public ActionResult send()
        //{

        //    var url = "http://localhost:2892/smsgcard";
        //  var postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\" standalone=\"no\"?><smsDeliver batchID=\"-1535647542\" binary=\"false\" company=\"RAHYAB\"><destAddr>IRANCELL-02</destAddr><destAddrNbr>10006020</destAddrNbr><origAddr>09141863449</origAddr><message>*</message></smsDeliver>";
        ////   var postData = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><xml>...</xml>";
        //    byte[] bytes = System.Text.Encoding.ASCII.GetBytes(postData);

        //    var req = (HttpWebRequest)WebRequest.Create(url);

        //    req.ContentType = "text/xml";
        //    req.Method = "POST";
        //    req.ContentLength = bytes.Length;

        //    using (Stream os = req.GetRequestStream())
        //    {
        //        os.Write(bytes, 0, bytes.Length);
        //    }

        //    string response = "";

        //    using (System.Net.WebResponse resp = req.GetResponse())
        //    {
        //        using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
        //        {
        //            response = sr.ReadToEnd().Trim();
        //        }
        //    }

        //    return View();
        //}
    }
}