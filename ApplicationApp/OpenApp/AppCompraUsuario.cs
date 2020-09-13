using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceCompraUsuario;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppCompraUsuario : InterfaceCompraUsuarioApp
    {
        private readonly ICompraUsuario _ICompraUsuario;
        private readonly IServiceCompraUsuario _iServiceCompraUsuario;

        public AppCompraUsuario(ICompraUsuario CompraUsuario, IServiceCompraUsuario iServiceCompraUsuario)
        {
            _ICompraUsuario = CompraUsuario;
            _iServiceCompraUsuario = iServiceCompraUsuario;
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

        public async Task<CompraUsuario> CarrinhoCompras(string userId)
        {
            return await _iServiceCompraUsuario.CarrinhoCompras(userId);
        }

        public async Task<CompraUsuario> ProdutosComprados(string userId)
        {
            return await _iServiceCompraUsuario.ProdutosComprados(userId);
        }

        public async Task<bool> ConfirmaCompraCarrinhoUsuario(string userId)
        {
            return await _ICompraUsuario.ConfirmarCompraCarrinhoUsuario(userId);
        }
    }
}
