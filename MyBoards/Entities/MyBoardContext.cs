using Microsoft.EntityFrameworkCore;

namespace MyBoards.Entities
{
    public class MyBoardContext : DbContext
    {
        public MyBoardContext(DbContextOptions<MyBoardContext> options) : base(options) { }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<WorkItem>(p =>
        //    {
        //        p.Property(x => x.State).IsRequired();
        //        p.Property(x => x.Area).HasColumnType("varchar(200)");
        //        p.Property(x => x.IterationPath).HasColumnName("Iteration_Path");
        //        p.Property(x => x.Efford).HasColumnType("decimal(5, 2)");
        //        p.Property(x => x.EndDate).HasPrecision(3);
        //        p.Property(x => x.Activity).HasMaxLength(200);
        //        p.Property(x => x.RemaingWork).HasPrecision(14, 2);
        //    });
        //}
    }
}
