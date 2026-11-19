using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
namespace CozyPortfolio.Models;

[Table("projects")]
public class ProjectRow : BaseModel
{
    [PrimaryKey("id", false)] public long Id { get; set; }
    [Column("title")]       public string Title { get; set; } = "";
    [Column("description")] public string Description { get; set; } = "";
    [Column("icon")]        public string Icon { get; set; } = nameof(IconName.Sparkle);
    [Column("deleted_at")]  public DateTime? DeletedAt { get; set; }
}

[Table("project_cards")]
public class ProjectCardRow : BaseModel
{
    [PrimaryKey("id", false)] public long Id { get; set; }
    [Column("title")]       public string Title { get; set; } = "";
    [Column("description")] public string Description { get; set; } = "";
    [Column("icon")]        public string Icon { get; set; } = nameof(IconName.Sparkle);
    [Column("tags")]        public List<string> Tags { get; set; } = new();
    [Column("deleted_at")]  public DateTime? DeletedAt { get; set; }
}