using System.Web.Mvc;

namespace GPLUSPLUS.Areas.eshop
{
    public class eshopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "eshop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "eshop_default",
                "eshop/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "GPLUSPLUS.Areas.eshop.Controllers" }
            );
        }
    }
}
