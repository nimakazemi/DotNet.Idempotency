namespace DotNet.Idempotency;

public record CreateOrderRequest(string CustomerName, string ProductName, int Quantity);
