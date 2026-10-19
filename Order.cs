namespace DotNet.Idempotency;

public record Order(Guid Id, string CustomerName, string ProductName, int Quantity, DateTime CreatedAt);
