using ArticlesApiPostgres.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesApiPostgres.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracja relacji wiele-do-wielu
            modelBuilder.Entity<ArticleTag>()
                .HasKey(at => new { at.ArticleID, at.TagID });

            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(at => at.ArticleID);

            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(at => at.TagID);
        }
    }
}