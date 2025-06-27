using MovieManagement.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieManagement.Repositories
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<List<Comment>> GetCommentsForMovieAsync(int movieId);
    }
}