using SporOkulu.Domain.Entities;

namespace SporOkulu.Domain;

public class Branch
{
    public int Id {get; set;}
    public string Name {get;set;} = string.Empty;
    public decimal? MonthlyFee { get; set; }
    public List<Student> students {get;set;} = new List<Student>();
    public ICollection<Coach> Coaches { get; set; } = new List<Coach>();

}
