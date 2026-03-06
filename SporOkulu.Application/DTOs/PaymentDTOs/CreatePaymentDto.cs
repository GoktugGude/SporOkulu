using System.Security.Cryptography.X509Certificates;

namespace SporOkulu.Application;

public sealed record  CreatePaymentDto(
    int StudentId,
    decimal Amount,
    DateTime PaymentDate,
    string? Description
    );
