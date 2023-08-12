 using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Final_Project.Models;
using Org.BouncyCastle.Bcpg;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Final_Project.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Pricing> Pricings { get; set; }
        public DbSet<VideoQuality> VideoQualities { get; set; }
        public DbSet<Resolution> Resolutions { get; set; }

        public DbSet<BlogAuthor> BlogAuthors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmImage> FilmImages { get; set; }
        public DbSet<FilmTopic> FilmTopics { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceOptions> ServiceOptions { get; set; }
        public DbSet<Streaming> Streamings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Film>()
            .HasMany(e => e.Episodes)
            .WithOne(e => e.Film)
            .OnDelete(DeleteBehavior.Restrict);
        }

    }
}