using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PDFSharpTeste.Models;

namespace PDFSharpTeste.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public FileResult GerarRelatorio()
        {
            using (var doc = new PdfSharpCore.Pdf.PdfDocument())
            { 
                var page = doc.AddPage(); //ADICIONANDO A PÁGINA 
                page.Size = PdfSharpCore.PageSize.A4; // ADICIONANDO O TAMANHO DA PÁGINA 
                page.Orientation = PdfSharpCore.PageOrientation.Portrait; //ORIENTAÇÃO DA PÁGINA

                var graphics = PdfSharpCore.Drawing.XGraphics.FromPdfPage(page);
                var corFonte = PdfSharpCore.Drawing.XBrushes.Black; // DEFININDO A FONTE DO DOCUMENTO
                
                
                var textFormatter = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);// USADO PARA IMPRIMIR (adicionar) "COISAS" NO DOCUMENTO


                //DEFININDO AS FONTES DO DOCUMENTO
                var fonteOrganizacao = new PdfSharpCore.Drawing.XFont("Arial", 10);
                var fonteDescricao = new PdfSharpCore.Drawing.XFont("Arial", 8, PdfSharpCore.Drawing.XFontStyle.BoldItalic);
                var titulodetalhes = new PdfSharpCore.Drawing.XFont("Arial", 14, PdfSharpCore.Drawing.XFontStyle.Bold);
                var fonteDetalhesDescricao = new PdfSharpCore.Drawing.XFont("Arial", 7);

                //ADICIONANDO A LOGO NO DOCUMENTO
                var logo = @"C:\Users\schua\source\repos\PDFSharpTeste\PDFSharpTeste\wwwroot\imagens\senaccharp.png"; 
                XImage imagem = XImage.FromFile(logo);
                graphics.DrawImage(imagem, 20, 5, 300, 70);

                
                //ADICIONANDO INFORMAÇÕES AO BANCO DE DADOS
                textFormatter.DrawString("Nome :", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, 75, page.Width, page.Height));
                textFormatter.DrawString("Pedro Schuavab", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(80, 75, page.Width, page.Height));

                textFormatter.DrawString(" Profissão :", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, 95, page.Width, page.Height));
                textFormatter.DrawString("Programador", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(80, 95, page.Width, page.Height));

                textFormatter.DrawString("Tempo :", fonteDescricao, corFonte, new PdfSharpCore.Drawing.XRect(20, 115, page.Width, page.Height));
                textFormatter.DrawString("10", fonteOrganizacao, corFonte, new PdfSharpCore.Drawing.XRect(80, 115, page.Width, page.Height));


                var qtdPaginas = doc.PageCount; //CONTADOR DE PÁGINAS DO DOCUMENTO
                textFormatter.DrawString(qtdPaginas.ToString(), new PdfSharpCore.Drawing.XFont("Arial", 10), corFonte, new PdfSharpCore.Drawing.XRect(535, 825, page.Width, page.Height));

                
                // ADICIONADO NOME AO DOCUMENTO
                using (MemoryStream stream = new MemoryStream())
                {
                    var contantType = "application/pdf";
                    doc.Save(stream, false);
                    var nomearquivo = "RelatorioCadastroSenac.pdf";

                    return File(stream.ToArray(), contantType, nomearquivo);
                }


                
            }
        }
    }
}
