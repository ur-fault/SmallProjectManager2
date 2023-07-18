using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmallProjectManager2.Shared.Models;
using SmallProjectManager2.Shared.Models.Dto;

namespace SmallProjectManager2.Server.Data.Models;

public abstract class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string Firstname { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string Lastname { get; set; }

    [Required] public int AddressID { get; set; }

    public virtual Address Address { get; set; }

    public virtual ICollection<Project> Projects { get; set; }

    public override string ToString() =>
        $"{ID}: {Firstname} {Lastname}, {AddressID}: {Address}, {Projects?.Count.ToString() ?? "?"} projects";

    public PersonDtoGet ToDtoGet() => new() {
        ID = ID,
        Firstname = Firstname,
        Lastname = Lastname,
        Address = Address?.ToDto(),
        Projects = Projects?.Select(p => p.ToDtoGet(true)).ToList(),
    };
}