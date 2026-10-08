namespace CozyPortfolio.Api.Models;

public record ReviewDraft(string? Author, int Rating, string Comment)
{
    public Review ToReview(string albumId) => new()
    {
        Id = Guid.NewGuid(),
        AlbumId = albumId,
        Author = string.IsNullOrWhiteSpace(Author) ? "Anonymous" : Author.Trim(),
        Rating = Math.Clamp(Rating, 1, 5),
        Comment = Comment.Trim(),
        PostedAt = DateTime.UtcNow
    };
}
