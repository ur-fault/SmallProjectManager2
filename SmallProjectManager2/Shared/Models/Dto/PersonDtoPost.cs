using System.ComponentModel.DataAnnotations;

namespace SmallProjectManager2.Shared.Models.Dto;

public class PersonDtoPost
{
    [Required] public int ID { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string Firstname { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string Lastname { get; set; }

    [Required] public int AddressID { get; set; }

    public virtual ICollection<int>? Projects { get; set; }
}