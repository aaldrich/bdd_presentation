using Domain.Entities;

namespace Domain.Tasks
{
    public interface IShippingTask
    {
        bool ship(Order order);
    }
}