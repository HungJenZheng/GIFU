namespace GIFU.Models
{
    public class GoodsSearchArg
    {
        public int? GoodId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public int? Offset { get; set; }
        public int? NextNo { get; set; }
    }
}