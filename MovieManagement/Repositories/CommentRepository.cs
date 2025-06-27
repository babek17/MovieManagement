using MongoDB.Driver;
using MovieManagement.Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task AddAsync(Comment comment)
        {
            await _comments.InsertOneAsync(comment);
        }

        public async Task<List<Comment>> GetCommentsForMovieAsync(int movieId)
        {
            var filter = Builders<Comment>.Filter.Eq(c => c.MovieId, movieId);
            return await _comments.Find(filter).ToListAsync();
        }
    }
}