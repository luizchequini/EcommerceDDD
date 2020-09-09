using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCompraUsuario;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppCompraUsuario : InterfaceCompraUsuarioApp
    {
        private readonly ICompraUsuario _ICompraUsuario;

        public AppCompraUsuario(ICompraUsuario CompraUsuario)
        {
            _ICompraUsuario = CompraUsuario;
        }

        public async Task<int> QuantidadeProdutoCarrinhoUsuario(string userId)
        {
            return await _ICompraUsuario.QuantidadeProdutosCarrinhoUsuario(userId);
        }

        public async Task Add(CompraUsuario compraUsuario)
        {
            await _ICompraUsuario.Add(compraUsuario);
        }

        public async Task Delete(CompraUsuario compraUsuario)
        {
            await _ICompraUsuario.Delete(compraUsuario);
        }

        public async Task<CompraUsuario> GetEntityById(int id)
        {
            return await _ICompraUsuario.GetEntityById(id);
        }

        public async Task<List<CompraUsuario>> List()
        {
            return await _ICompraUsuario.List();
        }

        public async Task Update(CompraUsuario compraUsuario)
        {
            await _ICompraUsuario.Update(compraUsuario);
        }
    }
}
