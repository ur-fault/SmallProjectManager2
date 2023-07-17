using Microsoft.AspNetCore.Mvc;
using SmallProjectManager.Data;
using SmallProjectManager2.Shared.Models;

namespace SmallProjectManager2.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private ProjectsDbContext _db;

    public ProjectsController(ProjectsDbContext db) {
        _db = db;
    }

    [HttpGet("projects")]
    public ActionResult<ICollection<Project>> GetProjects() {
        return Ok(new List<Project> {
            new Project {
                Name = "Test Project1",
                Progress = 0.5f,
            },
        });
    }

    [HttpGet("external")]
    public ActionResult<ICollection<ExternalWorker>> GetExternalPeople() {
        return Ok(new List<ExternalWorker> {
            new ExternalWorker {
                Firstname = "John",
                Lastname = "Doe",
                Company = "Example Inc.",
            },
        });
    }

    [HttpGet("internal")]
    public ActionResult<ICollection<InternalWorker>> GetInternalPeople() {
        return Ok(new List<InternalWorker> {
            new InternalWorker {
                Firstname = "John",
                Lastname = "Doe",
                FirstWorkDay = DateTime.Today.AddDays(-7),
            },
        });
    }
}