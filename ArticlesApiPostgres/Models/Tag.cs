namespace ArticlesApiPostgres.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string Name { get; set; }

        public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    }
}