namespace LocalStore.Domain.Models.ProductAggregate
{
    public interface IMeasurable
    {
        string MeasuringUnit { get; }
        decimal Quantity { get; }
    }
}
