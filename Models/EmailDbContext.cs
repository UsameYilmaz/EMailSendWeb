
using Microsoft.EntityFrameworkCore;

namespace EMailSendWeb.Models//veritabanı baglnatısı.
{
    public class EmailDbContext : DbContext
    {
       
        public EmailDbContext() : base() { } 
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options) { } 
        public DbSet<Email> Emails { get; set; } 
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{ 
        //    modelBuilder.ApplyConfiguration(new EmailMap()); 
        //} 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            optionsBuilder.UseSqlServer(connectionString: @"Server = (localdb)\MSSQLLocalDB; Database = EmailDb; Trusted_Connection = true"); 
        }
    }
}


