using System.Linq;
using Blog.Dtos;

namespace Blog;

public class BlogService
{
    private readonly MyDbContext _dbContext;

    public BlogService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<UserComments> GetUserComments()
    {
        var userComments = _dbContext.BlogComments
            .GroupBy(bc => bc.UserName)
            .Select(g => new UserComments
            {
                UserName = g.Key,
                Count = g.Count()
            });
        
        return userComments;
    }
    
    public IQueryable<LastBlogPostComments> GetLastBlogPostComments()
    {
        var lastBlogPostComments = (from bp in _dbContext.BlogPosts
            let comments = bp.Comments
                .OrderByDescending(x=>x.CreatedDate)
                .FirstOrDefault()
            select new LastBlogPostComments
            {
                PostTitle = bp.Title,
                LastDateComment = comments.CreatedDate.Date,
                CommentText = comments.Text
            }).OrderByDescending(x=>x.LastDateComment);
        
        return lastBlogPostComments;
    }
    
    public IQueryable<UserComments> GetLastCommentsByUser()
    {
        var lastCommentsByUser = _dbContext.BlogPosts
            .Select(post => post.Comments
                .OrderByDescending(comment => comment.CreatedDate)
                .FirstOrDefault()
            )
            .GroupBy(comment => comment.UserName)
            .Select(group => new UserComments {
                UserName = group.Key,
                Count = group.Count()
            });
        
        return lastCommentsByUser;
    }
}