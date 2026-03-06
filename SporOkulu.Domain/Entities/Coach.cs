using System;

namespace SporOkulu.Domain.Entities;

public class Coach
{
    public int Id { get; set; }
    public decimal Salary { get; set; }
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public int BranchId { get; set; }
    public Branch Branch { get; set; } = null!;
}
