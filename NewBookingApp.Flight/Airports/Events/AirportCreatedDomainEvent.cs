using NewBookingApp.Core.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Flight.Domain.Airports.Events
{
    public record AirportCreatedDomainEvent(long Id, string Name, string Address, string Code) : IDomainEvent;
}
