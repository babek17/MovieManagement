using MovieManagement.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieManagement.MongoEntities;

namespace MovieManagement.Repositories
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        Task SoftDeleteAsync(Comment comment);
        Task<Comment> FindByIdAsync(string commentId);
        Task<Comment> FindCommentByUserAndMovieIdAsync(string userId, int movieId);
        Task<List<Comment>> GetCommentsForMovieAsync(int movieId);
    }
}