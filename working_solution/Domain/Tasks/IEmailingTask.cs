using Domain.Entities;

namespace Domain.Tasks
{
    public interface IEmailingTask
    {
        bool send(Order order);
    }
}