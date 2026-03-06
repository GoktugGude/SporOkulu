namespace SporOkulu.Domain;

public class StudentTestResult
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student Student {get;set;}
    public int TestTypeId { get; set; }
    public string Value { get; set; } 
    public DateTime Date { get; set; }
    
    public TestType TestType { get; set; }
}
