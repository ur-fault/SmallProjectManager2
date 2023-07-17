namespace SmallProjectManager2.Shared.Models.Dto;

public class InternalDtoGet : PersonDtoGet
{
    public DateTime FirstWorkDay { get; set; }

    public override string ToString() => base.ToString() + $", {FirstWorkDay}";
}