using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Web;


namespace BaseRepository.Filters
{
    //public class Authorization : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {

    //        if (HttpContext.Session["User"] == null)
    //        {
    //            filterContext.Result = new RedirectToRouteResult(
    //                  new RouteValueDictionary
    //                {
    //                    {"controller", "Login"},
    //                    {"action", "Index"}
    //                });
    //        }

    //    }
    //}
    //public class Authentication : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {

    //        if (HttpContext.Current.Request.ServerVariables["http_referer"] == null)
    //        {
    //            filterContext.Result = new RedirectToRouteResult(
    //                  new RouteValueDictionary
    //                {
    //                    {"controller", "Login"},
    //                    {"action", "Index"}
    //                });
    //        }

    //    }
    //}

}