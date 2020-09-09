using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options):base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CompraUsuario> CompraUsuarios { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConnectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        private string GetStringConnectionConfig()
        {
            string strCon = "Server=LAPTOP-CV9KCCO0\\SQLEXPRESS01;Database=EcommerceDDD;User Id=sa;password=1234;Trusted_Connection=False;MultipleActiveResultSets=true;";
            return strCon;
        }
    }
}
