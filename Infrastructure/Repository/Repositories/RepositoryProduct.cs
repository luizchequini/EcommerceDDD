using Domain.Interfaces.InterfaceProduct;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<List<Produto>> ListarProdutos(Expression<Func<Produto, bool>> exProduto)
        {
            using (var banco = new ContextBase(_dbContextOptions))
            {
                return await banco.Produtos.Where(exProduto).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Produto>> ListarProdutosCarrinhoUsuário(string userId)
        {
            using (var banco = new ContextBase(_dbContextOptions))
            {
                var produtosCarrinhoUsuario = await (from p in banco.Produtos
                                                     join c in banco.CompraUsuarios on p.Id equals c.ProdutoId
                                                     where c.UserId.Equals(userId) && c.Estado == EnumEstadoCompra.Produto_Carrinho
                                                     select new Produto
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         QtdCompra = c.QtdCompra,
                                                         IdProdutoCarrinho = c.Id
                                                     }).AsNoTracking().ToListAsync();

                return produtosCarrinhoUsuario;
            }
        }

        public async Task<List<Produto>> ListarProdutosUsuario(string userId)
        {
            using (var banco = new ContextBase(_dbContextOptions))
            {
                return await banco.Produtos.Where(p => p.UserId == userId).AsNoTracking().ToListAsync();
            }
        }

        public async Task<Produto> ObterProdutoCarrinho(int idProdutoCarrinho)
        {
            using (var banco = new ContextBase(_dbContextOptions))
            {
                var produtosCarrinhoUsuario = await (from p in banco.Produtos
                                                     join c in banco.CompraUsuarios on p.Id equals c.ProdutoId
                                                     where c.Id.Equals(idProdutoCarrinho) && c.Estado == EnumEstadoCompra.Produto_Carrinho
                                                     select new Produto
                                                     {
                                                         Id = p.Id,
                                                         Nome = p.Nome,
                                                         Descricao = p.Descricao,
                                                         Observacao = p.Observacao,
                                                         Valor = p.Valor,
                                                         QtdCompra = c.QtdCompra,
                                                         IdProdutoCarrinho = c.Id
                                                     }).AsNoTracking().FirstOrDefaultAsync();

                return produtosCarrinhoUsuario;

            }
        }
    }
}
