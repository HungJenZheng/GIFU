using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace GIFU.Controllers
{
    public class StoreController : Controller
    {
        private Models.GoodsServices goodServices = new Models.GoodsServices();
        private Tools.AttachmentHandler attachmentHandler = new Tools.AttachmentHandler();

        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GoodsSearch()
        {
            return View();
        }

        public ActionResult GoodsDetail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetGoodsByConditions(Models.GoodsSearchArg arg)
        {
            var goods = goodServices.GetGoodsByConditions(arg);
            return this.Json(goods);
        }

        [HttpPost]
        public JsonResult GetGoodDetailById(string goodId)
        {
            int id = Convert.ToInt32(goodId);
            var good = goodServices.GetGoodDetailById(id);
            return this.Json(good);
        }

        [HttpPost]
        public JsonResult GetGoodPicturePathById(string goodId)
        {
            int id = Convert.ToInt32(goodId);
            var goodPics = goodServices.GetGoodPicturePathById(id);
            return this.Json(goodPics);
        }

        /// <summary>
        /// 新增商品、圖片
        /// </summary>
        /// <param name="file">上傳的圖片</param>
        /// <param name="good">商品資訊</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddGoods(HttpPostedFileBase file, Models.Goods good)
        {
            if (ModelState.IsValid && file != null)
            {
                int goodId = goodServices.AddGood(good);
                Models.ResultVM resultVM;
                //商品新增成功
                if (goodId != 0)
                    resultVM = attachmentHandler.SaveFile(file, Server.MapPath("~/FileUploads"), goodId.ToString());
                else
                    resultVM = Variable.GetResult(-1, "商品新增失敗");
                return this.Json(resultVM);
            }
            return this.Json(Variable.GetResult(-1, "商品資訊輸入有誤"));
        }

        [HttpPost]
        public JsonResult AddGoodsWithMultiPicture(IEnumerable<HttpPostedFileBase> files, Models.Goods good)
        {
            Models.ResultVM resultVM;
            if (ModelState.IsValid && files != null)
            {
                int goodId = goodServices.AddGood(good);
                if (goodId == 0) return this.Json(Variable.GetResult(-1, "商品新增失敗"));
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        resultVM = attachmentHandler.SaveFile(file, Server.MapPath(Variable.GetSaveFilePath), goodId.ToString());
                        if (resultVM.result > 0)
                        {
                            string path = Path.Combine("..", goodId.ToString(), file.FileName);
                            path = path.Replace("\\", "/");
                            goodServices.AddPicturePath(goodId, path);
                        }
                        else
                        {
                            return this.Json(Variable.GetResult(-1, "新增商品錯誤"));
                        }
                    }
                    else
                    {
                        return this.Json(Variable.GetResult(-1, "上傳檔案錯誤"));
                    }
                }
            }
            else
            {
                return this.Json(Variable.GetResult(-1, "商品資訊輸入有誤"));
            }
            return this.Json(Variable.GetResult(1, "商品新增成功"));
        }

        /// <summary>
        /// 更新商品資訊
        /// </summary>
        /// <param name="good"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateGood(Models.Goods good)
        {
            if (ModelState.IsValid && good.GoodId != null)
            {
                int result = goodServices.UpdateGood(good);
                Models.ResultVM resultVM;
                if (result > 0)
                    resultVM = Variable.GetResult(result, good.Title + "更新成功");
                else
                    resultVM = Variable.GetResult(result, good.Title + "更新失敗");
                return this.Json(resultVM);
            }
            return this.Json(Variable.GetResult(-1, "商品資訊輸入有誤"));
        }
    }
}