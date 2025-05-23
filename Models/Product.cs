using System.ComponentModel.DataAnnotations;
using ProductControl.Validators;
using System.ComponentModel.DataAnnotations.Schema;

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

    [Display(Name = "Data de Fabricação")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [FutureOrPresent(ErrorMessage = "A data de fabricação deve ser hoje ou em uma data futura.")]
    public DateOnly? DataFabricacao { get; set; }

    [Display(Name = "Validade")]
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [FutureOrPresent(ErrorMessage = "A data de validade deve ser posterior à data de fabricação.")]
    public DateOnly? Validade { get; set; }


    [Display(Name = "Criado em")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Finalizado em")]
    public DateOnly? FinalizadoEm { get; set; }

    [Display(Name = "Loja")]
    public int IdLoja { get; set; }

    [ForeignKey(nameof(IdLoja))]
    public Loja? Loja { get; set; }
}