using System.ComponentModel.DataAnnotations;

namespace GIFU.Models
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Passwd { get; set; }
        public string ReturnUrl { get; set; }
        public string ShaPasswd { get; set; }
    }
}