using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManagement.Entities;

namespace MovieManagement.Models;

public class MovieDetails
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string DirectorName { get; set; }
    public int RunningTime { get; set; }
    public int ReleaseYear { get; set; }
    public string ImageUrl { get; set; }
    public string TrailerUrl { get; set; }
    public Decimal Rating { get; set; } = 0;
    public int? UserRating { get; set; }
    public List<Comment> Comments { get; set; }= new List<Comment>();
    public string Description { get; set; }
    public bool IsInWatchlist { get; set; }

}