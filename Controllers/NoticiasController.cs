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
    public class NoticiasController : Controller
    {
        private readonly MvcNoticiaContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NoticiasController(MvcNoticiaContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Noticias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Noticia.ToListAsync());
        }

        // GET: Noticias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticia
                .FirstOrDefaultAsync(m => m.id == id);
            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        // GET: Noticias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,titulo,descripcion,contenido,archivoImagen,visible,fecha_alta,video,contenido2,visibleV,visibleC2")] Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                if (noticia.archivoImagen == null)
                {
                    ModelState.AddModelError("archivoImagen", "Imagen es un campo requerido.");
                }
                else
                {
                    //guarda la imagen en wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(noticia.archivoImagen.FileName);
                    string extension = Path.GetExtension(noticia.archivoImagen.FileName);
                    noticia.imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await noticia.archivoImagen.CopyToAsync(fileStream);
                    }

                    _context.Add(noticia);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(noticia);
        }

        // GET: Noticias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticia.FindAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }
            return View(noticia);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,titulo,descripcion,contenido,imagen,visible,fecha_alta,archivoImagen,video,contenido2,visibleV,visibleC2")] Noticia noticia)
        {
            if (id != noticia.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string path = null;

                    if (noticia.archivoImagen != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(noticia.archivoImagen.FileName);
                        string extension = Path.GetExtension(noticia.archivoImagen.FileName);
                        noticia.imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        path = Path.Combine(wwwRootPath + "/image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await noticia.archivoImagen.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(noticia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticiaExists(noticia.id))
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
            return View(noticia);
        }

        // GET: Noticias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticia = await _context.Noticia
                .FirstOrDefaultAsync(m => m.id == id);
            if (noticia == null)
            {
                return NotFound();
            }

            return View(noticia);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noticia = await _context.Noticia.FindAsync(id);

            //borra imagen de wwwroot/image
            var noticiaPath = Path.Combine(_hostEnvironment.WebRootPath, "image", noticia.imagen);
            if (System.IO.File.Exists(noticiaPath))
            {
                System.IO.File.Delete(noticiaPath);
            }

            _context.Noticia.Remove(noticia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticiaExists(int id)
        {
            return _context.Noticia.Any(e => e.id == id);
        }
    }
}

