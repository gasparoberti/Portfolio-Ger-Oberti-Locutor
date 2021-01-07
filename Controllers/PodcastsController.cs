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
    public class PodcastsController : Controller
    {
        private readonly MvcPodcastContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PodcastsController(MvcPodcastContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Podcasts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Podcast.ToListAsync());
        }

        // GET: Podcasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _context.Podcast
                .FirstOrDefaultAsync(m => m.id == id);
            if (podcast == null)
            {
                return NotFound();
            }

            return View(podcast);
        }

        // GET: Podcasts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Podcasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,url,titulo,descripcion,contenido,visible,prioridad,fecha_alta,archivoImagen,contenido2,visibleI,visibleC2")] Podcast podcast)
        {
            if (ModelState.IsValid)
            {
                if (podcast.archivoImagen != null)
                {
                    //guarda la imagen en wwwroot/image
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(podcast.archivoImagen.FileName);
                    string extension = Path.GetExtension(podcast.archivoImagen.FileName);
                    podcast.imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/image/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await podcast.archivoImagen.CopyToAsync(fileStream);
                    }
                }

                if (podcast.archivoImagen == null && podcast.visibleI == true)
                {
                    podcast.visibleI = false;
                }

                _context.Add(podcast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            return View(podcast);
        }

        // GET: Podcasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _context.Podcast.FindAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }
            return View(podcast);
        }

        // POST: Podcasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,url,titulo,descripcion,contenido,visible,prioridad,fecha_alta,imagen,archivoImagen,contenido2,visibleI,visibleC2")] Podcast podcast)
        {
            if (id != podcast.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string path = null;

                    if (podcast.archivoImagen != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(podcast.archivoImagen.FileName);
                        string extension = Path.GetExtension(podcast.archivoImagen.FileName);
                        podcast.imagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        path = Path.Combine(wwwRootPath + "/image/", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await podcast.archivoImagen.CopyToAsync(fileStream);
                        }
                    }

                    //para los opcionales hay que hacer esta verificacion por si se activa el check pero la imagen es nulla
                    if (podcast.imagen == null && podcast.visibleI == true)
                    {
                        podcast.visibleI = false;
                    }

                    _context.Update(podcast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PodcastExists(podcast.id))
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
            return View(podcast);
        }

        // GET: Podcasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _context.Podcast
                .FirstOrDefaultAsync(m => m.id == id);
            if (podcast == null)
            {
                return NotFound();
            }

            return View(podcast);
        }

        // POST: Podcasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podcast = await _context.Podcast.FindAsync(id);
            _context.Podcast.Remove(podcast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodcastExists(int id)
        {
            return _context.Podcast.Any(e => e.id == id);
        }
    }
}

