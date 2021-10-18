using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Lotto3000App.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Lotto3000App.Shared.Enums;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel;

namespace Lotto3000App.DataAccess
{
    public class Lotto3000AppDbContext : DbContext
    {

        public Lotto3000AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var converter = new BoolToStringConverter("False", "True");

            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes("password"));
            var hashedPassword = Encoding.ASCII.GetString(md5Data);


            modelBuilder.Entity<Admin>()
                .Property(x => x.FirstName)
                .HasMaxLength(50);
            modelBuilder.Entity<Admin>()
                .Property(x => x.LastName)
                .HasMaxLength(50);
            modelBuilder.Entity<Admin>()
                .Property(x => x.Username)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Admin>()
                .Property(x => x.IsAdmin)
                .HasConversion(converter);
            modelBuilder.Entity<Admin>()
                .HasData(
                    new Admin()
                    {
                        Id = 1,
                        FirstName = "Jana",
                        LastName = "Simjanovska",
                        Username = "admin",
                        Password = hashedPassword
                    }
                );

            var md5Data1 = md5.ComputeHash(Encoding.ASCII.GetBytes("password123"));
            var hashedPassword1 = Encoding.ASCII.GetString(md5Data1);
            modelBuilder.Entity<Player>()
                .Property(x => x.FirstName)
                .HasMaxLength(50);
            modelBuilder.Entity<Player>()
                .Property(x => x.LastName)
                .HasMaxLength(50);
            modelBuilder.Entity<Player>()
                .Property(x => x.Username)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Player>()
                .Property(x => x.IsAdmin)
                .HasConversion(converter);
            modelBuilder.Entity<Player>()
                .HasMany(x => x.SubmittedTickets);
            //modelBuilder.Entity<Player>()
            //    .HasData(
            //        new Player
            //        {
            //            Id = -1,
            //            FirstName = "Pero",
            //            LastName = "Blazovski",
            //            Username = "pero123",
            //            Password = hashedPassword1,

            //        }
            //    );


            modelBuilder.Entity<Ticket>()
                .HasOne(x => x.Player)
                .WithMany(x => x.SubmittedTickets)
                .HasForeignKey(x => x.PlayerId);
            modelBuilder.Entity<Ticket>()
                .Property(x => x.IsWinning)
                .HasConversion(converter);
            modelBuilder.Entity<Ticket>()
                .Property(x => x.Prize)
                .HasConversion(
                    x => x.ToString(),
                    x => (Prize)Enum.Parse(typeof(Prize), x)
                );
            modelBuilder.Entity<Ticket>()
                .HasOne(x => x.Session)
                .WithMany(x => x.SubmittedTickets)
                .HasForeignKey(x => x.SessionId);
            //modelBuilder.Entity<Ticket>()
            //    .HasData(
            //        new Ticket
            //        {
            //            SessionId = -1,
            //            PlayerId = -1,
            //            Id = -1,
            //        }
            //     );

            modelBuilder.Entity<Session>()
                .Property(x => x.Duration)
                .HasConversion(new TimeSpanToStringConverter());
            modelBuilder.Entity<Session>()
                .Property(x => x.StartOfSession)
                .HasConversion(new DateTimeToStringConverter());
            modelBuilder.Entity<Session>()
                .Property(x => x.EndOfSession)
                .HasConversion(new DateTimeToStringConverter())
                .IsRequired();
            modelBuilder.Entity<Session>()
                .Ignore(x => x.WinningCombination);
            modelBuilder.Entity<Session>()
                .HasMany(x => x.SubmittedTickets);
            modelBuilder.Entity<Session>()
                .Ignore(x => x.WinningTickets);
            //modelBuilder.Entity<Session>()
            //    .HasData(
            //        new Session
            //        {
            //            Id = -1,
            //        }
            //    );

            modelBuilder.Entity<EnteredCombination>()
                .Property(x => x.Number1)
                .IsRequired();
            modelBuilder.Entity<EnteredCombination>()
                .Property(x => x.Number2)
                .IsRequired();
            modelBuilder.Entity<EnteredCombination>()
                .Property(x => x.Number3)
                .IsRequired();
            modelBuilder.Entity<EnteredCombination>()
                .Property(x => x.Number4)
                .IsRequired();
            modelBuilder.Entity<EnteredCombination>()
                .Property(x => x.Number5)
                .IsRequired();
            modelBuilder.Entity<EnteredCombination>()
                .Property(x => x.Number6)
                .IsRequired();
            modelBuilder.Entity<EnteredCombination>()
                .Property(x => x.Number7)
                .IsRequired();
            modelBuilder.Entity<EnteredCombination>()
                .HasOne(x => x.Ticket)
                .WithOne(x => x.EnteredCombination)
                .HasForeignKey<EnteredCombination>(x => x.TicketId);
            modelBuilder.Entity<EnteredCombination>()
                .Ignore(x => x.EnteredNumbersList);
            //modelBuilder.Entity<EnteredCombination>()

            //    .HasData(
            //        new EnteredCombination
            //        {
            //            Id= -1,
            //            Number1 = 2,
            //            Number2 = 5,
            //            Number3 = 7,
            //            Number4 = 22,
            //            Number5 = 28,
            //            Number6 = 33,
            //            Number7 = 11,
            //            TicketId = -1

            //        }
            //    );

            modelBuilder.Entity<WinningCombination>()
                .HasOne(x => x.Session)
                .WithOne(x => x.WinningCombination)
                .HasForeignKey<WinningCombination>(x => x.SessionId);
            modelBuilder.Entity<WinningCombination>()
                .Ignore(x => x.WinningComboList);

        }
    }
}
