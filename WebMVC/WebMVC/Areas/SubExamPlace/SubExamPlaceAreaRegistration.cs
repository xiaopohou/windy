using System.Web.Mvc;

namespace Windy.WebMVC.Areas.SubExamPlace
{
    public class SubExamPlaceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SubExamPlace";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SubExamPlace_default",
                "SubExamPlace/{controller}/{action}/{id}",
                new { namespaces = "Windy.WebMVC.Areas.SubExamPlace.Controllers", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
