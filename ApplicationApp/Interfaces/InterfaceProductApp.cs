using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceProductApp : InterfaceGenericaApp<Produto>
    {
        Task AddProduct(Produto produto);

        Task UpdateProduct(Produto produto);

        Task<List<Produto>> ListarProdutosUsuario(string userId);

        Task<List<Produto>> ListarProdutosComEstoque();

        Task<List<Produto>> ListarProdutosCarrinhoUsuário(string userId);

        Task<Produto> ObterProdutoCarrinho(int idProdutoCarrinho);
    }
}
