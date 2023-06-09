﻿using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace MyBoards.Entities
{
    public class MyBoardContext : DbContext
    {
        public MyBoardContext(DbContextOptions<MyBoardContext> options) : base(options) { }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>(p =>
            {
                p.Property(x => x.Area).HasColumnType("varchar(200)");
                p.Property(x => x.IterationPath).HasColumnName("Iteration_Path");
                p.Property(x => x.Priority).HasDefaultValue(1);

                p.HasMany(x => x.Comments)
                .WithOne(c => c.WorkItem)
                .HasForeignKey(c => c.WorkItemId);

                p.HasOne(x => x.Author)
                .WithMany(u => u.WorkItems)
                .HasForeignKey(w => w.AuthorId);

                p.HasMany(x => x.Tags)
                .WithMany(t => t.WorkItems)
                .UsingEntity<WorkItemTag>(
                    w => w.HasOne(wit => wit.Tag)
                    .WithMany()
                    .HasForeignKey(wit => wit.TagId),
                    w => w.HasOne(wit => wit.WorkItem)
                    .WithMany()
                    .HasForeignKey(wit => wit.WorkItemId),
                    wit =>
                    {
                        wit.HasKey(x => new { x.TagId, x.WorkItemId });
                        wit.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                    }
                 );

                p.HasOne(s => s.State)
                .WithMany(wit => wit.WorkItems)
                .HasForeignKey(wit => wit.StateId);
            });

            modelBuilder.Entity<Epic>(p =>
            {
                p.Property(x => x.EndDate).HasPrecision(3);
            });

            modelBuilder.Entity<Issue>(p =>
            {
                p.Property(x => x.Efford).HasPrecision(5, 2);
            });

            modelBuilder.Entity<Task>(p =>
            {
                p.Property(x => x.RemaingWork).HasPrecision(5, 2);
                p.Property(x => x.Activity).HasMaxLength(200);
            });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<Comment>(p =>
            {
                p.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                p.Property(X => X.UpdatedDate).ValueGeneratedOnUpdate();

                p.HasOne(x => x.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<State>(p =>
            {
                p.Property(x => x.Value).IsRequired();
                p.Property(x => x.Value).HasColumnType("varchar(50)");
            });
        }
    }
}
