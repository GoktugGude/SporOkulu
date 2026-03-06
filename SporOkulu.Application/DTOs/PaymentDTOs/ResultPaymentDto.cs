namespace SporOkulu.Application;

public sealed record  DetailPaymentDto(
    int Id,
    string StudentName,
    decimal Amount,
    DateTime PaymentDate,
    string? Description
    )
    {
    // AutoMapper v12 için "Boş Constructor" (Parametresiz)
    public DetailPaymentDto() : this(0, string.Empty, 0m, DateTime.Now, null)
    {
    }
}
