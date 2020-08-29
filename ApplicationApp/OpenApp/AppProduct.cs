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
        IProduct _iProduct;
        IServiceProduct _iServiceProduct;

        public AppProduct(IProduct iProduct, IServiceProduct iServiceProduct)
        {
            _iProduct = iProduct;
            _iServiceProduct = iServiceProduct;
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

    }
}
