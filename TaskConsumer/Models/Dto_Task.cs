namespace TaskConsumer.Models;

public class Dto_Task
{
    public int  Id { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }
    public string Status { get; set; }
}