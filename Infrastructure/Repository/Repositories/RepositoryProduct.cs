using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryProduct : RepositoryGenerics<Produto>, IProduct
    {
        private readonly DbContextOptions<ContextBase> _dbContextOptions;
        public RepositoryProduct()
        {
            _dbContextOptions = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Produto>> ListarProdutosUsuario(string userId)
        {
            using(var banco = new ContextBase(_dbContextOptions))
            {
                return await banco.Produtos.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
            }
        }
    }
}
