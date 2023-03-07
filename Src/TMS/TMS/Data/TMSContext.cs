using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TMS.Models;

namespace TMS.Data
{
    public class TMSContext : IdentityDbContext
    {
        public TMSContext (DbContextOptions<TMSContext>options)
            : base(options)
        {
        }

        public DbSet<TMS.Models.EmployeeInfo> EmployeeInfo { get; set; } = default!;

        public DbSet<TMS.Models.VehicleInfo> VehicleInfo { get; set; } = default!;

        public DbSet<TMS.Models.RouteInfo> RouteInfo { get; set; } = default!;

        public DbSet<TMS.Models.Allocate> Allocate { get; set; } = default!;
    }
}
