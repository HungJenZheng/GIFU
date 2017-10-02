using System.ComponentModel.DataAnnotations;

namespace GIFU.Models
{
    public class GoodsMessage
    {
        [Required]
        public int? GoodId { get; set; }
        public int? CommentNo { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public string Message { get; set; }
        public string Time { get; set; }
    }
}