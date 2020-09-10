namespace LocalStore.Domain.Models.OrderAggregate
{
    public interface IMeasurable
    {
        string MeasuringUnit { get; }
        decimal Quantity { get; }
    }
}
