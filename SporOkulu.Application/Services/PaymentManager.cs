using AutoMapper;
using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Services;
using SporOkulu.Domain;
using SporOkulu.Domain.Interfaces;

namespace SporOkulu.Application;

public class PaymentManager : GenericManager<Payment, DetailPaymentDto, CreatePaymentDto, UpdatePaymentDto>, IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    public PaymentManager(IMapper mapper, IPaymentRepository paymentRepository) : base(paymentRepository, mapper)
    {
        // _studentRepository = studentRepository;
        _paymentRepository = paymentRepository;
    }

    // public async Task<ResponseDto<List<DetailStudentDto>>> GetUnpaidStudentsAsync(int month, int year)
    // {
    //     var allStudents = await _studentRepository.GetAllAsync2("AppUser", "Branch");

    //     var bulundugumuzAyOdeme =  _repository.GetWhere(p => p.PaymentDate.Month == month && p.PaymentDate.Year == year);

        
    //     var padStudentIds = bulundugumuzAyOdeme.Select(p => p.StudentId).ToList();

    //     var unpaidStudents = allStudents.Where(s => !padStudentIds.Contains(s.Id)).ToList();

    //     var dtos = _mapper.Map<List<DetailStudentDto>>(unpaidStudents);

    //     return ResponseDto<List<DetailStudentDto>>.SuccessResult(dtos, $"{month}/{year} dönemi ödeme yapmayanlar başarıyla listelendi.");
    // }

    // protected override string[] Includes => new [] {"Student.AppUser"};
    public override async Task<ResponseDto<List<DetailPaymentDto>>> GetAllAsync()
    {
        var data = await _paymentRepository.GetPaymentsAsync();
        var dto = _mapper.Map<List<DetailPaymentDto>>(data);
        return ResponseDto<List<DetailPaymentDto>>.SuccessResult(dto);
    }
    public override async Task<ResponseDto<DetailPaymentDto>> GetByIdAsync(int id)
    {
        var data = await _paymentRepository.GetPaymentAsync(id);
        if(data == null) return ResponseDto<DetailPaymentDto>.ErrorResult(ErrorCodes.NotFound,"Ödeme Bulunamadı.");
        var dto = _mapper.Map<DetailPaymentDto>(data);
        return ResponseDto<DetailPaymentDto>.SuccessResult(dto);
    }

    public Task<ResponseDto<List<DetailStudentDto>>> GetUnpaidStudentsAsync(int month, int year)
    {
        throw new NotImplementedException();
    }
}
