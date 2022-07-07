using NewBookingApp.Core.Model;
using NewBookingApp.Flight.Domain.Aircrafts.Events;

namespace NewBookingApp.Flight.Domain.Aircrafts.Models
{
    public class Aircraft : Aggregate<long>
    {
        public Aircraft()
        {
        }

        public string Name { get; private set; }
        public string Model { get; private set; }

        public static Aircraft Create(long id, string name, string model)
        {
            var aircraft = new Aircraft
            {
                Id = id,
                Name = name,
                Model = model,
            };

            var @event = new AircraftCreatedDomainEvent(
                aircraft.Id,
                aircraft.Name,
                aircraft.Model);

            

            return aircraft;
        }
    }
}
