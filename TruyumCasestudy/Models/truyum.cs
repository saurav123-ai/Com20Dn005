using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Truyumcasestudy.Models
{
    public partial class truyum : DbContext
    {
        public truyum()
            : base("name=truyum")
        {
        }

        public virtual DbSet<menu> menus { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<menu>()
                .Property(e => e.menu_name)
                .IsUnicode(false);

            modelBuilder.Entity<menu>()
                .Property(e => e.active)
                .IsUnicode(false);

            modelBuilder.Entity<menu>()
                .Property(e => e.category)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
