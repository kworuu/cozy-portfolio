using System.ComponentModel.DataAnnotations;

namespace CozyPortfolio.Api.Models;

public class Tag
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public ICollection<ProjectTag> ProjectTags { get; set; } = new List<ProjectTag>();
}
