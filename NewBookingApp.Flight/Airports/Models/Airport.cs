using NewBookingApp.Core.Model;
using NewBookingApp.Flight.Domain.Airports.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Flight.Domain.Airports.Models
{
    public class Airport : Aggregate<long>
    {
        public Airport()
        {
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Code { get; private set; }

        public static Airport Create(long id, string name, string address, string code)
        {
            var airport = new Airport
            {
                Id = id,
                Name = name,
                Address = address,
                Code = code
            };

            var @event = new AirportCreatedDomainEvent(
                airport.Id,
                airport.Name,
                airport.Address,
                airport.Code);

           // airport.AddDomainEvent(@event);

            return airport;
        }
    }
}
