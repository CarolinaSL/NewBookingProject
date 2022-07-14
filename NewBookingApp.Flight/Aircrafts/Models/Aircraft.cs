using NewBookingApp.Core.Model;

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


            return aircraft;
        }
    }
}
