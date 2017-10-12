using System.ComponentModel.DataAnnotations;

namespace GIFU.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public int? GoodId { get; set; }
        [Required]
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string PlaceTime { get; set; }
        [Required]
        public int? Amount { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        [Required]
        public string Address { get; set; }
        public string TradeMethod { get; set; }
        public string UpdateDate { get; set; }
    }
}