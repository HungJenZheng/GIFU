using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace GIFU.Controllers
{
    public class StoreController : Controller
    {
        private Models.GoodsServices goodsServices = new Models.GoodsServices();
        private Models.OrderServices orderServices = new Models.OrderServices();

        // GET: Store
        public ActionResult Index()
        {
            ViewBag.Recommend = goodsServices.GetRecommendGoods();
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
                Models.Goods goods = goodsServices.GetGoodDetailByGoodsId(goodId);
                ViewBag.Pictures = goodsServices.GetGoodPicturePathById(goodId);
                ViewBag.Messages = goodsServices.GetGoodsMessagesById(goodId);
                ViewBag.Recommend = goodsServices.GetRecommendGoods();
                return View(goods);
            }
            return RedirectToAction("GoodsSearch", "Store");
        }

        [HttpPost]
        public JsonResult GetGoodsByConditions(Models.GoodsSearchArg arg)
        {
            var goods = goodsServices.GetGoodsByConditions(arg);
            return this.Json(goods);
        }

        [HttpPost]
        public ActionResult GetGoodsByConditionsReturnView(Models.GoodsSearchArg arg)
        {
            var goods = goodsServices.GetGoodsByConditions(arg);
            return PartialView("_GoodsBox", goods);
        }

        [HttpPost]
        public JsonResult PlaceAMessage(Models.GoodsMessage msg)
        {
            Models.ResultVM resultVM = Variable.GetResult(-1, "請稍後嘗試");
            if (ModelState.IsValid)
            {
                int result = goodsServices.AddGoodsMessage(msg);
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
        /// 新增物品、圖片
        /// </summary>
        /// <param name="file">上傳的圖片</param>
        /// <param name="goods">物品資訊</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddGoods(HttpPostedFileBase file, Models.Goods goods)
        {
            Tools.AttachmentHandler attachmentHandler = new Tools.AttachmentHandler();
            if (ModelState.IsValid && file != null)
            {
                int goodId = goodsServices.AddGoods(goods);
                Models.ResultVM resultVM;
                //物品新增成功
                if (goodId != 0)
                    resultVM = attachmentHandler.SaveFile(file, Server.MapPath("~/FileUploads"), goodId.ToString());
                else
                    resultVM = Variable.GetResult(-1, "物品新增失敗");
                return this.Json(resultVM);
            }
            return this.Json(Variable.GetResult(-1, "物品資訊輸入有誤"));
        }

        /// <summary>
        /// 上傳物品及多張圖片
        /// </summary>
        /// <param name="files"></param>
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddGoodsWithMultiPicture(IEnumerable<HttpPostedFileBase> files, Models.Goods goods)
        {
            Tools.AttachmentHandler attachmentHandler = new Tools.AttachmentHandler();
            Models.ResultVM resultVM;
            bool error = false;
            string message = "物品新增成功";
            int goodId = 0;
            if (ModelState.IsValid && files != null)
            {
                goodId = goodsServices.AddGoods(goods);
                if (goodId == 0) return this.Json(Variable.GetResult(-1, "物品新增失敗"));
                foreach (var file in files)
                {
                    if (file == null || file.ContentLength <= 0)
                    {
                        error = true;
                        message = "上傳檔案錯誤";
                        break;
                    }
                    string checkMsg = attachmentHandler.CheckFile(file);
                    if (checkMsg != string.Empty)
                    {
                        error = true;
                        message = checkMsg;
                        break;
                    }
                    resultVM = attachmentHandler.SaveFile(file, Server.MapPath(Variable.GetSaveFilePath), goodId.ToString());
                    if (resultVM.result <= 0)
                    {
                        error = true;
                        message = "新增物品錯誤";
                        break;
                    }
                    string path = Path.Combine(Variable.GetSaveFilePath, goodId.ToString(), file.FileName);
                    path = path.Replace("\\", "/").Replace("~", "");
                    goodsServices.AddPicturePath(goodId, path);
                }
            }
            else
            {
                error = true;
                message = "物品資訊輸入有誤";
            }

            if (error)
            {
                goodsServices.DeleteGoods(goodId);
                goodsServices.DeletePicturePathByGoodsId(goodId);
                return this.Json(Variable.GetResult(-1, message + "，請再嘗試一次。"));
            }
            else
                return this.Json(Variable.GetResult(1, message));
        }

        /// <summary>
        /// 更新物品資訊
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateGoods(Models.Goods goods)
        {
            int result = -1;
            string message = "物品資訊輸入有誤";
            if (ModelState.IsValid && goods.GoodId != null)
            {
                result = goodsServices.UpdateGoods(goods);
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
            int result = goodsServices.AddHitCount(goodId);
            return this.Json(result);
        }

        [HttpPost]
        public JsonResult AddGoodsMessage(Models.GoodsMessage msg) //AnswerGoodsMessage
        {
            int result = goodsServices.AddGoodsMessage(msg);
            return this.Json(result);
        }

        [HttpPost]
        public JsonResult AnswerGoodsMessage(Models.GoodsMessage msg) //AnswerGoodsMessage
        {
            int result = goodsServices.AnswerGoodsMessage(msg);
            return this.Json(result);
        }

        [HttpPost]
        public JsonResult UpdateGoodsMessage(Models.GoodsMessage msg)
        {
            int result = goodsServices.UpdateGoodsMessage(msg);
            return this.Json(result);
        }

        [HttpPost]
        public ActionResult GetOrderManageList(Models.OrderGetArg arg)
        {
            List<Models.Order> orders = orderServices.GetOrdersByCondition(arg);
            return PartialView("_OrderManageList", orders);
        }

        [HttpPost]
        public JsonResult UpdateOrderStatus(int orderId, int status)
        {
            int result = -1;
            string message;
            if (orderId != 0 && status != 0)
            {
                result = orderServices.UpdateStatus(orderId, status);
            }
            if (result > 0)
                message = "更新成功。";
            else
                message = "更新失敗，請稍後嘗試";
            return this.Json(Variable.GetResult(result, message));
        }

        [HttpPost]
        public JsonResult GetNewestGoodsByUserId(int userId)
        {
            if (userId != 0)
            {
                Models.Goods goods = goodsServices.GetNewestGoodsByUserId(userId);
                return this.Json(goods);
            }
            return this.Json(new Models.Goods());
        }
    }
}