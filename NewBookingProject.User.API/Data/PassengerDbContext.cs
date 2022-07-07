using Microsoft.EntityFrameworkCore;
using NewBookingApp.Core.Data;

namespace NewBookingProject.Passenger.API.Data
{
    public class PassengerDbContext : DbContext, IUnitOfWork
    {

        public PassengerDbContext(DbContextOptions<PassengerDbContext> options) : base(options)
        {
        }

        public DbSet<Passenger> Passengers => Set<Passengers.Models.Passenger>();
        public Task<bool> Commit()
        {
            throw new NotImplementedException();
        }
    }
}
