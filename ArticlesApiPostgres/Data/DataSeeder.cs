using System.Text.Json;
using ArticlesApiPostgres.Models;
using ArticlesApiPostgres.Data;

public static class DataSeeder
{
    public static async Task SeedDatabase(IServiceProvider serviceProvider, string jsonFilePath)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!context.Articles.Any())
        {
            // Wczytanie danych z pliku JSON
            var jsonData = await File.ReadAllTextAsync(jsonFilePath);
            var articlesFromFile = JsonSerializer.Deserialize<List<ArticleFromFile>>(jsonData);

            if (articlesFromFile == null || !articlesFromFile.Any())
            {
                Console.WriteLine("No data to seed.");
                return;
            }

            // Tworzenie tagów i artykułów na podstawie JSON
            var tagsDict = new Dictionary<string, Tag>();
            foreach (var article in articlesFromFile)
            {
                foreach (var tagName in article.Tags)
                {
                    if (!tagsDict.ContainsKey(tagName))
                    {
                        tagsDict[tagName] = new Tag { Name = tagName };
                    }
                }
            }

            var tags = tagsDict.Values.ToList();
            context.Tags.AddRange(tags);

            var articles = articlesFromFile.Select(a => new Article
            {
                Title = a.Title,
                ShortDescription = a.ShortDescription,
                FullContent = a.FullContent,
                Author = a.Author,
                PublishedDate = DateTime.SpecifyKind(a.PublishedDate, DateTimeKind.Utc),
                ArticleTags = a.Tags.Select(tagName => new ArticleTag
                {
                    Tag = tagsDict[tagName]
                }).ToList()
            }).ToList();

            context.Articles.AddRange(articles);
            await context.SaveChangesAsync();
        }
    }

    private class ArticleFromFile
    {
        public int ArticleID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullContent { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<string> Tags { get; set; }
    }
}
