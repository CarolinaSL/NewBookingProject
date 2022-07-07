using NewBookingApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregate
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
