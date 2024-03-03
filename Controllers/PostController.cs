using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBlog.ViewModels;
using NewBlog.ViewModels.Posts;

namespace NewBlog.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    [HttpGet("v1/posts")]
    public async Task<IActionResult> GetPostsAsync([FromServices] BlogDataContext context, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
    {
        try
        {
            var count = await context.Posts.AsNoTracking().CountAsync();
            var posts = await context
            .Posts
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Select(x => new ListPostsViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                LastUpdateDate = x.LastUpdateDate,
                Category = x.Category.Name,
                Author = $"{x.Author.Name} ({x.Author.Email})"
            })
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderByDescending(x => x.LastUpdateDate)
            .ToListAsync();
            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page,
                pageSize,
                posts
            }));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Post>("Falha interna"));
        }
    }

    [HttpGet("v1/posts/{id:int}")]
    public async Task<IActionResult> GetPostsByIdAsync([FromServices] BlogDataContext context, [FromRoute] int id)
    {
        try
        {
            var post = await context
            .Posts
            .AsNoTracking()
            .Include(x => x.Author)
                .ThenInclude(x => x.Roles)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (post == null)
                return NotFound(new ResultViewModel<Post>("Post nao encontrado"));

            return Ok(new ResultViewModel<Post>(post));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Post> ("Falha interna"));
        }
    }

    [HttpGet("v1/posts/category/{category}")]
    public async Task<IActionResult> GetPostsByCategorySlugAsync([FromRoute] string category, [FromServices] BlogDataContext context, [FromQuery] int page = 0, [FromQuery] int pageSize = 10)
    {        
        try
        {
            var count = await context.Posts.AsNoTracking().CountAsync();
            var posts = await context
            .Posts
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Author)
            .Where(x => x.Category.Slug == category)
            .Select(x => new ListPostsViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                LastUpdateDate = x.LastUpdateDate,
                Category = x.Category.Name,
                Author = $"{x.Author.Name} ({x.Author.Email})"
            })
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderByDescending(x => x.LastUpdateDate)
            .ToListAsync();
            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page,
                pageSize,
                posts
            }));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Post>("Falha interna"));
        }
    }
}
