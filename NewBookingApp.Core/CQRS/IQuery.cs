using MediatR;

namespace NewBookingApp.Core.CQRS
{
    public interface IQuery<out T> : IRequest<T>
    where T : notnull
    {
    }
}
