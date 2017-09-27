namespace GIFU.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int? GoodId { get; set; }
        public int? UserId { get; set; }
        public string PlaceTime { get; set; }
        public int? Amount { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string TradeMethod { get; set; }
        public string UpdateDate { get; set; }
    }
}