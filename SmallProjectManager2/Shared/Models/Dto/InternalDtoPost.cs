using System.ComponentModel.DataAnnotations;
using SmallProjectManager2.Shared.Validation;

namespace SmallProjectManager2.Shared.Models.Dto;

public class InternalDtoPost : PersonDtoPost
{
    [Required] [ToPast(100)] public DateTime FirstWorkDay { get; set; }

    public override string ToString() => base.ToString() + $", {FirstWorkDay}";
}