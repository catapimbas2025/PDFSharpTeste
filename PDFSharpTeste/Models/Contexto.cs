using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDFSharpTeste.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        

        //VALIDA O TIPO DE DADO DO CAMPO ESTADO CIVIL
        //[Column(TypeName = "nvarchar(20)")]
        //public EstadoCivil EstadoCivil { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            

        }

    }
}
