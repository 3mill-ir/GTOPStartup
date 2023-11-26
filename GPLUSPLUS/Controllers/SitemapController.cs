using SimpleMvcSitemap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GPLUSPLUS.Controllers
{
    public class SitemapController : Controller
    {
        //
        // GET: /Sitemap/

        public ActionResult Index()
        {
            List<SitemapNode> nodes = new List<SitemapNode>
        {
            new SitemapNode(Url.Action("Index","Home")){
    ChangeFrequency = ChangeFrequency.Weekly,
    LastModificationDate = DateTime.UtcNow,
    Priority = 1M
}
            //other nodes
        };

            return new SitemapProvider().CreateSitemap(HttpContext, nodes);
        }

    }
}
