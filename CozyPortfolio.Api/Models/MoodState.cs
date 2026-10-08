using System.ComponentModel.DataAnnotations;

namespace CozyPortfolio.Api.Models;

public class MoodState
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string MoodName { get; set; } = string.Empty;

    [Required, MaxLength(300)]
    public string Message { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;
    public string ThemeClass { get; set; } = string.Empty;
    public IconName Icon { get; set; }
    public string AudioPath { get; set; } = string.Empty;
}
