using System.ComponentModel.DataAnnotations;

namespace GIFU.Models
{
    public class Account
    {
        public int? UserId { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string Email { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string Passwd { get; set; }
        [Required(ErrorMessage = "此欄位必填")]
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int ProvideCount { get; set; }
        public string PhotoType { get; set; }
        public string IsValid { get; set; }
        public string UserType { get; set; }
        public string UpdateDate { get; set; }
        public string Token { get; set; }
    }
}