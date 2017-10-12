namespace GIFU.Models
{
    public class OrderShow
    {
        public int OrderId { get; set; }
        public int GoodId { get; set; }
        public string GoodsTitle { get; set; }
        public string GiverName { get; set; }
        public string PlaceTime { get; set; }
        public int? Amount { get; set; }
        public string Comment { get; set; }
        public string StatusName { get; set; }
        public string Address { get; set; }
        public string UpdateDate { get; set; }
        public int NewDegree { get; set; }
        public string Tag1Name { get; set; }
        public string Tag2Name { get; set; }
        public string PicPath { get; set; }
    }
}