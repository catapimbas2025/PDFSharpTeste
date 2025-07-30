using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PDFSharpTeste.Migrations;
using PDFSharpTeste.Models;
using ClienteModel = PDFSharpTeste.Models.Cliente;



namespace PDFSharpTeste.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Contexto _context;
        
        public ClienteModel Clientes;

        public ClientesController(Contexto context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(string? nome)
        {
            if (nome == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Nome == nome);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,DataCadastro,Nome,CPF,RG,Email,EstadoCivil,DataDeNascimento,Endereco,Numero,Bairro,Cidade,Estado,CEP,Telefone,Telefone2,Referencias,Observacoes")] Models.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string nome, [Bind("Id,DataCadastro,Nome,CPF,RG,Email,EstadoCivil,DataDeNascimento,Endereco,Numero,Bairro,Cidade,Estado,CEP,Telefone,Telefone2,Referencias,Observacoes")] Models.Cliente cliente)
        {
            if (nome != cliente.Nome)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Nome))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cliente = await _context.Clientes
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (cliente == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cliente);
        //}

        public async Task<IActionResult> Delete(String nome)
        {
            if (nome == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(m => m.Nome == nome);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool ClienteExists(int id)
        //{
        //    return _context.Clientes.Any(e => e.Id == id);
        //}

        private bool ClienteExists(String nome)
        {
            return _context.Clientes.Any(e => e.Nome == nome);
        }

        public FileResult GerarRelatorioCliente()


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
                var logo = @"C:\Repositorio\PDFSharpTeste\PDFSharpTeste\wwwroot\imagens\senaccharp.png";
                XImage imagem = XImage.FromFile(logo);
                graphics.DrawImage(imagem, 20, 5, 300, 50);


                var tituloDetalhes = new PdfSharpCore.Drawing.Layout.XTextFormatter(graphics);
                tituloDetalhes.Alignment = PdfSharpCore.Drawing.Layout.XParagraphAlignment.Center;
                tituloDetalhes.DrawString ("Relatório dos Clientes", titulodetalhes, corFonte, new PdfSharpCore.Drawing.XRect(0, 120, page.Width, page.Height));


                // ID TEM QUE SER VERIFICADO
                textFormatter.DrawString("Id:", fonteDescricao, corFonte, new XRect(20, 75, page.Width, page.Height));
                textFormatter.DrawString(Clientes.Id.ToString(), fonteOrganizacao, corFonte, new XRect(80, 75, page.Width, page.Height));

                textFormatter.DrawString("DataCadastro:", fonteDescricao, corFonte, new XRect(20, 95, page.Width, page.Height));
                textFormatter.DrawString(Clientes.DataCadastro.ToString("dd/MM/yyyy"), fonteOrganizacao, corFonte, new XRect(80, 95, page.Width, page.Height));

                textFormatter.DrawString("Nome:", fonteDescricao, corFonte, new XRect(20, 115, page.Width, page.Height));
                textFormatter.DrawString(Clientes.Nome, fonteOrganizacao, corFonte, new XRect(80, 115, page.Width, page.Height));

                textFormatter.DrawString("CPF:", fonteDescricao, corFonte, new XRect(20, 135, page.Width, page.Height));
                textFormatter.DrawString(Clientes.CPF, fonteOrganizacao, corFonte, new XRect(80, 135, page.Width, page.Height));

                textFormatter.DrawString("RG:", fonteDescricao, corFonte, new XRect(20, 155, page.Width, page.Height));
                textFormatter.DrawString(Clientes.RG, fonteOrganizacao, corFonte, new XRect(80, 155, page.Width, page.Height));

                textFormatter.DrawString("Email:", fonteDescricao, corFonte, new XRect(20, 175, page.Width, page.Height));
                textFormatter.DrawString(Clientes.Email, fonteOrganizacao, corFonte, new XRect(80, 175, page.Width, page.Height));

                textFormatter.DrawString("EstadoCivil:", fonteDescricao, corFonte, new XRect(20, 195, page.Width, page.Height));
                textFormatter.DrawString(Clientes.EstadoCivil, fonteOrganizacao, corFonte, new XRect(80, 195, page.Width, page.Height));

                textFormatter.DrawString("DataDeNascimento:", fonteDescricao, corFonte, new XRect(20, 215, page.Width, page.Height));
                textFormatter.DrawString(Clientes.DataDeNascimento.ToString("dd/MM/yyyy"), fonteOrganizacao, corFonte, new XRect(80, 215, page.Width, page.Height));

                textFormatter.DrawString("Endereço:", fonteDescricao, corFonte, new XRect(20, 235, page.Width, page.Height));
                textFormatter.DrawString($"{Clientes.Endereco}, {Clientes.Numero} - {Clientes.Bairro}", fonteOrganizacao, corFonte, new XRect(80, 235, page.Width, page.Height));
                textFormatter.DrawString("Cidade/Estado:", fonteDescricao, corFonte, new XRect(20, 255, page.Width, page.Height));
                textFormatter.DrawString($"{Clientes.Cidade}/{Clientes.Estado}", fonteOrganizacao, corFonte, new XRect(80, 255, page.Width, page.Height));

                textFormatter.DrawString("CEP:", fonteDescricao, corFonte, new XRect(20, 275, page.Width, page.Height));
                textFormatter.DrawString(Clientes.CEP, fonteOrganizacao, corFonte, new XRect(80, 275, page.Width, page.Height));

                textFormatter.DrawString("Telefone:", fonteDescricao, corFonte, new XRect(20, 295, page.Width, page.Height));
                textFormatter.DrawString(Clientes.Telefone, fonteOrganizacao, corFonte, new XRect(80, 295, page.Width, page.Height));

                textFormatter.DrawString("Telefone2:", fonteDescricao, corFonte, new XRect(20, 315, page.Width, page.Height));
                textFormatter.DrawString(Clientes.Telefone2, fonteOrganizacao, corFonte, new XRect(80, 315, page.Width, page.Height));

                textFormatter.DrawString("Referencias:", fonteDescricao, corFonte, new XRect(20, 335, page.Width, page.Height));
                textFormatter.DrawString(Clientes.Referencias, fonteOrganizacao, corFonte, new XRect(80, 335, page.Width, page.Height));

                textFormatter.DrawString("Observacoes:", fonteDescricao, corFonte, new XRect(20, 355, page.Width, page.Height));
                textFormatter.DrawString(Clientes.Observacoes, fonteOrganizacao, corFonte, new XRect(80, 355, page.Width, page.Height));
              

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
