using System;

namespace Blog.Dtos;

public class LastBlogPostComments
{
    public string PostTitle { get; init; }
    
    public DateTime LastDateComment { get; init; }
    
    public string CommentText { get; init; }
    
}