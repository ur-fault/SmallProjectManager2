using Microsoft.EntityFrameworkCore;
using SmallProjectManager2.Shared.Models;

namespace SmallProjectManager.Data;

public class ProjectsDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<ExternalWorker> ExternalWorkers { get; set; }
    public DbSet<InternalWorker> InernalWorkers { get; set; }
    public DbSet<Address> Addresses { get; set; }

    private string _dbPath;

    public ProjectsDbContext() {
        var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _dbPath = Path.Join(dir, "projects.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=Projects;Trusted_Connection=True;TrustServerCertificate=True;");
}