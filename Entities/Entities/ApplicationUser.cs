using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USR_CPF")]
        [Display(Name ="CPF")]
        [MaxLength(50)]
        public string Cpf { get; set; }

        [Column("USR_IDADE")]
        [Display(Name ="Idade")]
        public int Idade { get; set; }

        [Column("USR_NOME")]
        [Display(Name = "Nome")]
        [MaxLength(255)]
        public string Nome { get; set; }

        [Column("USR_CEP")]
        [Display(Name = "CEP")]
        [MaxLength(15)]
        public string Cep { get; set; }

        [Column("USR_ENDERECO")]
        [Display(Name = "Endereço")]
        [MaxLength(255)]
        public string Endereco { get; set; }

        [Column("USR_COMPLEMENTO_ENDERECO")]
        [Display(Name = "Complemento de Endereço")]
        [MaxLength(450)]
        public string ComplementoEndereco { get; set; }

        [Column("USR_CELULAR")]
        [Display(Name = "Celular")]
        [MaxLength(20)]
        public string Celular { get; set; }

        [Column("USR_TELEFONE")]
        [Display(Name = "Telefone")]
        [MaxLength(20)]
        public string Telefone { get; set; }

        [Column("USR_ESTADO")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [Column("USR_TIPO")]
        [Display(Name = "Tipo")]
        public TipoUsuario Tipo { get; set; }
    }
}
