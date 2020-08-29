using Entities.Notifications;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("Produto")]
    public class Produto : Notifies
    {
        [Column("PRD_ID")]
        [Display(Name ="Codigo")]
        public int Id { get; set; }

        [Column("PRD_NOME")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("PRD_VALOR")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Column("PRD_ESTADO")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
    }
}
