using System;

namespace SporOkulu.Domain.Entities;

public class Parent
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
