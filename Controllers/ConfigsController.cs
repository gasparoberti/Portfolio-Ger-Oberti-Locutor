using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioCore.Data;
using PortfolioCore.Models;

namespace PortfolioCore.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ConfigsController : Controller
    {
        private readonly MvcConfigContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ConfigsController(MvcConfigContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Noticias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Config.ToListAsync());
        }

        // GET: Configs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,archivoImagen1Home,archivoImagen2Home,archivoImage3Home,archivoImagenRelatos,archivoImagenCardRelatos,archivoImagenPodcasts,archivoImagenCardPodcasts,archivoImagenTips,archivoImagenSobreMi,fecha_alta")] Config config)
        public async Task<IActionResult> Create([Bind("id,archivoImagen1Home,imagen2Home,imagen3Home,imagenRelatos,imagenCardRelatos,imagenPodcasts,imagenCardPodcasts,imagenTips,imagenSobreMi,fecha_alta")] Config config)
        {
            if (config.archivoImagen1Home == null 
                //|| config.archivoImagenCardRelatos == null 
                //|| config.archivoImagenCardPodcasts == null
                )
            {
                ModelState.AddModelError("archivoImagen1Home", "Imagen es un campo requerido.");
                //ModelState.AddModelError("archivoImagenCardRelatos", "Imagen es un campo requerido.");
                //ModelState.AddModelError("archivoImagenCardPodcasts", "Imagen es un campo requerido.");
            }
            else
            {
                //guarda la imagen en wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = "archivoImagen1Home";
                string extension = Path.GetExtension(config.archivoImagen1Home.FileName);
                config.imagen1Home = fileName += extension;
                string path = Path.Combine(wwwRootPath + "/image/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await config.archivoImagen1Home.CopyToAsync(fileStream);
                }

                _context.Add(config);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(config);
        }

        // GET: Configs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = await _context.Config.FindAsync(id);
            if (config == null)
            {
                return NotFound();
            }
            return View(config);
        }

        // POST: Configs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,imagen1Home,archivoImagen1Home,imagen2Home,imagen3Home,imagenRelatos,imagenCardRelatos,imagenPodcasts,imagenCardPodcasts,imagenTips,imagenSobreMi,fecha_alta")] Config config)
        {
            if (id != config.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string path = null;

                    if (config.archivoImagen1Home != null)
                    {
                        string fileName = "archivoImagen1Home";
                        string extension = Path.GetExtension(config.archivoImagen1Home.FileName);
                        config.imagen1Home = fileName += extension;
                        path = Path.Combine(wwwRootPath + "/image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await config.archivoImagen1Home.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(config);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigExists(config.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(config);
        }

        private bool ConfigExists(int id)
        {
            return _context.Config.Any(e => e.id == id);
        }
    }
}
