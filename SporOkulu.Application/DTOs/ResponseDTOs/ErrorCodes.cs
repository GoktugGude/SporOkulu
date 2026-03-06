using System;

namespace SporOkulu.Application.DTOs.ResponseDTOs;

public static class ErrorCodes
{
    public const string NotFound = "NOT_FOUND";
    public const string BadRequest = "BAD_REQUEST";
    public const string Unauthorized = "UNAUTHORIZED";
    public const string Exception = "EXCEPTION";
    public const string Validation= "VALIDATION_ERROR";
}