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
        public async Task<IActionResult> Create([Bind("id," +
            "archivoImagen1Home," +
            "archivoImagen2Home,visibleH2," +
            "archivoImagen3Home,visibleH3," +
            "archivoImagenRelatos,visibleR," +
            "archivoImagenCardRelatos," +
            "archivoimagenPortfolio,visiblePorf," +
            "videoSobreMi," +
            "visibleV," +
            "archivoImagenPodcasts,visibleP," +
            "archivoImagenCardPodcasts," +
            "imagenTips,archivoImagenTips,visibleT," +
            "imagenSobreMi," +
            "fecha_alta"
            )] Config config)
        {
            if (config.archivoImagen1Home == null
                //|| config.archivoImagenCardRelatos == null
                || config.archivoImagenCardPodcasts == null
                )
            {
                if (config.archivoImagen1Home == null)
                {
                    ModelState.AddModelError("archivoImagen1Home", "Imagen 1 es un campo requerido.");
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

                string fileName = "archivoImagen1H";
                string extension = Path.GetExtension(config.archivoImagen1Home.FileName);
                config.imagen1Home = fileName += extension;
                string path = Path.Combine(wwwRootPath + "/image/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await config.archivoImagen1Home.CopyToAsync(fileStream);
                }

                if (config.archivoImagen2Home != null && config.visibleH2 == true)
                {
                    string fileName2 = "archivoImagen2H";
                    string extension2 = Path.GetExtension(config.archivoImagen2Home.FileName);
                    config.imagen2Home = fileName2 += extension2;
                    string path2 = Path.Combine(wwwRootPath + "/image/", fileName2);

                    using (var fileStream = new FileStream(path2, FileMode.Create))
                    {
                        await config.archivoImagen2Home.CopyToAsync(fileStream);
                    }
                }

                if (config.archivoImagen3Home != null && config.visibleH3 == true)
                {
                    string fileName3 = "archivoImagen3H";
                    string extension3 = Path.GetExtension(config.archivoImagen3Home.FileName);
                    config.imagen3Home = fileName3 += extension3;
                    string path3 = Path.Combine(wwwRootPath + "/image/", fileName3);

                    using (var fileStream = new FileStream(path3, FileMode.Create))
                    {
                        await config.archivoImagen3Home.CopyToAsync(fileStream);
                    }
                }


                if (config.archivoImagenCardPodcasts != null)
                {
                    string fileNameCP = "archivoImagenCardPod";
                    string extensionCP = Path.GetExtension(config.archivoImagenCardPodcasts.FileName);
                    config.imagenCardPodcasts = fileNameCP += extensionCP;
                    string pathCP = Path.Combine(wwwRootPath + "/image/", fileNameCP);

                    using (var fileStream = new FileStream(pathCP, FileMode.Create))
                    {
                        await config.archivoImagenCardPodcasts.CopyToAsync(fileStream);
                    }
                }

                
                if (config.archivoimagenPortfolio != null && config.visiblePorf == true)
                {
                    string fileNameR = "archivoimagenPort";
                    string extensionR = Path.GetExtension(config.archivoimagenPortfolio.FileName);
                    config.imagenPortfolio = fileNameR += extensionR;
                    string pathR = Path.Combine(wwwRootPath + "/image/", fileNameR);

                    using (var fileStream = new FileStream(pathR, FileMode.Create))
                    {
                        await config.archivoimagenPortfolio.CopyToAsync(fileStream);
                    }
                }

                if (config.archivoImagenPodcasts != null && config.visibleP == true)
                {
                    string fileNameP = "archivoImagenPod";
                    string extensionP = Path.GetExtension(config.archivoImagenPodcasts.FileName);
                    config.imagenPodcasts = fileNameP += extensionP;
                    string pathP = Path.Combine(wwwRootPath + "/image/", fileNameP);

                    using (var fileStream = new FileStream(pathP, FileMode.Create))
                    {
                        await config.archivoImagenPodcasts.CopyToAsync(fileStream);
                    }
                }

                if (config.archivoImagenTips != null && config.visibleT == true)
                {
                    string fileNameT = "archivoImagenTip";
                    string extensionT = Path.GetExtension(config.archivoImagenTips.FileName);
                    config.imagenTips = fileNameT += extensionT;
                    string pathT = Path.Combine(wwwRootPath + "/image/", fileNameT);

                    using (var fileStream = new FileStream(pathT, FileMode.Create))
                    {
                        await config.archivoImagenTips.CopyToAsync(fileStream);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,imagen1Home,archivoImagen1Home," +
            "imagen2Home,archivoImagen2Home,visibleH2," +
            "imagen3Home,archivoImagen3Home,visibleH3," +
            "imagenRelatos,archivoImagenRelatos,visibleR," +
            "imagenPortfolio,archivoimagenPortfolio,visiblePorf," +
            "imagenCardRelatos,archivoImagenCardRelatos," +
            "videoSobreMi," +
            "visibleV," +
            "imagenPodcasts,archivoImagenPodcasts,visibleP," +
            "imagenCardPodcasts,archivoImagenCardPodcasts," +
            "imagenTips,archivoImagenTips,visibleT," +
            "imagenSobreMi," +
            "fecha_alta"
            )] Config config)
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
                    string path, path2, path3, pathCP, pathR, pathP, pathT = null;

                    if (config.archivoImagen1Home != null)
                    {
                        string fileName = "archivoImagen1H";
                        string extension = Path.GetExtension(config.archivoImagen1Home.FileName);
                        config.imagen1Home = fileName += extension;
                        path = Path.Combine(wwwRootPath + "/image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await config.archivoImagen1Home.CopyToAsync(fileStream);
                        }
                    }

                    if (config.archivoImagen2Home != null && config.visibleH2 == true)
                    {
                        string fileName2 = "archivoImagen2H";
                        string extension2 = Path.GetExtension(config.archivoImagen2Home.FileName);
                        config.imagen2Home = fileName2 += extension2;
                        path2 = Path.Combine(wwwRootPath + "/image/", fileName2);

                        using (var fileStream = new FileStream(path2, FileMode.Create))
                        {
                            await config.archivoImagen2Home.CopyToAsync(fileStream);
                        }
                    }
                    else if (config.imagen2Home != null && config.visibleH2 == false)
                    {
                        //borra imagen de wwwroot/image
                        path2 = Path.Combine(_hostEnvironment.WebRootPath, "image", config.imagen2Home);
                        if (System.IO.File.Exists(path2))
                        {
                            System.IO.File.Delete(path2);
                        }
                    }


                    if (config.archivoImagen3Home != null && config.visibleH3 == true)
                    {
                        string fileName3 = "archivoImagen3H";
                        string extension3 = Path.GetExtension(config.archivoImagen3Home.FileName);
                        config.imagen3Home = fileName3 += extension3;
                        path3 = Path.Combine(wwwRootPath + "/image/", fileName3);

                        using (var fileStream = new FileStream(path3, FileMode.Create))
                        {
                            await config.archivoImagen3Home.CopyToAsync(fileStream);
                        }
                    }
                    else if (config.imagen3Home != null && config.visibleH3 == false)
                    {
                        //borra imagen de wwwroot/image
                        path3 = Path.Combine(_hostEnvironment.WebRootPath, "image", config.imagen3Home);
                        if (System.IO.File.Exists(path3))
                        {
                            System.IO.File.Delete(path3);
                        }
                    }


                    if (config.archivoImagenCardPodcasts != null)
                    {
                        string fileNameCP = "archivoImagenCardPod";
                        string extensionCP = Path.GetExtension(config.archivoImagenCardPodcasts.FileName);
                        config.imagenCardPodcasts = fileNameCP += extensionCP;
                        pathCP = Path.Combine(wwwRootPath + "/image/", fileNameCP);

                        using (var fileStream = new FileStream(pathCP, FileMode.Create))
                        {
                            await config.archivoImagenCardPodcasts.CopyToAsync(fileStream);
                        }
                    }

                    
                    if (config.archivoimagenPortfolio != null && config.visiblePorf == true)
                    {
                        string fileNameR = "archivoimagenPort";
                        string extensionR = Path.GetExtension(config.archivoimagenPortfolio.FileName);
                        config.imagenPortfolio = fileNameR += extensionR;
                        pathR = Path.Combine(wwwRootPath + "/image/", fileNameR);

                        using (var fileStream = new FileStream(pathR, FileMode.Create))
                        {
                            await config.archivoimagenPortfolio.CopyToAsync(fileStream);
                        }
                    }
                    else if (config.imagenPortfolio != null && config.visiblePorf == false)
                    {
                        //borra imagen de wwwroot/image
                        pathR = Path.Combine(_hostEnvironment.WebRootPath, "image", config.imagenPortfolio);
                        if (System.IO.File.Exists(pathR))
                        {
                            System.IO.File.Delete(pathR);
                        }
                    }


                    if (config.archivoImagenPodcasts != null && config.visibleP == true)
                    {
                        string fileNameP = "archivoImagenPod";
                        string extensionP = Path.GetExtension(config.archivoImagenPodcasts.FileName);
                        config.imagenPodcasts = fileNameP += extensionP;
                        pathP = Path.Combine(wwwRootPath + "/image/", fileNameP);

                        using (var fileStream = new FileStream(pathP, FileMode.Create))
                        {
                            await config.archivoImagenPodcasts.CopyToAsync(fileStream);
                        }
                    }
                    else if (config.imagenPodcasts != null && config.visibleP == false)
                    {
                        //borra imagen de wwwroot/image
                        pathP = Path.Combine(_hostEnvironment.WebRootPath, "image", config.imagenPodcasts);
                        if (System.IO.File.Exists(pathP))
                        {
                            System.IO.File.Delete(pathP);
                        }
                    }


                    if (config.archivoImagenTips != null && config.visibleT == true)
                    {
                        string fileNameT = "archivoImagenTip";
                        string extensionT = Path.GetExtension(config.archivoImagenTips.FileName);
                        config.imagenTips = fileNameT += extensionT;
                        pathT = Path.Combine(wwwRootPath + "/image/", fileNameT);

                        using (var fileStream = new FileStream(pathT, FileMode.Create))
                        {
                            await config.archivoImagenTips.CopyToAsync(fileStream);
                        }
                    }
                    else if (config.imagenTips != null && config.visibleT == false)
                    {
                        //borra imagen de wwwroot/image
                        pathT = Path.Combine(_hostEnvironment.WebRootPath, "image", config.imagenTips);
                        if (System.IO.File.Exists(pathT))
                        {
                            System.IO.File.Delete(pathT);
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

