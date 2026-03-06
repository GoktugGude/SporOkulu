using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain;

namespace SporOkulu.Application;

public interface IPaymentService : IGenericService<Payment,DetailPaymentDto,CreatePaymentDto,UpdatePaymentDto> 
{
    Task<ResponseDto<List<DetailStudentDto>>> GetUnpaidStudentsAsync(int month, int year);
}
