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
        private Models.OrderServices orderServices = new Models.OrderServices();
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

        public ActionResult GoodsDetail(string id)
        {
            if (id != null)
            {
                int goodId = Convert.ToInt32(id);
                Models.Goods goods = goodServices.GetGoodDetailById(goodId);
                ViewBag.Pictures = goodServices.GetGoodPicturePathById(goodId);
                ViewBag.Messages = goodServices.GetGoodsMessagesById(goodId);
                return View(goods);
            }
            return RedirectToAction("GoodsSearch", "Store");
        }

        [HttpPost]
        public JsonResult GetGoodsByConditions(Models.GoodsSearchArg arg)
        {
            var goods = goodServices.GetGoodsByConditions(arg);
            return this.Json(goods);
        }

        [HttpPost]
        public ActionResult GetGoodsByConditionsReturnView(Models.GoodsSearchArg arg)
        {
            var goods = goodServices.GetGoodsByConditions(arg);
            return PartialView("_GoodsBox", goods);
        }

        [HttpPost]
        public JsonResult PlaceAMessage(Models.GoodsMessage msg)
        {
            Models.ResultVM resultVM = Variable.GetResult(-1, "請稍後嘗試");
            if (ModelState.IsValid)
            {
                int result = goodServices.AddGoodsMessage(msg);
                if (result > 0)
                {
                    resultVM.result = result;
                    resultVM.message = "留言成功";
                }
            }
            return this.Json(resultVM);
        }

        [HttpPost]
        public JsonResult AddAnOrder(Models.Order order)
        {
            Models.ResultVM resultVM = Variable.GetResult(-1, "請稍後嘗試");
            if (ModelState.IsValid)
            {
                int result = orderServices.AddOrder(order);
                if (result > 0)
                {
                    resultVM.result = result;
                    resultVM.message = "請等候贈與方確認";
                }
            }
            return this.Json(resultVM);
        }

        /// <summary>
        /// 新增商品、圖片
        /// </summary>
        /// <param name="file">上傳的圖片</param>
        /// <param name="goods">商品資訊</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddGoods(HttpPostedFileBase file, Models.Goods goods)
        {
            if (ModelState.IsValid && file != null)
            {
                int goodId = goodServices.AddGood(goods);
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

        /// <summary>
        /// 上傳商品及多張圖片
        /// </summary>
        /// <param name="files"></param>
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddGoodsWithMultiPicture(IEnumerable<HttpPostedFileBase> files, Models.Goods goods)
        {
            Models.ResultVM resultVM;
            if (ModelState.IsValid && files != null)
            {
                int goodId = goodServices.AddGood(goods);
                if (goodId == 0) return this.Json(Variable.GetResult(-1, "商品新增失敗"));
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        resultVM = attachmentHandler.SaveFile(file, Server.MapPath(Variable.GetSaveFilePath), goodId.ToString());
                        if (resultVM.result > 0)
                        {
                            string path = Path.Combine(goodId.ToString(), file.FileName);
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
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateGood(Models.Goods goods)
        {
            int result = -1;
            string message = "商品資訊輸入有誤";
            if (ModelState.IsValid && goods.GoodId != null)
            {
                result = goodServices.UpdateGood(goods);
                if (result > 0)
                    message = goods.Title + "更新成功";
                else
                    message = goods.Title + "更新失敗";
            }
            return this.Json(Variable.GetResult(result, message));
        }

        [HttpPost]
        public JsonResult AddHitCount(int? goodId)
        {
            int result = goodServices.AddHitCount(goodId);
            return this.Json(result);
        }
    }
}