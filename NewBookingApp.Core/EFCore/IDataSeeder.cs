using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBookingApp.Core.EFCore
{
    public interface IDataSeeder
    {
        Task SeedAllAsync();
    }
}
