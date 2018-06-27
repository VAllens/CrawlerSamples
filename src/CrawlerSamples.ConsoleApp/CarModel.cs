namespace CrawlerSamples
{
    public class CarModel
    {
        public virtual string Title { get; set; }

        public virtual string ImageUrl { get; set; }

        public virtual string ProductUrl { get; set; }

        public virtual string OriginalPrice { get; set; }

        public virtual string PresentPrice { get; set; }

        public virtual string OrdersNumber { get; set; }

        public virtual string Tip { get; set; }
    }
}