using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticlesApiPostgres.Models
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullContent { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();
    }
}