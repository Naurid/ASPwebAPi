namespace Repository.Entities;

public class TaskEntity
{
    
    public int  Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }
}