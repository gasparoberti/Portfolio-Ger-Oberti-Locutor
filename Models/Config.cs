using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioCore.Models
{
    public class Config
    {
        public int id { get; set; }

        [Display(Name = "Imagen 1 Portada Home")]
        public string imagen1Home { get; set; }

        [NotMapped]
        [Display(Name = "Imagen 1 Portada Home *")]
        public IFormFile archivoImagen1Home { get; set; }
        
        
        [Display(Name = "Imagen 2 Portada Home")]
        public string imagen2Home { get; set; }

        [NotMapped]
        [Display(Name = "Imagen 2 Portada Home")]
        public IFormFile archivoImagen2Home { get; set; }
        
        
        [Display(Name = "Imagen 3 Portada Home")]
        public string imagen3Home { get; set; }

        [NotMapped]
        [Display(Name = "Imagen 3 Portada Home")]
        public IFormFile archivoImagen3Home { get; set; }
        
        
        [Display(Name = "Imagen Portada Relatos")]
        public string imagenRelatos { get; set; }

        [NotMapped]
        [Display(Name = "Imagen Portada Relatos")]
        public IFormFile archivoImagenRelatos { get; set; }
        
        
        [Display(Name = "Imagen Card Relatos")]
        public string imagenCardRelatos { get; set; }

        [NotMapped]
        [Display(Name = "Imagen Card Relatos *")]
        public IFormFile archivoImagenCardRelatos { get; set; }
        
        
        [Display(Name = "Imagen Portada Podcasts")]
        public string imagenPodcasts { get; set; }

        [NotMapped]
        [Display(Name = "Imagen Portada Podcasts")]
        public IFormFile archivoImagenPodcasts { get; set; }


        [Display(Name = "Imagen Card Podcasts")]
        public string imagenCardPodcasts { get; set; }

        [NotMapped]
        [Display(Name = "Imagen Card Podcasts *")]
        public IFormFile archivoImagenCardPodcasts { get; set; }


        [Display(Name = "Imagen Portada Tips")]
        public string imagenTips { get; set; }

        [NotMapped]
        [Display(Name = "Imagen Portada Tips")]
        public IFormFile archivoImagenTips { get; set; }


        [Display(Name = "Imagen Portada Sorbre Mí")]
        public string imagenSobreMi { get; set; }

        [NotMapped]
        [Display(Name = "Imagen Portada Sorbre Mí")]
        public IFormFile archivoImagenSobreMi { get; set; }


        [Required(ErrorMessage = "{0} es un campo requerido.")]
        [Display(Name = "Fecha Alta *")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_alta { get; set; }

        
    }
}
