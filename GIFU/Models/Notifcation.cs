using System;
using System.ComponentModel;

namespace GIFU.Models
{
    public class Notification
    {
        [DisplayName("訊息ID")]
        public int NotificationId { get; set; }
        [DisplayName("接收人ID")]
        public int ReceiveId { get; set; }
        [DisplayName("寄送人")]
        public int SendId { get; set; }
        [DisplayName("內容")]
        public string Content { get; set; }
        [DisplayName("商品ID")]
        public int GoodId { get; set; }
        [DisplayName("網址")]
        public string Url { get; set; }
        [DisplayName("時間")]
        public DateTime Time { get; set; }
        [DisplayName("圖片網址")]
        public string ImageUrl { get; set; }
    }
}