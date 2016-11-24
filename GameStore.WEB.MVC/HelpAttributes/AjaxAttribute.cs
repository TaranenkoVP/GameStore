using System.Reflection;
using System.Web.Mvc;

namespace GameStore.WEB.MVC.HelpAttributes
{
    public class AjaxAttribute : ActionMethodSelectorAttribute
    {
        public AjaxAttribute()
        {
            ajax = true;
        }

        public AjaxAttribute(bool a)
        {
            ajax = a;
        }

        public bool ajax { get; set; }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return ajax == controllerContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}