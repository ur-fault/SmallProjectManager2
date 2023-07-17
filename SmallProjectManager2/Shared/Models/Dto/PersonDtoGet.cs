using System.ComponentModel.DataAnnotations;

namespace SmallProjectManager2.Shared.Models.Dto;

public class PersonDtoGet
{
    public int ID { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public virtual Address Address { get; set; }

    public virtual ICollection<Project> Projects { get; set; }

    public override string ToString() => $"{Firstname} {Lastname}, {Address}, {Projects.Count} projects";
}