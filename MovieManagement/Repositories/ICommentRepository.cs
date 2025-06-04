using MovieManagement.Entities;

namespace MovieManagement.Repositories;

public interface ICommentRepository
{
    void Save();
    void Add(Comment comment);
    IQueryable<Comment> GetCommentsForMovie(int movieId);
}