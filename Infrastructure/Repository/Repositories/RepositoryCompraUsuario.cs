using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryCompraUsuario : RepositoryGenerics<CompraUsuario>, ICompraUsuario
    {
        private readonly DbContextOptions<ContextBase> _dbContextOptions;
        public RepositoryCompraUsuario()
        {
            _dbContextOptions = new DbContextOptions<ContextBase>();
        }

        public async Task<int> QuantidadeProdutosCarrinhoUsuario(string userId)
        {
            using (var banco = new ContextBase(_dbContextOptions))
            {
                return await banco.CompraUsuarios.CountAsync(c => c.UserId.Equals(userId) && c.Estado == EnumEstadoCompra.Produto_Carrinho);
            }
        }
    }
}
