using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmallProjectManager2.Shared.Models;

namespace SmallProjectManager2.Server.Data.Models;

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

    public override string ToString() => $"{ID}: {Country}, {City}, {Street} {HouseNumber}";

    public AddressDto ToDto() => new() {
        ID = ID,
        Country = Country,
        City = City,
        Street = Street,
        HouseNumber = HouseNumber,
    };

    public static Address ToModel(AddressDto dto) => new() {
        ID = dto.ID,
        Country = dto.Country,
        City = dto.City,
        Street = dto.Street,
        HouseNumber = dto.HouseNumber,
    };
}