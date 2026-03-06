using System;
using Microsoft.EntityFrameworkCore;
using SporOkulu.Domain;
using SporOkulu.Domain.Interfaces;
using SporOkulu.Infrastructure.Context;

namespace SporOkulu.Infrastructure.Repositories;

public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Payment?> GetPaymentAsync(int id)
    {
        return await _context.Set<Payment>()
            .Include(x => x.Student)
            .ThenInclude(x => x.AppUser)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Payment>> GetPaymentsAsync()
    {
        return await _context.Set<Payment>()
            .Include(p => p.Student)
            .ThenInclude(p => p.AppUser)
            .ToListAsync();
    }
}
