﻿using System.IO;
using System.Web;

namespace GIFU.Tools
{
    public class AttachmentHandler
    {
        /// <summary>
        /// 取得路徑名稱
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public string GetFolderPath(string serverPath, string goodId)
        {
            string path = Path.Combine(serverPath, goodId);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }


        public Models.ResultVM SaveFile(HttpPostedFileBase file, string AbsolutePath, string id)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = GetFolderPath(AbsolutePath, id);
                    path = Path.Combine(path, fileName);
                    file.SaveAs(path);
                    return new Models.ResultVM() { result = 1, messange = "上傳完成" };
                }
            }
            return new Models.ResultVM() { result = -1, messange = "上傳失敗" };
        }

        /// <summary>
        /// 檢查檔案
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public string CheckFile(HttpPostedFileBase file)
        {
            string res = string.Empty;
            if (file != null)
            {
                //檔案Size(最多1MB)
                if (file.ContentLength > 1024 * 1024 * 2)
                {
                    res = "Error:檔案大小超過上限";
                }
                //檔案類型
                string extension = Path.GetExtension(file.FileName);
                if (extension != ".png" && extension != ".jpeg"
                    && extension != ".jpg" && extension != ".bmp"
                    && extension != ".gif" && extension != ".mpg"
                    && extension != ".mpeg")
                {
                    res = "Error:檔案類型限制:jpg, jpeg, gif, bmp, png, tif, tiff, doc, docx, xls, xlsx,txt, rtf, ppt, pptx, pdf, mpg, mpeg, avi, wmv, rm, mov, mkv, dat, 3gp, mp3, mp4, wav, zip, rar, 7z";
                }
            }

            return res;
        }
    }
}