using Microsoft.AspNetCore.Mvc;
using SmallProjectManager.Data;
using SmallProjectManager2.Server.Data.Models;
using SmallProjectManager2.Shared.Models;

namespace SmallProjectManager2.Server.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private ProjectsDbContext _db;

    public AddressController(ProjectsDbContext db) {
        _db = db;
    }

    [HttpGet]
    public ActionResult<ICollection<Address>> GetAll() {
        return Ok(_db.Addresses);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Address>> Get(int id) {
        var addr = await _db.Addresses.FindAsync(id);
        if (addr is not null)
            return Ok(addr);
        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Add(Address address) {
        if (TryValidateModel(address)) {
            _db.Addresses.Add(address);
            await _db.SaveChangesAsync();
            return Ok();
        }

        return ValidationProblem();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id) {
        var addr = await _db.Addresses.FindAsync(id);
        if (addr is null) return NotFound();

        _db.Addresses.Remove(addr);
        await _db.SaveChangesAsync();
        return Ok();
    }
}