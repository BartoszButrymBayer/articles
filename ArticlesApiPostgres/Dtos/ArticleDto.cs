namespace ArticlesApiPostgres.Dtos
{
    public class ArticleDTO
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullContent { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}