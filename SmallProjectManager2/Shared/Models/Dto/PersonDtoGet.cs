using System.ComponentModel.DataAnnotations;

namespace SmallProjectManager2.Shared.Models.Dto;

public class PersonDtoGet
{
    public int ID { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public AddressDto Address { get; set; }

    public ICollection<ProjectDtoGet> Projects { get; set; }

    public string Kind { get; set; }

    public override string ToString() =>
        $"{Firstname} {Lastname}, {Address?.ToString() ?? "? addr"}, {Projects?.Count.ToString() ?? "?"} projects";
}