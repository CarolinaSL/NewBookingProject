using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Core.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out T> : IRequest<T>
        where T : notnull
    {
    }
}
