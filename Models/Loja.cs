using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductControl.Models
{
    public class Loja
    {
        [Key]
        public int IdLoja { get; set; }

        [Required(ErrorMessage = "O nome da loja é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; }

        public List<Product> Produtos { get; set; } = new();
    }
}