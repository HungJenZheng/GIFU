using System.ComponentModel.DataAnnotations;

namespace GIFU.Models
{
    public class GoodsMessage
    {
        public int? GoodId { get; set; }
        public int? CommentNo { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string Type { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public int? UserId { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string Message { get; set; }
        public string Time { get; set; }
    }
}