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
        public async Task<IActionResult> Create([Bind("id,archivoImagen1Home,archivoImagen2Home,archivoImagen3Home,archivoImagenRelatos,visibleR,archivoImagenCardRelatos,imagenPodcasts,archivoImagenCardPodcasts,imagenTips,imagenSobreMi,fecha_alta")] Config config)
        {
            if (config.archivoImagen1Home == null 
                || config.archivoImagenCardRelatos == null 
                || config.archivoImagenCardPodcasts == null
                )
            {
                if (config.archivoImagen1Home == null)
                {
                    ModelState.AddModelError("archivoImagen1Home", "Imagen 1 es un campo requerido.");
                }
                
                if (config.archivoImagenCardRelatos == null)
                {
                    ModelState.AddModelError("archivoImagenCardRelatos", "Imagen Card Relatos es un campo requerido.");
                }
                
                if (config.archivoImagenCardPodcasts == null)
                {
                    ModelState.AddModelError("archivoImagenCardPodcasts", "Imagen Card Podcasts es un campo requerido.");
                }

                if ((config.archivoImagen1Home == null && config.archivoImagen2Home != null)
                || (config.archivoImagen1Home == null && config.archivoImagen3Home != null)
                || (config.archivoImagen1Home == null && config.archivoImagen2Home != null && config.archivoImagen3Home != null)
                ) 
                {
                    ModelState.AddModelError("archivoImagen1Home", "Campo requerido.");
                    ModelState.AddModelError("archivoImagen2Home", "No se puede cargar esta imagen sin cargar la primera.");
                    ModelState.AddModelError("archivoImagen3Home", "No se puede cargar esta imagen sin cargar la primera.");
                }
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

                if (config.archivoImagen2Home != null)
                {
                    string fileName2 = "archivoImagen2Home";
                    string extension2 = Path.GetExtension(config.archivoImagen2Home.FileName);
                    config.imagen2Home = fileName2 += extension2;
                    string path2 = Path.Combine(wwwRootPath + "/image/", fileName2);

                    using (var fileStream = new FileStream(path2, FileMode.Create))
                    {
                        await config.archivoImagen2Home.CopyToAsync(fileStream);
                    }
                }
                
                if (config.archivoImagen3Home != null)
                {
                    string fileName3 = "archivoImagen3Home";
                    string extension3 = Path.GetExtension(config.archivoImagen3Home.FileName);
                    config.imagen3Home = fileName3 += extension3;
                    string path3 = Path.Combine(wwwRootPath + "/image/", fileName3);

                    using (var fileStream = new FileStream(path3, FileMode.Create))
                    {
                        await config.archivoImagen3Home.CopyToAsync(fileStream);
                    }
                }
                
                if (config.archivoImagenCardRelatos != null)
                {
                    string fileNameCR = "archivoImagenCardRelatos";
                    string extensionCR = Path.GetExtension(config.archivoImagenCardRelatos.FileName);
                    config.imagenCardRelatos = fileNameCR += extensionCR;
                    string pathCR = Path.Combine(wwwRootPath + "/image/", fileNameCR);

                    using (var fileStream = new FileStream(pathCR, FileMode.Create))
                    {
                        await config.archivoImagenCardRelatos.CopyToAsync(fileStream);
                    }
                }
                
                if (config.archivoImagenCardPodcasts != null)
                {
                    string fileNameCP = "archivoImagenCardPodcasts";
                    string extensionCP = Path.GetExtension(config.archivoImagenCardPodcasts.FileName);
                    config.imagenCardPodcasts = fileNameCP += extensionCP;
                    string pathCP = Path.Combine(wwwRootPath + "/image/", fileNameCP);

                    using (var fileStream = new FileStream(pathCP, FileMode.Create))
                    {
                        await config.archivoImagenCardPodcasts.CopyToAsync(fileStream);
                    }
                }
                
                if (config.archivoImagenRelatos != null)
                {
                    string fileNameR = "archivoImagenRelatos";
                    string extensionR = Path.GetExtension(config.archivoImagenRelatos.FileName);
                    config.imagenRelatos = fileNameR += extensionR;
                    string pathR = Path.Combine(wwwRootPath + "/image/", fileNameR);

                    using (var fileStream = new FileStream(pathR, FileMode.Create))
                    {
                        await config.archivoImagenRelatos.CopyToAsync(fileStream);
                    }
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
        public async Task<IActionResult> Edit(int id, [Bind("id,imagen1Home,archivoImagen1Home,imagen2Home,archivoImagen2Home,imagen3Home,archivoImagen3Home,imagenRelatos,archivoImagenRelatos,visibleR,imagenCardRelatos,archivoImagenCardRelatos,imagenPodcasts,archivoImagenCardPodcasts,imagenCardPodcasts,imagenTips,imagenSobreMi,fecha_alta")] Config config)
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
                    string path, path2, path3, pathCR, pathCP, pathR = null;

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

                    if (config.archivoImagen2Home != null)
                    {
                        string fileName2 = "archivoImagen2Home";
                        string extension2 = Path.GetExtension(config.archivoImagen2Home.FileName);
                        config.imagen2Home = fileName2 += extension2;
                        path2 = Path.Combine(wwwRootPath + "/image/", fileName2);

                        using (var fileStream = new FileStream(path2, FileMode.Create))
                        {
                            await config.archivoImagen2Home.CopyToAsync(fileStream);
                        }
                    }
                    
                    if (config.archivoImagen3Home != null)
                    {
                        string fileName3 = "archivoImagen3Home";
                        string extension3 = Path.GetExtension(config.archivoImagen3Home.FileName);
                        config.imagen3Home = fileName3 += extension3;
                        path3 = Path.Combine(wwwRootPath + "/image/", fileName3);

                        using (var fileStream = new FileStream(path3, FileMode.Create))
                        {
                            await config.archivoImagen3Home.CopyToAsync(fileStream);
                        }
                    }
                    
                    if (config.archivoImagenCardRelatos != null)
                    {
                        string fileNameCR = "archivoImagenCardRelatos";
                        string extensionCR = Path.GetExtension(config.archivoImagenCardRelatos.FileName);
                        config.imagenCardRelatos = fileNameCR += extensionCR;
                        pathCR = Path.Combine(wwwRootPath + "/image/", fileNameCR);

                        using (var fileStream = new FileStream(pathCR, FileMode.Create))
                        {
                            await config.archivoImagenCardRelatos.CopyToAsync(fileStream);
                        }
                    }
                    
                    if (config.archivoImagenCardPodcasts != null)
                    {
                        string fileNameCP = "archivoImagenCardPodcasts";
                        string extensionCP = Path.GetExtension(config.archivoImagenCardPodcasts.FileName);
                        config.imagenCardPodcasts = fileNameCP += extensionCP;
                        pathCP = Path.Combine(wwwRootPath + "/image/", fileNameCP);

                        using (var fileStream = new FileStream(pathCP, FileMode.Create))
                        {
                            await config.archivoImagenCardPodcasts.CopyToAsync(fileStream);
                        }
                    }
                    
                    if (config.archivoImagenRelatos != null)
                    {
                        string fileNameR = "archivoImagenRelatos";
                        string extensionR = Path.GetExtension(config.archivoImagenRelatos.FileName);
                        config.imagenRelatos = fileNameR += extensionR;
                        pathR = Path.Combine(wwwRootPath + "/image/", fileNameR);

                        using (var fileStream = new FileStream(pathR, FileMode.Create))
                        {
                            await config.archivoImagenRelatos.CopyToAsync(fileStream);
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
