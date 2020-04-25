namespace Ecommerce.ViewModels.Article
{
    public class IndexPublicViewModel
    {
        public string Description { get; set; }
        public int LotId { get; set; }
        public int TakenId { get; set; }
        public Lot.LotState State { get; set; }
    }
}
