using Microsoft.EntityFrameworkCore;
using CozyPortfolio.Api.Models;

namespace CozyPortfolio.Api.Data;

public class CozyPortfolioDbContext : DbContext
{
    public CozyPortfolioDbContext(DbContextOptions<CozyPortfolioDbContext> options)
        : base(options) { }

    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<ProjectTag> ProjectTags => Set<ProjectTag>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<MoodState> MoodStates => Set<MoodState>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1:N Album -> Review with Cascade Delete
        modelBuilder.Entity<Album>()
            .HasMany(a => a.Reviews)
            .WithOne(r => r.Album)
            .HasForeignKey(r => r.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);

        // M:N Project <-> Tag composite key & navigation
        modelBuilder.Entity<ProjectTag>()
            .HasKey(pt => new { pt.ProjectId, pt.TagId });

        modelBuilder.Entity<ProjectTag>()
            .HasOne(pt => pt.Project)
            .WithMany(p => p.ProjectTags)
            .HasForeignKey(pt => pt.ProjectId);

        modelBuilder.Entity<ProjectTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.ProjectTags)
            .HasForeignKey(pt => pt.TagId);

        // Unique index on Tag Name to prevent duplicate tags
        modelBuilder.Entity<Tag>()
            .HasIndex(t => t.Name)
            .IsUnique();
    }
}
