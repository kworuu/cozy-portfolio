using CozyPortfolio.Models;
using static Supabase.Postgrest.Constants;
namespace CozyPortfolio.Services;

public class ProjectService
{
    private readonly Supabase.Client _db;
    public ProjectService(Supabase.Client db) => _db = db;

    // READ: filter in the database, one request, first 50 rows
    public async Task<IReadOnlyList<Project>> GetAllAsync()
    {
        var res = await _db.From<ProjectCardRow>()
            .Where(p => p.DeletedAt == null)
            .Order(p => p.Id, Ordering.Ascending)
            .Range(0, 49)
            .Get();

        return res.Models
            .Select(r => new Project(r.Title, r.Description, r.Tags.ToArray(), ParseIcon(r.Icon), r.Id))
            .ToList();
    }

    // CREATE (POST)
    public async Task AddAsync(string title, string description, IconName icon)
    {
        await _db.From<ProjectRow>().Insert(new ProjectRow
        {
            Title = title.Trim(), Description = description.Trim(), Icon = icon.ToString()
        });
    }

    // UPDATE (PATCH) - always with Where
    public async Task UpdateAsync(long id, string title, string description, IconName icon)
    {
        await _db.From<ProjectRow>()
            .Where(x => x.Id == id)
            .Set(x => x.Title, title.Trim())
            .Set(x => x.Description, description.Trim())
            .Set(x => x.Icon, icon.ToString())
            .Update();
    }

    // DELETE = soft delete: just fills deleted_at
    public async Task SoftDeleteAsync(long id)
    {
        await _db.From<ProjectRow>()
            .Where(x => x.Id == id)
            .Set(x => x.DeletedAt, DateTime.UtcNow)
            .Update();
    }

    private static IconName ParseIcon(string value) =>
        Enum.TryParse<IconName>(value, out var icon) ? icon : IconName.Sparkle;
}