using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<FriendshipStatus>()
                .HasOne(x => x.RequesterUser)
                .WithMany(a => a.SentFriendRequests)
                .HasForeignKey(f => f.RequesterUserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<FriendshipStatus>()
                .HasOne(x => x.AddresseeUser)
                .WithMany(a => a.ReceievedFriendRequests)
                .HasForeignKey(f => f.AddresseeUserId)
                .OnDelete(DeleteBehavior.Restrict); 


        }

        public DbSet<FriendshipStatus> FriendshipStatuses { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
