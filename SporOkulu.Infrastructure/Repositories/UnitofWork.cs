using System;
using SporOkulu.Domain;
using SporOkulu.Domain.Interfaces;
using SporOkulu.Infrastructure.Context;

namespace SporOkulu.Infrastructure.Repositories;

public class UnitofWork : IUnitofWork
{
    private readonly AppDbContext _context;
    private  IParentRepository _parentRepository;
    private  IAttendanceRepository _attendanceRepository;
    private  ICoachRepository _coachRepository;
    private  IPaymentRepository _paymentRepository;
    private  IStudentRepository _studentRepository;

    public UnitofWork(AppDbContext context)
    {
        
        _context = context;
    }

    public IAttendanceRepository Attendance => _attendanceRepository  ??= new AttendanceRepository(_context);
    public ICoachRepository Coach { get => _coachRepository ??= new CoachRepository(_context); }
    public IParentRepository Parent { get => _parentRepository ??= new ParentRepository(_context); }
    public IPaymentRepository Payment { get => _paymentRepository ??= new PaymentRepository(_context);}
    public IStudentRepository Student { get => _studentRepository ??= new StudentRepository(_context);}

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
