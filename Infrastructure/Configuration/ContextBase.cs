using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options):base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConnectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        private string GetStringConnectionConfig()
        {
            string strCon = "Server=LAPTOP-CV9KCCO0\\SQLEXPRESS;Database=EcommerceDDD;Trusted_Connection=True;MultipleActiveResultSets=true";
            //string strCon = "Server=apolo.hostsrv.org,1433;Database=EcommerceDDD;User ID=cyberlacs777;Password=N75l%5sg";
            return strCon;
        }
    }
}
