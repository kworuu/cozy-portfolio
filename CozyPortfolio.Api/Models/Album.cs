using System.ComponentModel.DataAnnotations;

namespace CozyPortfolio.Api.Models;

public class Album
{
    [Key]
    public string Id { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(150)]
    public string Artist { get; set; } = string.Empty;

    public int Year { get; set; }

    [Required, MaxLength(100)]
    public string Genre { get; set; } = string.Empty;

    public string CoverImagePath { get; set; } = string.Empty;
    public string SnippetAudioPath { get; set; } = string.Empty;
    public string GlowColor { get; set; } = string.Empty;
    public string GlossTint { get; set; } = string.Empty;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
