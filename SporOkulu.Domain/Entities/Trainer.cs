namespace SporOkulu.Domain;

public class Trainer
{
    public int Id { get; set; }
    public string AppUserId { get; set; }
    public int BranchId { get; set; }
    public Branch Branch {get;set;}
    public string Bio{get;set;}
}
