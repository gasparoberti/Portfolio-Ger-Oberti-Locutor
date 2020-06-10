using System.ComponentModel.DataAnnotations;

namespace PortfolioCore.Models
{
    public class Relato
    {
        public int id { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [StringLength(1000, ErrorMessage = "{0} debe tener una longitud de {1} caracteres como mínimo y {2} caracteres como máximo.", MinimumLength = 5)]
        [Display(Name = "URL")]
        public string url { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [Display(Name = "Visible")]
        public bool visible { get; set; }
    }
}
