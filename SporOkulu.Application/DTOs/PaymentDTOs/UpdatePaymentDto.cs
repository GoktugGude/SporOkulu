namespace SporOkulu.Application;

public sealed record  UpdatePaymentDto(
    int Id,
    int StudentId,
    decimal Amount,
    DateTime PaymentDate,
    string? Description
    );
