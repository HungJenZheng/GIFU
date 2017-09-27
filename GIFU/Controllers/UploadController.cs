using System.IO;
using System.Web;
using System.Web.Mvc;

namespace GIFU.Controllers
{
    public class UploadController : Controller
    {
        private Tools.AttachmentHandler attachmentHandler = new Tools.AttachmentHandler();
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, FormCollection forms)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = attachmentHandler.GetFolderPath(Server.MapPath("~/FileUploads"), "123");
                    path = Path.Combine(path, fileName);
                    file.SaveAs(path);
                }
            }
            return RedirectToAction("Index");
        }
    }
}