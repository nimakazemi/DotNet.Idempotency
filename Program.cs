using DotNet.Idempotency;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

var orders = new List<Order>();

app.MapPost("/orders", (CreateOrderRequest request) =>
{
    var errors = new Dictionary<string, string[]>();

    if (string.IsNullOrWhiteSpace(request.CustomerName))
        errors["customerName"] = ["Customer name is required."];

    if (string.IsNullOrWhiteSpace(request.ProductName))
        errors["productName"] = ["Product name is required."];

    if (request.Quantity < 1)
        errors["quantity"] = ["Quantity must be at least 1."];

    if (errors.Count > 0)
        return Results.ValidationProblem(errors);

    var order = new Order(
        Id: Guid.NewGuid(),
        CustomerName: request.CustomerName.Trim(),
        ProductName: request.ProductName.Trim(),
        Quantity: request.Quantity,
        CreatedAt: DateTime.UtcNow);

    orders.Add(order);
    return Results.Created($"/orders/{order.Id}", order);
});

app.Run();
