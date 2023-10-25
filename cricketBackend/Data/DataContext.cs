using cricketBackend.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cricketBackend.Data
{
    public class DataContext: DbContext
    {
        private readonly ConnectionStrings _dbConnection;
        public DataContext(DbContextOptions<DataContext> option, IOptions<ConnectionStrings> dbConnection) : base(option)
        {
            _dbConnection = dbConnection.Value;
        }

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(_dbConnection.DbConnection,
                   builder => builder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Text).HasColumnName("text");
                entity.Property(e => e.DateAdded).HasColumnName("dateAdded");
            });
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Text).HasColumnName("text");
                entity.Property(e => e.PostId).HasColumnName("postId");
            });
            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("Like");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PostId).HasColumnName("postId");
            });

        }
    }
}
