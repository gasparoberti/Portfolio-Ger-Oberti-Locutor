using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioCore.Models
{
    public class SobreMi
    {
        public int id { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [StringLength(500, ErrorMessage = "{0} debe tener una longitud de {1} caracteres como mínimo y {2} caracteres como máximo.", MinimumLength = 5)]
        [Display(Name = "Titulo *")]
        public string titulo { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [StringLength(2500, ErrorMessage = "{0} debe tener una longitud de {1} caracteres como mínimo y {2} caracteres como máximo.", MinimumLength = 5)]
        [Display(Name = "Descripción *")]
        public string descripcion { get; set; }

        [StringLength(4500, ErrorMessage = "{0} debe tener una longitud de {1} caracteres como mínimo y {2} caracteres como máximo.", MinimumLength = 5)]
        [Display(Name = "Contenido")]
        public string contenido { get; set; }

        [Display(Name = "Imagen *")]
        public string imagen { get; set; }

        [Display(Name = "Visible")]
        public bool visible { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [Display(Name = "Prioridad")]
        public int prioridad { get; set; }

        [NotMapped]
        [Display(Name = "Imagen *")]
        public IFormFile archivoImagen { get; set; }
    }
}
