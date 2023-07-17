using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallProjectManager2.Shared.Models;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string Country { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string City { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string Street { get; set; }

    [Required] [Range(1, int.MaxValue)] public int HouseNumber { get; set; }

    public override string ToString() => $"{Country}, {City}, {Street} {HouseNumber}";
}