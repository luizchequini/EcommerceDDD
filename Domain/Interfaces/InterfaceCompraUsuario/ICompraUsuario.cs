using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceCompraUsuario
{
    public interface ICompraUsuario : IGeneric<CompraUsuario>
    {
        public Task<int> QuantidadeProdutosCarrinhoUsuario(string userId);
    }
}
