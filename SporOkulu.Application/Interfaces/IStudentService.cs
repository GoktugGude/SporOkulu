using System;
using SporOkulu.Domain;

namespace SporOkulu.Application.Interfaces;

public interface IStudentService : IGenericService<Student, DetailStudentDto,CreateStudentDto,UpdateStudentDto>
{

}

