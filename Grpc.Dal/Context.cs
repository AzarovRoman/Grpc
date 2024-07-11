using Grpc.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Grpc.Dal
{
    public class Context : DbContext
    {
        public DbSet<StudentEntity> Students { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetSchema("NotPublic");
            }
        }
    }
}