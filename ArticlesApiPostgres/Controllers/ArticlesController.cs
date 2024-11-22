using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArticlesApiPostgres.Data;
using ArticlesApiPostgres.Models;
using ArticlesApiPostgres.Dtos;

[Route("api/articles")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ArticlesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDTO>>> GetArticles()
    {
        var articles = await _context.Articles
        .Include(a => a.ArticleTags)
        .ThenInclude(at => at.Tag)
        .ToListAsync();

        var articleDTOs = articles.Select(article => new ArticleDTO
        {
            ArticleID = article.ArticleID,
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            FullContent = article.FullContent,
            Author = article.Author,
            PublishedDate = article.PublishedDate,
            Tags = article.ArticleTags.Select(at => at.Tag.Name).ToList()
        }).ToList();

        return Ok(articleDTOs);
    }

    [HttpGet("{id}")]
    [Produces(typeof(ArticleDTO))]
    public async Task<ActionResult<ArticleDTO>> GetArticle(int id)
    {
        var article = await _context.Articles
        .Include(a => a.ArticleTags)
        .ThenInclude(at => at.Tag)
        .FirstOrDefaultAsync(a => a.ArticleID == id);

        if (article == null)
        {
            return NotFound();
        }

        var articleDTO = new ArticleDTO
        {
            ArticleID = article.ArticleID,
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            FullContent = article.FullContent,
            Author = article.Author,
            PublishedDate = article.PublishedDate,
            Tags = article.ArticleTags.Select(at => at.Tag.Name).ToList()
        };

        return Ok(articleDTO);
    }

    [HttpPost]
    public async Task<ActionResult<ArticleDTO>> PostArticle(ArticleDTO articleDto)
    {
        var tagsInDb = await _context.Tags
        .Where(t => articleDto.Tags.Contains(t.Name))
        .ToListAsync();

        var newTags = articleDto.Tags
        .Except(tagsInDb.Select(t => t.Name))
        .Select(tagName => new Tag { Name = tagName })
        .ToList();

        var utcPublishedDate = articleDto.PublishedDate.ToUniversalTime();

        var article = new Article
        {
            Title = articleDto.Title,
            ShortDescription = articleDto.ShortDescription,
            FullContent = articleDto.FullContent,
            Author = articleDto.Author,
            PublishedDate = utcPublishedDate,
            ArticleTags = tagsInDb
            .Select(tag => new ArticleTag { Tag = tag })
            .Concat(newTags.Select(tag => new ArticleTag { Tag = tag }))
            .ToList()
        };

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        var resultDTO = new ArticleDTO
        {
            ArticleID = article.ArticleID,
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            FullContent = article.FullContent,
            Author = article.Author,
            PublishedDate = article.PublishedDate,
            Tags = article.ArticleTags.Select(at => at.Tag.Name).ToList()
        };

        return CreatedAtAction(nameof(GetArticle), new { id = article.ArticleID }, resultDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutArticle(int id, ArticleDTO articleDto)
    {
        if (id != articleDto.ArticleID)
        {
            return BadRequest("ArticleID in URL and body must match.");
        }

        var article = await _context.Articles
        .Include(a => a.ArticleTags)
        .ThenInclude(at => at.Tag)
        .FirstOrDefaultAsync(a => a.ArticleID == id);

        if (article == null)
        {
            return NotFound($"Article with ID {id} not found.");
        }

        article.Title = articleDto.Title;
        article.ShortDescription = articleDto.ShortDescription;
        article.FullContent = articleDto.FullContent;
        article.Author = articleDto.Author;
        article.PublishedDate = articleDto.PublishedDate.ToUniversalTime();

        _context.Entry(article).State = EntityState.Modified;

        var existingTags = await _context.Tags
            .Where(t => articleDto.Tags.Contains(t.Name))
            .ToListAsync();

        var newTags = articleDto.Tags
            .Except(existingTags.Select(t => t.Name))
            .Select(tagName => new Tag { Name = tagName })
            .ToList();

        article.ArticleTags = existingTags
            .Select(tag => new ArticleTag { ArticleID = article.ArticleID, TagID = tag.TagID })
            .Concat(newTags.Select(tag => new ArticleTag { Tag = tag }))
            .ToList();

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await _context.Articles
        .Include(a => a.ArticleTags)
        .FirstOrDefaultAsync(a => a.ArticleID == id);

        if (article == null)
        {
            return NotFound($"Article with ID {id} not found.");
        }

        // Usuń powiązania z tagami
        _context.ArticleTags.RemoveRange(article.ArticleTags);

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}