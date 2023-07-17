using System.ComponentModel.DataAnnotations;

namespace SmallProjectManager2.Shared.Models;

public class Project
{
    public int ID { get; set; }

    [Required]
    [StringLength(2, MinimumLength = 50)]
    public string Name { get; set; } = default!;

    [Range(0, 1)] public float Progress { get; set; }

    [Required] public int PersonID { get; set; }

    [Required] public virtual Person Person { get; set; }
}