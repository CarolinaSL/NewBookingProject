using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Core.Model
{
  
    public abstract class Aggregate<TId> : Entity, IAggregate<TId>
    {

        public TId Id { get; protected set; }
    }
}
