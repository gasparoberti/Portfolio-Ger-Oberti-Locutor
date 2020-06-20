using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcNoticia.Data;
using PortfolioCore.Data;
using PortfolioCore.Models;

namespace PortfolioCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcNoticiaContext _context;
        private readonly MvcRelatoContext _contextRelato;
        private readonly MvcTipContext _contextTip;
        private readonly MvcSobreMiContext _contextSobreMi;
        private readonly MvcPodcastContext _contextPodcast;
        private readonly MvcConfigContext _contextConfig;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, 
            MvcNoticiaContext context, 
            MvcRelatoContext contextRelato,
            MvcTipContext contextTip, 
            MvcSobreMiContext contextSobreMi,
            MvcPodcastContext contextPodcast,
            MvcConfigContext contextConfig, 
            IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _contextRelato = contextRelato;
            _contextTip = contextTip;
            _contextSobreMi = contextSobreMi;
            _contextPodcast = contextPodcast;
            _contextConfig = contextConfig;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            //config
            var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            ViewBag.config = config;


            //últimas publicaciones
            var relato = (from r in _contextRelato.Relato orderby r.fecha_alta descending select r).First();
            ViewBag.relato = relato;

            var podcast = (from p in _contextPodcast.Podcast orderby p.fecha_alta descending select p).First();
            ViewBag.podcast = podcast;

            var tip = (from t in _contextTip.Tip orderby t.fecha_alta descending select t).First();
            ViewBag.tip = tip;


            //publicaciones favoritas
            var relatoFav = (from r in _contextRelato.Relato orderby r.prioridad select r).First();
            ViewBag.relatoFav = relatoFav;

            var podcastFab = (from p in _contextPodcast.Podcast orderby p.prioridad select p).First();
            ViewBag.podcastFab = podcastFab;

            var tipFab = (from t in _contextTip.Tip orderby t.prioridad select t).First();
            ViewBag.tipFab = tipFab;


            return View(await _context.Noticia.ToListAsync());
        }

        public async Task<IActionResult> Relatos()
        {
            //config
            var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            ViewBag.config = config;

            return View(await _contextRelato.Relato.ToListAsync());
        }
        
        public async Task<IActionResult> Tips()
        {
            return View(await _contextTip.Tip.ToListAsync());
        }

        public async Task<IActionResult> SobreMi()
        {
            return View(await _contextSobreMi.SobreMi.ToListAsync());
        }
        
        public async Task<IActionResult> Podcasts()
        {
            //config
            var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            ViewBag.config = config;

            return View(await _contextPodcast.Podcast.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Noticias/Detalles/5
        public async Task<IActionResult> DetalleNoticia(int? id)
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
        
        // GET: Noticias/Detalles/5
        public async Task<IActionResult> DetalleTip(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tip = await _contextTip.Tip
                .FirstOrDefaultAsync(m => m.id == id);
            if (tip == null)
            {
                return NotFound();
            }

            return View(tip);
        }
        
        // GET: Relatos/Detalles/5
        public async Task<IActionResult> DetalleRelato(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relato = await _contextRelato.Relato
                .FirstOrDefaultAsync(m => m.id == id);
            if (relato == null)
            {
                return NotFound();
            }

            return View(relato);
        }
        
        // GET: Relatos/Detalles/5
        public async Task<IActionResult> DetallePodcast(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _contextPodcast.Podcast
                .FirstOrDefaultAsync(m => m.id == id);
            if (podcast == null)
            {
                return NotFound();
            }

            return View(podcast);
        }
    }
}
