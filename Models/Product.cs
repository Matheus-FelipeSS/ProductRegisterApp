using System.ComponentModel.DataAnnotations;
using ProductControl.Validators;

namespace ProductControl.Models;

public class Product
{
    [Key]
    public int IdProduct { get; set; }

 private string _produto = string.Empty;

[Display(Name = "Nome do Produto")]
[Required(ErrorMessage = "O campo {0} é obrigatório.")]
[StringLength(100, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.")]
public string Produto
{
    get => _produto;
    set => _produto = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value?.ToLower() ?? string.Empty);
}

    [Display(Name = "Quantidade")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "A {0} deve ser maior que zero.")]
    public int Quantidade { get; set; }

    [Display(Name = "Data de Entrega")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [FutureOrPresent(ErrorMessage = "A data de entrega deve ser no presente ou no futuro.")]
    public DateOnly DataEntrega { get; set; }

    [Display(Name = "Validade")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [FutureOrPresent(ErrorMessage = "A data de validade deve ser posterior a data de entrega.")]
    public DateOnly Validade { get; set; }

    [Display(Name = "Criado em")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Finalizado em")]
    public DateOnly? FinalizadoEm { get; set; }
}