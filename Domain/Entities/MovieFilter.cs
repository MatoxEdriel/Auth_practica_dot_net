namespace Domain.Entities;
//Se tiene pensado usar para filtros 
public class MovieFilter
{
    public string? Title { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsActive { get; set; }
    public int? RoomId { get; set; }
}