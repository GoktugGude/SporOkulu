using System;
using AutoMapper;
using SporOkulu.Application.DTOs.ResponseDTOs;
using SporOkulu.Application.Interfaces;
using SporOkulu.Domain;
using SporOkulu.Domain.Interfaces;

namespace SporOkulu.Application.Services;

public class StudentManager : GenericManager<Student, DetailStudentDto, CreateStudentDto, UpdateStudentDto> , IStudentService
{
    private readonly IStudentRepository _studentRepository;


    public StudentManager(IStudentRepository studentRepository, IMapper mapper, IStudentRepository repository) : base(studentRepository, mapper)
    {
        _studentRepository = studentRepository;
    }

    public override async Task<ResponseDto<List<DetailStudentDto>>> GetAllAsync()
    {
        var data = await _studentRepository.GetStudentsAsync();
        var dto = _mapper.Map<List<DetailStudentDto>>(data);
        return ResponseDto<List<DetailStudentDto>>.SuccessResult(dto);
    }

    public override async Task<ResponseDto<DetailStudentDto>> GetByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentAsync(id);
        if(student == null) return ResponseDto<DetailStudentDto>.ErrorResult(ErrorCodes.NotFound, "Öğrenci bulunamadı.");
        var dto = _mapper.Map<DetailStudentDto>(student);
        return ResponseDto<DetailStudentDto>.SuccessResult(dto);
    }


}
