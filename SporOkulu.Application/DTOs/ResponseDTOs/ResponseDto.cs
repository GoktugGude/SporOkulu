using SporOkulu.Application.DTOs.CoachDTOs;

namespace SporOkulu.Application.DTOs.ResponseDTOs;

public class ResponseDto<T> where T : class
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public string? ErrorCode { get; set; } // Senin static class'ındaki kodlar buraya gelecek

    // Başarı Durumları
public static ResponseDto<T> SuccessResult(T data, string message = "İşlem başarılı.") 
    {
        return new ResponseDto<T> { Success = true, Data = data, Message = message };
    }

  public static ResponseDto<T> SuccessResult(string message) 
    {
        return new ResponseDto<T> { Success = true, Message = message };
    }

    // Hata Durumları (Hata kodunu da alıyoruz)
    public static ResponseDto<T> ErrorResult(string errorCode, string message) => 
        new() { Success = false, ErrorCode = errorCode, Message = message };

}