using System.ComponentModel.DataAnnotations;
using SmallProjectManager2.Shared.Models.Dto;

namespace SmallProjectManager2.Shared.Models;

public class ProjectDtoGet
{
    public int ID { get; set; }

    public string Name { get; set; }

    public float Progress { get; set; }

    public int PersonID { get; set; }

    public PersonDtoGet Person { get; set; }
}