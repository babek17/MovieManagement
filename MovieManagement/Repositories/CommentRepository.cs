using MongoDB.Driver;
using MovieManagement.Entities;
using MovieManagement.MongoEntities;

namespace MovieManagement.Repositories
{
    public class CommentRepository: ICommentRepository
    {
        private readonly IMongoCollection<Comment> _comments;

        public CommentRepository(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDBSettings:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDBSettings:DatabaseName"]);
            _comments = database.GetCollection<Comment>(config["MongoDBSettings:CommentsCollectionName"]);
        }

        public async Task<Comment> FindCommentByUserAndMovieIdAsync(string userId, int movieId)
        {
            var comment =  await _comments.Find(comment => comment.UserId == userId && comment.MovieId == movieId).FirstOrDefaultAsync();
            return comment;
        }
        public async Task AddAsync(Comment comment)
        {
            await _comments.InsertOneAsync(comment);
        }

        public async Task DeleteAsync(Comment comment)
        {
            await _comments.DeleteOneAsync(c => c.Id == comment.Id);
        }

        public async Task<List<Comment>> GetCommentsForMovieAsync(int movieId)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.MovieId, movieId);
            return await _comments.Find(filter).ToListAsync();
        }
        
        public async Task DeleteCommentAndRepliesRecursivelyAsync(Comment comment)
        {
            await _comments.DeleteOneAsync(c => c.Id == comment.Id);
            var replies = await _comments.Find(c => c.ParentCommentId == comment.Id).ToListAsync();

            foreach (var reply in replies)
            {
                await DeleteCommentAndRepliesRecursivelyAsync(reply);
            }
        }
    }
}