namespace MovieManagement.Models;

public class MovieQuery
{
    public string SortBy { get; set; } = "Title";
    public int Page { get; set; } = 1;
    public string Genre { get; set; } = "All Genres";
    public string Query { get; set; } = "";
    public int PageSize { get; set; } = 12;
}