using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyPortfolio.Api.Models;

public class Review
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string AlbumId { get; set; } = string.Empty;

    [ForeignKey(nameof(AlbumId))]
    public Album? Album { get; set; }

    [Required, MaxLength(100)]
    public string Author { get; set; } = "Anonymous";

    [Range(1, 5)]
    public int Rating { get; set; }

    [Required, MaxLength(2000)]
    public string Comment { get; set; } = string.Empty;

    public DateTime PostedAt { get; set; } = DateTime.UtcNow;
}
