using System.ComponentModel.DataAnnotations;
using SmallProjectManager2.Shared.Models.Dto;

namespace SmallProjectManager2.Server.Data.Models;

public class ExternalWorker : Person
{
    [StringLength(50, MinimumLength = 2)] public string? Company { get; set; }

    public override string ToString() => base.ToString() + $", {Company}";

    public ExternalDtoGet ToDtoGet() => new() {
        ID = ID,
        Firstname = Firstname,
        Lastname = Lastname,
        Address = Address.ToDto(),
        Projects = Projects.Select(p => p.ToDtoGet(true)).ToList(),
        Company = Company,
        Kind = "external",
    };
}