﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioCore.Models
{
    public class Tip
    {
        public int id { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [StringLength(500, ErrorMessage = "{0} debe tener una longitud de {1} caracteres como mínimo y {2} caracteres como máximo.", MinimumLength = 5)]
        [Display(Name = "Titulo *")]
        public string titulo { get; set; }

        [StringLength(2500, ErrorMessage = "{0} debe tener una longitud de {1} caracteres como mínimo y {2} caracteres como máximo.", MinimumLength = 5)]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [StringLength(4500, ErrorMessage = "{0} debe tener una longitud de {1} caracteres como mínimo y {2} caracteres como máximo.", MinimumLength = 5)]
        [Display(Name = "Contenido *")]
        public string contenido { get; set; }

        [Display(Name = "Imagen *")]
        public string imagen { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [Display(Name = "Tip Visible")]
        public bool visible { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [Display(Name = "Prioridad (máxima: 1)")]
        public int prioridad { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [Display(Name = "Fecha Alta *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_alta { get; set; }

        [NotMapped]
        [Display(Name = "Imagen *")]
        public IFormFile archivoImagen { get; set; }

        [StringLength(1000, ErrorMessage = "{0} debe tener una longitud de {1} caracteres como mínimo y {2} caracteres como máximo.", MinimumLength = 5)]
        [Display(Name = "URL Video")]
        public string video { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [Display(Name = "Visible")]
        public bool visibleV { get; set; }

        [StringLength(3000, ErrorMessage = "{0} debe tener una longitud de {1} caracteres como mínimo y {2} caracteres como máximo.", MinimumLength = 5)]
        [Display(Name = "Cuerpo 2")]
        public string contenido2 { get; set; }

        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [Display(Name = "Visible")]
        public bool visibleC2 { get; set; }
    }
}
