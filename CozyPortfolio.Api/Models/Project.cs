using System.ComponentModel.DataAnnotations;

namespace CozyPortfolio.Api.Models;

public class Project
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public IconName Icon { get; set; }

    public ICollection<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
}
