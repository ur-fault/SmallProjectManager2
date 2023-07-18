using System.ComponentModel.DataAnnotations;
using SmallProjectManager2.Shared.Models;

namespace SmallProjectManager2.Server.Data.Models;

public class Project
{
    public int ID { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = default!;

    [Range(0, 1)] public float Progress { get; set; }

    [Required] public int PersonID { get; set; }

    [Required] public virtual Person Person { get; set; }

    public override string ToString() => $"{ID}: {Name}, {Progress:P0}, {PersonID}: {Person}";

    public ProjectDtoGet ToDtoGet(bool ignorePerson = false) => new() {
        ID = ID,
        Name = Name,
        Progress = Progress,
        PersonID = PersonID,
        Person = ignorePerson ? null : Person?.ToDtoGet(),
    };
}