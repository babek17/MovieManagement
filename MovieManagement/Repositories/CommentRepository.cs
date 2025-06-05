using Microsoft.EntityFrameworkCore;
using MovieManagement.Data;
using MovieManagement.Entities;
namespace MovieManagement.Repositories;

public class CommentRepository: ICommentRepository
{
    private readonly MovieManagementDbContext _context;

    public CommentRepository(MovieManagementDbContext context)
    {
        _context = context;
    }
    
    public void Save()
    {
        _context.SaveChanges();
    }
    
    public void Add(Comment comment)
    {
        _context.Comments.Add(comment);
    }

    public IQueryable<Comment> GetCommentsForMovie(int movieId)
    {
        var comments = _context.Comments
            .Include(c => c.User)
            .Where(c => c.MovieId == movieId);
        return comments;
    }
    
    
}