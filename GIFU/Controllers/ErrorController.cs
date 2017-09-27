using System;
using System.Web.Mvc;

namespace GIFU.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult General(Exception exception)
        {
            return Content("General failure", "text/plain");
        }

        public ActionResult Http404()
        {
            return Content("Not found", "text/plain");
        }

        public ActionResult Http403()
        {
            return Content("Forbidden", "text/plain");
        }

        public ActionResult UnknownError()
        {
            return Content("UnknownError", "text/plain");
        }

        public ActionResult FileTooLarge()
        {
            return Content("ErrorFileTooLarge", "text/plain");
        }
    }
}