using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppProduct : InterfaceProductApp
    {
        private readonly IProduct _iProduct;
        private readonly IServiceProduct _iServiceProduct;

        public AppProduct(IProduct iProduct, IServiceProduct iServiceProduct)
        {
            _iProduct = iProduct;
            _iServiceProduct = iServiceProduct;
        }

        public async Task<List<Produto>> ListarProdutosUsuario(string userId)
        {
            return await _iProduct.ListarProdutosUsuario(userId);
        }

        public async Task AddProduct(Produto produto)
        {
            await _iServiceProduct.AddProduct(produto);
        }

        public async Task UpdateProduct(Produto produto)
        {
            await _iServiceProduct.UpdateProduct(produto);
        }

        public async Task Add(Produto produto)
        {
            await _iProduct.Add(produto);
        }

        public async Task Delete(Produto produto)
        {
            await _iProduct.Delete(produto);
        }

        public async Task<Produto> GetEntityById(int id)
        {
            return await _iProduct.GetEntityById(id);
        }

        public async Task<List<Produto>> List()
        {
            return await _iProduct.List();
        }

        public async Task Update(Produto produto)
        {
            await _iProduct.Update(produto);
        }

        public async Task<List<Produto>> ListarProdutosComEstoque()
        {
            return await _iServiceProduct.ListarProdutosComEstoque();
        }

        public async Task<List<Produto>> ListarProdutosCarrinhoUsuário(string userId)
        {
            return await _iProduct.ListarProdutosCarrinhoUsuário(userId);
        }

        public async Task<Produto> ObterProdutoCarrinho(int idProdutoCarrinho)
        {
            return await _iProduct.ObterProdutoCarrinho(idProdutoCarrinho);
        }
    }
}
