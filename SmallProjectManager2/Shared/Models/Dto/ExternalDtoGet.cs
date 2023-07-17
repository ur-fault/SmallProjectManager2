using System.ComponentModel.DataAnnotations;

namespace SmallProjectManager2.Shared.Models.Dto;

public class ExternalDtoGet : PersonDtoGet
{
    public string? Company { get; set; }

    public override string ToString() => base.ToString() + $", {Company}";
}