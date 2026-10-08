using System.ComponentModel.DataAnnotations;

namespace CozyPortfolio.Api.Models;

public class Skill
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public IconName Icon { get; set; }
}
