using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallProjectManager.Data;
using SmallProjectManager2.Shared.Models;
using SmallProjectManager2.Shared.Models.Dto;

namespace SmallProjectManager2.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private ProjectsDbContext _db;

    public ProjectsController(ProjectsDbContext db) {
        _db = db;
    }

    [HttpGet]
    public ActionResult<ICollection<ProjectDtoGet>> GetAll() {
        return Ok(_db.Projects);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDtoGet>> Get(int id) {
        var project = await _db.Projects.FindAsync(id);
        if (project is not null)
            return Ok(project);
        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDtoGet>> Add(ProjectDtoPost dto) {
        if (!TryValidateModel(dto)) return ValidationProblem();

        var project = new Project {
            ID = dto.ID,
            Name = dto.Name,
            Progress = dto.Progress,
            PersonID = dto.PersonID,
        };

        _db.Projects.Add(project);
        try {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException e) {
            return BadRequest();
        }

        return Created($"api/projects/{project.ID}", new ProjectDtoGet {
            ID = project.ID,
            Name = project.Name,
            Progress = project.Progress,
            PersonID = project.PersonID,
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id) {
        var project = await _db.Projects.FindAsync(id);
        if (project is null) return NotFound();

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();
        return Ok();
    }
}