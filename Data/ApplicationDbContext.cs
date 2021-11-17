using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notifier.Data.Common;
using Notifier.Data.Entities;
using Notifier.Data.Enums;

namespace Notifier.Data
{
    public sealed class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionText> SubscriptionTexts { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            ChangeTracker.StateChanged += UpdateTimestamps;
            ChangeTracker.Tracked += UpdateTimestamps;
        }

        private static void UpdateTimestamps(object sender, EntityEntryEventArgs e)
        {
            if (e.Entry.Entity is not IHasTimestamps entityWithTimestamps) return;
            switch (e.Entry.State)
            {
                case EntityState.Deleted:
                    entityWithTimestamps.Deleted = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entityWithTimestamps.Modified = DateTime.UtcNow;
                    break;
                case EntityState.Added:
                    entityWithTimestamps.Created = DateTime.UtcNow;
                    break;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var subscriptionTypeConverter = new EnumToStringConverter<SubscriptionType>();
            var subscriptionPolicyConverter = new EnumToStringConverter<SubscriptionPolicy>();
            var notificationStatusConverter = new EnumToStringConverter<NotificationStatus>();
            builder.Entity<Subscription>()
                .Property(s => s.Type)
                .HasConversion(subscriptionTypeConverter);            
            builder.Entity<Subscription>()
                .Property(s => s.Policy)
                .HasConversion(subscriptionPolicyConverter);
            builder.Entity<Notification>()
                .Property(s => s.Status)
                .HasConversion(notificationStatusConverter);
        }
    }
}