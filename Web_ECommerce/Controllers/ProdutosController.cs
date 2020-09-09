using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;

        public readonly InterfaceProductApp _interfaceProductApp;

        public readonly InterfaceCompraUsuarioApp _interfaceCompraUsuarioApp;
        public ProdutosController(InterfaceProductApp interfaceProductApp, UserManager<ApplicationUser> userManager, InterfaceCompraUsuarioApp interfaceCompraUsuarioApp)
        {
            _userManager = userManager;
            _interfaceProductApp = interfaceProductApp;
            _interfaceCompraUsuarioApp = interfaceCompraUsuarioApp;
        }

        // GET: ProdutosController
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarUsuarioLogado();

            return View(await _interfaceProductApp.ListarProdutosUsuario(idUsuario));
        }

        // GET: ProdutosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // GET: ProdutosController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                var idUsuario = await RetornarUsuarioLogado();
                produto.UserId = idUsuario;

                await _interfaceProductApp.AddProduct(produto);

                if (produto.Notificacoes.Any())
                {
                    foreach (var item in produto.Notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.Mensagem);
                    }

                    return View("Create", produto);
                }
            }
            catch
            {
                return View("Create", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _interfaceProductApp.UpdateProduct(produto);

                if (produto.Notificacoes.Any())
                {
                    foreach (var item in produto.Notificacoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.Mensagem);
                    }

                    ViewBag.Alerta = true;
                    ViewBag.Mensagem = "Verifique, ocorreu algum erro!";

                    return View("Edit", produto);
                }


            }
            catch
            {
                return View("Edit", produto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _interfaceProductApp.GetEntityById(id);

                await _interfaceProductApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> RetornarUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);

            return idUsuario.Id;
        }

        //GET
        [AllowAnonymous]
        [HttpGet("/api/ListaProdutoComEstoque")]
        public async Task<JsonResult> ListaProdutoComEstoque()
        {
            return Json(await _interfaceProductApp.ListarProdutosComEstoque());
        }

        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var idUsuario = await RetornarUsuarioLogado();

            return View(await _interfaceProductApp.ListarProdutosCarrinhoUsuário(idUsuario));
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            return View(await _interfaceProductApp.ObterProdutoCarrinho(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _interfaceCompraUsuarioApp.GetEntityById(id);

                await _interfaceCompraUsuarioApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch
            {
                return View();
            }
        }
    }
}
