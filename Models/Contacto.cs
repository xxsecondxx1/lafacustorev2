using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lafacustorev2.Models
{
    [Table("t_contacto")]
    public class Contacto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {   get; set;}
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? Category { get; set; }
        public string? Sentimiento { get; set; }

    }
}