using System.ComponentModel.DataAnnotations;
using SmallProjectManager2.Shared.Models.Dto;
using SmallProjectManager2.Shared.Validation;

namespace SmallProjectManager2.Server.Data.Models;

public class InternalWorker : Person
{
    [Required] [ToPast(100)] public DateTime FirstWorkDay { get; set; }

    public override string ToString() => base.ToString() + $", {FirstWorkDay}";

    public InternalDtoGet ToDtoGet() => new() {
        ID = ID,
        Firstname = Firstname,
        Lastname = Lastname,
        Address = Address.ToDto(),
        Projects = Projects.Select(p => p.ToDtoGet(true)).ToList(),
        FirstWorkDay = FirstWorkDay,
    };
}