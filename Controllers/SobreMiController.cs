using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcNoticia.Data;
using PortfolioCore.Data;
using PortfolioCore.Models;

namespace PortfolioCore.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class SobreMiController : Controller
    {
        private readonly MvcSobreMiContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SobreMiController(MvcSobreMiContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: SobreMi
        public async Task<IActionResult> Index()
        {
            return View(await _context.SobreMi.ToListAsync());
        }

        // GET: SobreMi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sobreMi = await _context.SobreMi
                .FirstOrDefaultAsync(m => m.id == id);
            if (sobreMi == null)
            {
                return NotFound();
            }

            return View(sobreMi);
        }

        // GET: SobreMi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SobreMi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,descripcion,contenido,archivoImagen,visible,prioridad")] SobreMi sobreMi)
        {
            if (ModelState.IsValid)
            {
                if (sobreMi.archivoImagen == null)
                {
                    ModelState.AddModelError("archivoImagen", "Imagen es un campo requerido.");
                }
                else
                {
                    //guarda la imagen en wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(sobreMi.archivoImagen.FileName);
                    string extension = Path.GetExtension(sobreMi.archivoImagen.FileName);
                    sobreMi.imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await sobreMi.archivoImagen.CopyToAsync(fileStream);
                    }

                    _context.Add(sobreMi);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(sobreMi);
        }

        // GET: SobreMi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sobreMi = await _context.SobreMi.FindAsync(id);
            if (sobreMi == null)
            {
                return NotFound();
            }
            return View(sobreMi);
        }

        // POST: SobreMi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,descripcion,contenido,imagen,visible,prioridad,archivoImagen")] SobreMi sobreMi)
        {
            if (id != sobreMi.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string path = null;

                    if (sobreMi.archivoImagen != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(sobreMi.archivoImagen.FileName);
                        string extension = Path.GetExtension(sobreMi.archivoImagen.FileName);
                        sobreMi.imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        path = Path.Combine(wwwRootPath + "/image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await sobreMi.archivoImagen.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(sobreMi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SobreMiExists(sobreMi.id))
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
            return View(sobreMi);
        }

        // GET: SobreMi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sobreMi = await _context.SobreMi
                .FirstOrDefaultAsync(m => m.id == id);
            if (sobreMi == null)
            {
                return NotFound();
            }

            return View(sobreMi);
        }

        // POST: SobreMi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sobreMi = await _context.SobreMi.FindAsync(id);

            //borra imagen de wwwroot/image
            var sobreMiPath = Path.Combine(_hostEnvironment.WebRootPath, "image", sobreMi.imagen);
            if (System.IO.File.Exists(sobreMiPath))
            {
                System.IO.File.Delete(sobreMiPath);
            }

            _context.SobreMi.Remove(sobreMi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SobreMiExists(int id)
        {
            return _context.SobreMi.Any(e => e.id == id);
        }
    }
}
