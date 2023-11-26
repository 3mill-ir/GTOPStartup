using System.Web.Mvc;

namespace GPLUSPLUS.Areas.seller
{
    public class sellerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "seller";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "seller_default",
                "seller/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                  namespaces: new[] { "GPLUSPLUS.Areas.seller.Controllers" }
            );
        }
    }
}
