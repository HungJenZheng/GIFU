﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GIFU
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    int maxFileSize = Variable.GetMaxRequestLength().MaxRequestLength * 1024;
        //    if (Request.ContentLength > maxFileSize)
        //    {
        //        Response.Redirect("~/Error/FileTooLarge");
        //    }
        //}

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            Response.Clear();
            Server.ClearError();
            var routeData = new RouteData();
            routeData.Values["controller"] = "Errors";
            routeData.Values["action"] = "General";
            routeData.Values["exception"] = exception;
            Response.StatusCode = 500;
            if (httpException != null)
            {
                Response.StatusCode = httpException.GetHttpCode();
                switch (Response.StatusCode)
                {
                    case 403:
                        routeData.Values["action"] = "Http403";
                        break;
                    case 404:
                        routeData.Values["action"] = "Http404";
                        break;
                    default:
                        routeData.Values["action"] = "UnknownError";
                        break;
                }
            }

            IController errorsController = new Controllers.ErrorController();
            var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
            errorsController.Execute(rc);
        }
    }
}