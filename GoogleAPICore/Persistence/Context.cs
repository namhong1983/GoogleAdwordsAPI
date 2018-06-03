using GoogleAPICore.Models;
using Microsoft.EntityFrameworkCore;

namespace GoogleAPICore.Persistence
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<OAuthLogin> OAuthLogin { get; set; }
    }
}
