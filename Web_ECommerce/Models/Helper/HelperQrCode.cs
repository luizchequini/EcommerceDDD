using Entities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Web_ECommerce.Models.Helper
{
    public class HelperQrCode : Controller
    {
        private async Task<byte[]> GeraQrCode(string dadosBanco)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(dadosBanco, QRCodeGenerator.ECCLevel.H);

            QRCode qRCode = new QRCode(qRCodeData);

            Bitmap qrCodeImage = qRCode.GetGraphic(20);

            var bitmapBytes = BitmapToBytes(qrCodeImage);

            return bitmapBytes;
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using(MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                return stream.ToArray();
            }
        }

        public async Task<IActionResult> Download(CompraUsuario compraUsuario, IWebHostEnvironment webHostEnvironment)
        {
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            {
                #region CONFIGURAÇÃO DA PÁGINA
                
                var page = doc.AddPage();

                page.Size = PdfSharpCore.PageSize.A4;
                page.Orientation = PdfSharpCore.PageOrientation.Portrait;

                var graphics = XGraphics.FromPdfPage(page);
                var corFonte = XBrushes.Black;

                #endregion

                #region NUMERAÇÃO DA PÁGINA
                int qtdPaginas = doc.PageCount;

                var numeracaoPagina = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                numeracaoPagina.DrawString(Convert.ToString(qtdPaginas), new XFont("Arial", 10), corFonte, new PdfSharpCore.Drawing.XRect(575, 825, page.Width, page.Height));
                #endregion

                #region LOGOMARCA
                var webRoot = webHostEnvironment.WebRootPath;
                var logoLoja = string.Concat(webRoot, "/imagem/", "loja-virtual-1.png");

                XImage imagem = XImage.FromFile(logoLoja);

                graphics.DrawImage(imagem, 20, 5, 150, 85);
                #endregion

                #region INFORMAÇÃO 1

                var relatorioCobrança = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var titulo = new PdfSharpCore.Drawing.XFont("Arial", 14, XFontStyle.Bold);

                relatorioCobrança.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                relatorioCobrança.DrawString("BOLETO ONLINE", titulo, corFonte, new XRect(0, 65, page.Width, page.Height));

                #endregion

                #region INFORMAÇÃO 2

                var deslocamentoY = 120;
                var detalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                var tituloInfo_1 = new XFont("Arial", 8, XFontStyle.Regular);

                detalhes.DrawString("Dados do Banco", tituloInfo_1, corFonte, new XRect(25, deslocamentoY, page.Width, page.Height));
                detalhes.DrawString("Banco Chequini S/A - 00567", tituloInfo_1, corFonte, new XRect(150, deslocamentoY, page.Width, page.Height));

                deslocamentoY += 9;

                detalhes.DrawString("Código Gerado", tituloInfo_1, corFonte, new XRect(25, deslocamentoY, page.Width, page.Height));
                detalhes.DrawString("00000 00000 000000 000000", tituloInfo_1, corFonte, new XRect(150, deslocamentoY, page.Width, page.Height));

                deslocamentoY += 9;

                detalhes.DrawString("quantidade", tituloInfo_1, corFonte, new XRect(25, deslocamentoY, page.Width, page.Height));
                detalhes.DrawString(compraUsuario.QuantidadeProdutos.ToString(), tituloInfo_1, corFonte, new XRect(150, deslocamentoY, page.Width, page.Height));

                deslocamentoY += 9;

                detalhes.DrawString("Valor Total", tituloInfo_1, corFonte, new XRect(25, deslocamentoY, page.Width, page.Height));
                detalhes.DrawString(compraUsuario.ValorTotal.ToString(), tituloInfo_1, corFonte, new XRect(150, deslocamentoY, page.Width, page.Height));

                var tituloInfo_2 = new XFont("Arial", 8, XFontStyle.Bold);

                try
                {
                    var textoFinal = string.Concat("Luiz Chequini - Concluiu as 09h:23min da Serie de Desenvolvimento de um sistema E-Commerce DDD em Asp.Net Core");

                    var img = await GeraQrCode(textoFinal);

                    Stream streamImage = new MemoryStream(img);

                    XImage qrCode = XImage.FromStream(() => streamImage);

                    deslocamentoY += 40;

                    graphics.DrawImage(qrCode, 140, deslocamentoY, 310, 310);
                }
                catch (Exception)
                {

                    throw;
                }

                deslocamentoY += 620;
                detalhes.DrawString("Canhoto com QrCode para pagamento on-line.", tituloInfo_1, corFonte, new XRect(20, deslocamentoY, page.Width, page.Height));
                
                #endregion

                using (MemoryStream stream = new MemoryStream())
                {
                    var contentType = "application/pdf";

                    doc.Save(stream, false);
                    return File(stream.ToArray(), contentType, "BoletoLojaOnLine.pdf");
                }


            }
        }
    }
}
