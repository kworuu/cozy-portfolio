namespace CozyPortfolio.Api.Models;

public class ProjectTag
{
    public int ProjectId { get; set; }
    public Project Project { get; set; } = default!;

    public int TagId { get; set; }
    public Tag Tag { get; set; } = default!;
}
