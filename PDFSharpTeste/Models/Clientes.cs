using System.ComponentModel.DataAnnotations;

namespace PDFSharpTeste.Models
{

    public class Cliente
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        [Required]
        [StringLength(15)]
        public string RG { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }


       
        
        public string? EstadoCivil { get; set; }

        [Required]
        public DateTime DataDeNascimento { get; set; }

        [Required]
        [StringLength(100)]
        public string Endereco { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        [StringLength(100)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(50)]
        public string Estado { get; set; }

        [Required]
        [StringLength(50)]
        public string CEP { get; set; }

        [Required]
        [StringLength(11)]
        public string Telefone { get; set; }

        public string? Telefone2 { get; set; }
        public string? Referencias { get; set; }
        public string? Observacoes { get; set; }

    }
}
