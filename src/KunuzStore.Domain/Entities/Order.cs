// Entities/Order.cs
using KunuzStore.Domain.Enums;
using KunuzStore.Domain.Exceptions;

namespace KunuzStore.Domain.Entities;

public class Order
{
    private readonly List<OrderItem> _items = new();

    public Guid Id { get; private set; }
    public string OrderNumber { get; private set; } = default!;
    public string CustomerName { get; private set; } = default!;
    public string CustomerPhone { get; private set; } = default!;
    public string Governorate { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string? Notes { get; private set; }
    public decimal Total { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.New;
    public string IdempotencyKey { get; private set; } = default!;
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order() { }   // EF Core فقط

    public static Order Create(string orderNumber, string customerName, string customerPhone,
        string governorate, string address, string? notes, string idempotencyKey) => new()
        {
            Id = Guid.NewGuid(),
            OrderNumber = orderNumber,
            CustomerName = customerName,
            CustomerPhone = customerPhone,
            Governorate = governorate,
            Address = address,
            Notes = notes,
            IdempotencyKey = idempotencyKey,
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.New,
        };

    public void AddItem(Guid productId, string name, decimal price, int qty,
        string? variantLabel, string? variantChoice)
    {
        RequireStatus(OrderStatus.New);
        if (qty <= 0) throw new DomainException("Quantity must be greater than zero.");
        if (price < 0) throw new DomainException("Price cannot be negative.");

        _items.Add(new OrderItem(productId, name, price, qty, variantLabel, variantChoice));
        Total = _items.Sum(i => i.Price * i.Qty);
    }

    public void Confirm()
    {
        if (_items.Count == 0) throw new DomainException("Cannot confirm an order with no items.");
        RequireStatus(OrderStatus.New);
        Status = OrderStatus.Confirmed;
    }

    public void MarkPreparing() => Transition(from: OrderStatus.Confirmed, to: OrderStatus.Preparing);
    public void MarkShipped() => Transition(from: OrderStatus.Preparing, to: OrderStatus.Shipped);
    public void MarkDelivered() => Transition(from: OrderStatus.Shipped, to: OrderStatus.Delivered);

    public void Cancel()
    {
        if (Status is OrderStatus.Delivered or OrderStatus.Cancelled)
            throw new DomainException($"Cannot cancel an order that is already {Status}.");
        Status = OrderStatus.Cancelled;
    }

    private void RequireStatus(OrderStatus required)
    {
        if (Status != required)
            throw new DomainException($"Order must be {required}, but is {Status}.");
    }

    private void Transition(OrderStatus from, OrderStatus to)
    {
        RequireStatus(from);
        Status = to;
    }
}