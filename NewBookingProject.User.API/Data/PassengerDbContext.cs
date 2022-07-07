using Microsoft.EntityFrameworkCore;
using NewBookingApp.Core.Data;

namespace NewBookingProject.Passenger.API.Data
{
    public class PassengerDbContext : DbContext, IUnitOfWork
    {

        public PassengerDbContext(DbContextOptions<PassengerDbContext> options) : base(options)
        {
        }

        public DbSet<Passengers.Models.Passenger> Passengers => Set<Passengers.Models.Passenger>();
        public async Task<bool> Commit()
        {
            var sucess = await base.SaveChangesAsync() > 0;

            return sucess;
        }
    }
}
