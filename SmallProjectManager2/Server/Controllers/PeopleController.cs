using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallProjectManager.Data;
using SmallProjectManager2.Server.Data.Models;
using SmallProjectManager2.Shared.Models;
using SmallProjectManager2.Shared.Models.Dto;

namespace SmallProjectManager2.Server.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly ProjectsDbContext _db;

    public PeopleController(ProjectsDbContext db) {
        _db = db;
    }

    #region all workers

    [HttpGet("all")]
    public async Task<ActionResult<ICollection<PersonDtoGet>>> GetAllWorkers() {
        var workers = (await _db.ExternalWorkers
                .Include(worker => worker.Address)
                .Include(worker => worker.Projects)
                .ToListAsync())
            .Select(worker => (PersonDtoGet)worker.ToDtoGet())
            .ToList();
        workers.AddRange((await _db.InternalWorkers
                .Include(worker => worker.Address)
                .Include(worker => worker.Projects)
                .ToListAsync())
            .Select(worker => (PersonDtoGet)worker.ToDtoGet()));

        return Ok(workers);
    }

    [HttpGet("all/{id:int}")]
    public async Task<ActionResult<ICollection<PersonDtoGet>>> GetWorker(int id) {
        var worker = await _db.ExternalWorkers
                         .Include(worker => worker.Address)
                         .Include(worker => worker.Projects)
                         .FirstOrDefaultAsync() as Person
                     ?? await _db.InternalWorkers.Include(worker => worker.Address)
                         .Include(worker => worker.Projects)
                         .FirstOrDefaultAsync();

        if (worker is null) return NotFound();
        return Ok(worker.ToDtoGet());
    }

    #endregion

    #region external workers

    [HttpGet("external")]
    public async Task<ActionResult<ICollection<ExternalDtoGet>>> GetAllExternalWorkers() {
        var workers = await _db.ExternalWorkers
            .Include(worker => worker.Address)
            .Include(worker => worker.Projects)
            .ToListAsync();
        var dtos = workers.Select(worker => worker.ToDtoGet());
        return Ok(dtos);
    }

    [HttpGet("external/{id:int}")]
    public async Task<ActionResult<ExternalDtoGet>> GetExternalWorker(int id) {
        var worker = await _db.ExternalWorkers
            .Include(worker => worker.Address)
            .Include(worker => worker.Projects)
            .FirstOrDefaultAsync();

        if (worker is null) return NotFound();
        var dto = worker.ToDtoGet();
        return Ok(dto);
    }

    [HttpPost("external")]
    public async Task<ActionResult<ExternalDtoGet>> AddExternalWorker(ExternalDtoPost dto) {
        if (!TryValidateModel(dto)) return ValidationProblem();

        var worker = new ExternalWorker {
            ID = dto.ID,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            AddressID = dto.AddressID,
            Projects = _db.Projects.Where(proj => dto.Projects != null && dto.Projects.Contains(proj.ID)).ToList(),
            Company = dto.Company,
        };

        _db.ExternalWorkers.Add(worker);
        try {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException e) {
            return BadRequest();
        }

        return Created($"api/people/external/{worker.ID}", worker.ToDtoGet());
    }

    [HttpDelete("external/{id:int}")]
    public async Task<ActionResult> DeleteExternalWorker(int id) {
        var worker = await _db.ExternalWorkers.FindAsync(id);
        if (worker is null) return NotFound();

        _db.ExternalWorkers.Remove(worker);
        await _db.SaveChangesAsync();
        return Ok();
    }

    #endregion


    #region internal workers

    [HttpGet("internal")]
    public async Task<ActionResult<ICollection<InternalDtoGet>>> GetAllInternalWorkers() {
        var workers = await _db.InternalWorkers
            .Include(worker => worker.Address)
            .Include(worker => worker.Projects)
            .ToListAsync();
        var dtos = workers.Select(worker => worker.ToDtoGet());
        return Ok(dtos);
    }

    [HttpGet("internal/{id:int}")]
    public async Task<ActionResult<InternalDtoGet>> GetInternalWorker(int id) {
        var worker = await _db.InternalWorkers
            .Include(worker => worker.Address)
            .Include(worker => worker.Projects)
            .FirstOrDefaultAsync();

        if (worker is null) return NotFound();
        var dto = worker.ToDtoGet();
        return Ok(dto);
    }

    [HttpPost("internal")]
    public async Task<ActionResult<InternalDtoGet>> AddInternalWorker(InternalDtoPost dto) {
        if (!TryValidateModel(dto)) return ValidationProblem();

        var worker = new InternalWorker {
            ID = dto.ID,
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            AddressID = dto.AddressID,
            Projects = _db.Projects.Where(proj => dto.Projects != null && dto.Projects.Contains(proj.ID)).ToList(),
            FirstWorkDay = dto.FirstWorkDay,
        };

        _db.InternalWorkers.Add(worker);
        try {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateException e) {
            return BadRequest();
        }

        // shouldn't be null, since there was no DbUpdateException
        worker.Address = (await _db.Addresses.FindAsync(worker.AddressID))!;

        return Created($"api/people/external/{worker.ID}", worker.ToDtoGet());
    }

    [HttpDelete("internal/{id:int}")]
    public async Task<ActionResult> DeleteInternalWorker(int id) {
        var worker = await _db.InternalWorkers.FindAsync(id);
        if (worker is null) return NotFound();

        _db.InternalWorkers.Remove(worker);
        await _db.SaveChangesAsync();
        return Ok();
    }

    #endregion
}