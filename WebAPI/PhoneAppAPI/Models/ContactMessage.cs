namespace PhoneAppAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContactMessage : DbContext
    {
        public ContactMessage()
            : base("name=ContactMessage")
        {
        }

        public virtual DbSet<Contact_message> Contact_message { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact_message>()
                .Property(e => e.full_name)
                .IsUnicode(false);

            modelBuilder.Entity<Contact_message>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Contact_message>()
                .Property(e => e.full_message)
                .IsUnicode(false);
        }
    }
}
