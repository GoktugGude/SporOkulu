using System.Dynamic;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Domain;

public class Student
{
    public int Id { get; set; }
    
    public DateTime BirthDate {get;set;}

    public int AppUserId {get;set;}
    public AppUser AppUser { get; set; } = null!;

    public int? ParentId {get;set;}
    public Parent? Parent {get;set;}

    public int BranchId { get; set; }
    public Branch Branch { get; set; }
    public string? BloodType { get; set; }
    public string? HealthNotes { get; set; }

    public ICollection<StudentTestResult> TestResults { get; set; } = new List<StudentTestResult>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
}
