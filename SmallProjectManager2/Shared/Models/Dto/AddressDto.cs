using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallProjectManager2.Shared.Models;

public class AddressDto
{
    public int ID { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public int HouseNumber { get; set; }

    public override string ToString() => $"{Country}, {City}, {Street} {HouseNumber}";
}