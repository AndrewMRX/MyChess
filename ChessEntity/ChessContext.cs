using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChessEntity
{
    public class ChessContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }

        public ChessContext(DbContextOptions<ChessContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(u => u.WhiteGames)
                        .WithOne(g => g.WhitePlayer)
                        .HasForeignKey(g => g.PlayerIdWhite);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.BlackGames)
                        .WithOne(g => g.BlackPlayer)
                        .HasForeignKey(g => g.PlayerIdBlack);
        }

    }
}
