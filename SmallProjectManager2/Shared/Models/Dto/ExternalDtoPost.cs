using System.ComponentModel.DataAnnotations;

namespace SmallProjectManager2.Shared.Models.Dto;

public class ExternalDtoPost : PersonDtoPost
{
    [StringLength(50, MinimumLength = 2)] public string? Company { get; set; }

    public override string ToString() => base.ToString() + $", {Company}";
}