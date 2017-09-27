using System.ComponentModel.DataAnnotations;

namespace GIFU.Models
{
    public class Goods
    {
        public int? GoodId { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string Title { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string Introduction { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public int NewDegree { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string Tag1 { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string Tag2 { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string IsReason { get; set; }
        public int HitCount { get; set; }
        public string PicPath { get; set; }
        public string UpdateDate { get; set; }
    }
}