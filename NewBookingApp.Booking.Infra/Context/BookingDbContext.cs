using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewBookingApp.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Booking.Infra.Context
{
    public class BookingDbContext : DbContext, IUnitOfWork
    {
     

        public BookingDbContext(DbContextOptions<BookingDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
        }

        public DbSet<Domain.Models.Booking> Bookings => Set<Domain.Models.Booking>();

        public async Task<bool> Commit()
        {
            var sucess = await base.SaveChangesAsync() > 0;

            return sucess;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }

}
