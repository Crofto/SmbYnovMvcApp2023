using Microsoft.EntityFrameworkCore;
using SmbYnovMvcApp2023.Models;

namespace SmbYnovMvcApp2023.Data
{
    public class AppYnovContext : DbContext
    {
        public AppYnovContext( DbContextOptions<AppYnovContext> options) : base(options)
        { }
        public DbSet<Device> Devices { get; set; }
        public DbSet<UplinkMessage> UplinkMessages { get; set; }
        public DbSet<TimeSeries> TimeSeries { get; set; }
        public DbSet<ServerSettings> ServerSettings { get; set; }
        
    }
}
