using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPLUSPLUS.Controllers
{
    public class UserManualController : Controller
    {
        //
        // GET: /UserManual/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetGift() {
            return View();

        }

        public ActionResult GiftDelivery()
        {
            return View();
        }

        public ActionResult PaymentTerms()
        {
            return View();
        }
        public ActionResult NormalGifts()
        {
            return View();
        }
        public ActionResult BoronzeGifts()
        {
            return View();
        }
        public ActionResult SilverGifts()
        {
            return View();
        }
        public ActionResult GoldGifts()
        {
            return View();
        }

    }
}
