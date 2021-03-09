using cs_asp_backend_server.Utility;
using Microsoft.EntityFrameworkCore;

namespace cs_asp_backend_server.Models
{
    public class EFRootContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseMySQL(Credentials.Instance.SQL_CONNECTION_STRING);
    }
}