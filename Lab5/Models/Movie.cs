using System.ComponentModel.DataAnnotations;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    [UIHint("LongText")]
    public string Description { get; set; } = string.Empty;

    [UIHint("Stars")]
    [Range(0, 5)]
    public int Rating { get; set; }
    public string TrailerLink { get; set; } = string.Empty;
}