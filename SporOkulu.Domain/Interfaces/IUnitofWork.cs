using System;

namespace SporOkulu.Domain.Interfaces;

public interface IUnitofWork : IDisposable
{
    IAttendanceRepository Attendance {get;}
    ICoachRepository Coach {get;}
    IParentRepository Parent {get;}
    IPaymentRepository Payment {get;}
    IStudentRepository Student {get;}

    Task<int> SaveChangesAsync();
}
