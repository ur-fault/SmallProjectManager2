using System.ComponentModel.DataAnnotations;
using SmallProjectManager2.Shared.Models.Dto;

namespace SmallProjectManager2.Shared.Models;

public class ProjectDtoPost
{
    [Required]
    public int ID { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = default!;

    [Range(0, 1)] public float Progress { get; set; }

    [Required] public int PersonID { get; set; }
}