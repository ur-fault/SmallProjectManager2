using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SmallProjectManager2.Shared.Models;

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

    public override string ToString() => $"{Firstname} {Lastname}, {AddressID}: {Address}, {Projects.Count} projects";
}