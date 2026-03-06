using System;
using SporOkulu.Domain.Entities;


namespace SporOkulu.Domain.Interfaces;

public interface IPaymentRepository : IGenericRepository<Payment>
{
    Task<List<Payment>> GetPaymentsAsync();
    Task<Payment> GetPaymentAsync(int id);
}
