using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Domain;

public class AppUser : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } =string.Empty;

    public Student? Student {get;set;}
    public Parent? Parent {get;set;}
    public Coach? Coach {get;set;}
}
