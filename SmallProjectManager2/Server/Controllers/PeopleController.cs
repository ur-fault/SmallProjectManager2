using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallProjectManager.Data;
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

    #region external workers

    [HttpGet("external")]
    public async Task<ActionResult<ICollection<ExternalDtoGet>>> GetAllExternalWorkers() {
        var workers = await _db.ExternalWorkers
            .Include(worker => worker.Address)
            .Include(worker => worker.Projects)
            .ToListAsync();
        var dtos = workers.Select(worker => new ExternalDtoGet {
            ID = worker.ID,
            Firstname = worker.Firstname,
            Lastname = worker.Lastname,
            Address = new AddressDto {
                ID = worker.Address.ID,
                Country = worker.Address.Country,
                City = worker.Address.City,
                Street = worker.Address.Street,
                HouseNumber = worker.Address.HouseNumber,
            },
            Projects = worker.Projects.Select(proj => new ProjectDtoGet {
                    ID = proj.ID,
                    Name = proj.Name,
                    Progress = proj.Progress,
                    PersonID = proj.PersonID,
                })
                .ToList(),
            Company = worker.Company,
        });
        return Ok(dtos);
    }

    [HttpGet("external/{id:int}")]
    public async Task<ActionResult<ExternalDtoGet>> GetExternalWorker(int id) {
        var worker = await _db.ExternalWorkers
            .Include(worker => worker.Address)
            .Include(worker => worker.Projects)
            .FirstOrDefaultAsync();

        if (worker is null) return NotFound();
        var dto = new ExternalDtoGet {
            ID = worker.ID,
            Firstname = worker.Firstname,
            Lastname = worker.Lastname,
            Address = new AddressDto {
                ID = worker.Address.ID,
                Country = worker.Address.Country,
                City = worker.Address.City,
                Street = worker.Address.Street,
                HouseNumber = worker.Address.HouseNumber,
            },
            Projects = worker.Projects.Select(proj => new ProjectDtoGet {
                    ID = proj.ID,
                    Name = proj.Name,
                    Progress = proj.Progress,
                    PersonID = proj.PersonID,
                })
                .ToList(),
            Company = worker.Company,
        };
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

        return Created($"api/people/external/{worker.ID}", new ExternalDtoGet {
            ID = worker.ID,
            Firstname = worker.Firstname,
            Lastname = worker.Lastname, Address = new AddressDto {
                ID = worker.Address.ID,
                Country = worker.Address.Country,
                City = worker.Address.City,
                Street = worker.Address.Street,
                HouseNumber = worker.Address.HouseNumber,
            },
            Projects = worker.Projects.Select(proj => new ProjectDtoGet {
                    ID = proj.ID,
                    Name = proj.Name,
                    Progress = proj.Progress,
                    PersonID = proj.PersonID,
                })
                .ToList(),
            Company = worker.Company,
        });
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
        var dtos = workers.Select(worker => new InternalDtoGet {
            ID = worker.ID,
            Firstname = worker.Firstname,
            Lastname = worker.Lastname,
            Address = new AddressDto {
                ID = worker.Address.ID,
                Country = worker.Address.Country,
                City = worker.Address.City,
                Street = worker.Address.Street,
                HouseNumber = worker.Address.HouseNumber,
            },
            Projects = worker.Projects.Select(proj => new ProjectDtoGet {
                    ID = proj.ID,
                    Name = proj.Name,
                    Progress = proj.Progress,
                    PersonID = proj.PersonID,
                })
                .ToList(),
            FirstWorkDay = worker.FirstWorkDay,
        });
        return Ok(dtos);
    }

    [HttpGet("internal/{id:int}")]
    public async Task<ActionResult<InternalDtoGet>> GetInternalWorker(int id) {
        var worker = await _db.InternalWorkers
            .Include(worker => worker.Address)
            .Include(worker => worker.Projects)
            .FirstOrDefaultAsync();

        if (worker is null) return NotFound();
        var dto = new InternalDtoGet {
            ID = worker.ID,
            Firstname = worker.Firstname,
            Lastname = worker.Lastname,
            Address = new AddressDto {
                ID = worker.Address.ID,
                Country = worker.Address.Country,
                City = worker.Address.City,
                Street = worker.Address.Street,
                HouseNumber = worker.Address.HouseNumber,
            },
            Projects = worker.Projects.Select(proj => new ProjectDtoGet {
                    ID = proj.ID,
                    Name = proj.Name,
                    Progress = proj.Progress,
                    PersonID = proj.PersonID,
                })
                .ToList(),
            FirstWorkDay = worker.FirstWorkDay,
        };
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

        return Created($"api/people/external/{worker.ID}", new InternalDtoGet {
            ID = worker.ID,
            Firstname = worker.Firstname,
            Lastname = worker.Lastname,
            Address = new AddressDto {
                ID = worker.Address.ID,
                Country = worker.Address.Country,
                City = worker.Address.City,
                Street = worker.Address.Street,
                HouseNumber = worker.Address.HouseNumber,
            },
            Projects = worker.Projects.Select(proj => new ProjectDtoGet {
                    ID = proj.ID,
                    Name = proj.Name,
                    Progress = proj.Progress,
                    PersonID = proj.PersonID,
                })
                .ToList(),
            FirstWorkDay = worker.FirstWorkDay,
        });
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