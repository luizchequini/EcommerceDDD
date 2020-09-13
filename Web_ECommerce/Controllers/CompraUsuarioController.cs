using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web_ECommerce.Models.Helper;

namespace Web_ECommerce.Controllers
{
    public class CompraUsuarioController : HelperQrCode
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InterfaceCompraUsuarioApp _interfaceCompraUsuarioApp;
        private IWebHostEnvironment _webHostEnvironment;

        public CompraUsuarioController(UserManager<ApplicationUser> userManager, InterfaceCompraUsuarioApp interfaceCompraUsuarioApp, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _interfaceCompraUsuarioApp = interfaceCompraUsuarioApp;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Imprimir()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _interfaceCompraUsuarioApp.ProdutosComprados(usuario.Id);

            return await Download(compraUsuario, _webHostEnvironment);
        }

        public async Task<IActionResult> FinalizarCompra()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _interfaceCompraUsuarioApp.CarrinhoCompras(usuario.Id);

            return View(compraUsuario);
        }

        public async Task<IActionResult> MinhasCompras(bool mensagem = false)
        {
            var usuario = await _userManager.GetUserAsync(User);
            var compraUsuario = await _interfaceCompraUsuarioApp.ProdutosComprados(usuario.Id);

            if (mensagem)
            {
                ViewBag.Sucesso = true;
                ViewBag.Mensagem = "Compra efetivada com sucesso. Pague o boleto para garantir sua compra!";
            }

            return View(compraUsuario);
        }

        public async Task<IActionResult> ComfirmaCompra()
        {
            var usuario = await _userManager.GetUserAsync(User);
            var sucesso = await _interfaceCompraUsuarioApp.ConfirmaCompraCarrinhoUsuario(usuario.Id);

            if (sucesso)
            {
                return RedirectToAction("MinhasCompras", new { mensagem = true });
            }
            else
            {
                return RedirectToAction("FinalizarCompra");
            }
        }

        [HttpPost("/api/AdicionaProdutoNoCarrinho")]
        public async Task<JsonResult> AdicionaProdutoNoCarrinho(string id, string nome, string qtd)
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario != null)
            {
                await _interfaceCompraUsuarioApp.Add(new CompraUsuario
                {
                    ProdutoId = Convert.ToInt32(id),
                    QtdCompra = Convert.ToInt32(qtd),
                    Estado = EnumEstadoCompra.Produto_Carrinho,
                    UserId = usuario.Id
                });

                return Json(new { sucesso = true });
            }

            return Json(new { sucesso = false });
        }

        [HttpGet("/api/QuantidadeProdutoCarrinhoUsuario")]
        public async Task<JsonResult> QuantidadeProdutoCarrinhoUsuario()
        {
            var usuario = await _userManager.GetUserAsync(User);

            var qtd = 0;

            if (usuario != null)
            {
                qtd = await _interfaceCompraUsuarioApp.QuantidadeProdutoCarrinhoUsuario(usuario.Id);

                return Json(new { sucesso = true, qtd = qtd });
            }

            return Json(new { sucesso = false, qtd = qtd });
        }
    }
}
