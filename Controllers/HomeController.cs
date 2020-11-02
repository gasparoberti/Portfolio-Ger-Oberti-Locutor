using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using MvcNoticia.Data;
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
        private readonly MvcPortfolioContext _contextPortfolio;
        private readonly MvcPodcastContext _contextPodcast;
        private readonly MvcConfigContext _contextConfig;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger,
            MvcNoticiaContext context,
            MvcRelatoContext contextRelato,
            MvcTipContext contextTip,
            MvcSobreMiContext contextSobreMi,
            MvcPortfolioContext contextPortfolio,
            MvcPodcastContext contextPodcast,
            MvcConfigContext contextConfig,
            IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _contextRelato = contextRelato;
            _contextTip = contextTip;
            _contextSobreMi = contextSobreMi;
            _contextPortfolio = contextPortfolio;
            _contextPodcast = contextPodcast;
            _contextConfig = contextConfig;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            //config
            var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            ViewBag.config = config;

            var portfolio = (from p in _contextPortfolio.Portfolio where p != null orderby p.fecha_alta descending select p).DefaultIfEmpty().First();
            ViewBag.portfolio = portfolio;

            var podcast = (from p in _contextPodcast.Podcast where p != null orderby p.fecha_alta descending select p).DefaultIfEmpty().First();
            ViewBag.podcast = podcast;

            var tip = (from t in _contextTip.Tip where t != null orderby t.fecha_alta descending select t).DefaultIfEmpty().First();
            ViewBag.tip = tip;

            
            var portfolioFav = (from p in _contextPortfolio.Portfolio where p != null orderby p.prioridad select p).DefaultIfEmpty().First();
            ViewBag.portfolioFav = portfolioFav;

            var podcastFab = (from p in _contextPodcast.Podcast where p != null orderby p.prioridad select p).DefaultIfEmpty().First();
            ViewBag.podcastFab = podcastFab;

            var tipFab = (from t in _contextTip.Tip where t != null orderby t.prioridad select t).DefaultIfEmpty().First();
            ViewBag.tipFab = tipFab;


            return View(await _context.Noticia.ToListAsync());
        }

        public async Task<IActionResult> Noticias()
        {
            //config
            //var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            //ViewBag.config = config;

            return View(await _context.Noticia.ToListAsync());
        }

        public async Task<IActionResult> Tips()
        {
            //config
            var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            ViewBag.config = config;

            return View(await _contextTip.Tip.ToListAsync());
        }

        public async Task<IActionResult> SobreMi()
        {
            //config
            var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            ViewBag.config = config;

            return View(await _contextSobreMi.SobreMi.ToListAsync());
        }

        public async Task<IActionResult> Podcasts()
        {
            //config
            var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            ViewBag.config = config;

            return View(await _contextPodcast.Podcast.ToListAsync());
        }

        public async Task<IActionResult> Portfolios()
        {
            //config
            var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            ViewBag.config = config;

            return View(await _contextPortfolio.Portfolio.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // GET: Noticias/Detalles/5
        public async Task<IActionResult> DetalleNoticia(int? id)
        {
            //noticias
            var noticias = (from n in _context.Noticia where n != null select n);
            ViewBag.noticias = noticias;

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
        
        // GET: Portfolios/Detalles/5
        public async Task<IActionResult> DetallePortfolio(int? id)
        {
            //porfolios
            var porfolios = (from p in _contextPortfolio.Portfolio where p != null select p);
            ViewBag.porfolios = porfolios;

            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _contextPortfolio.Portfolio
                .FirstOrDefaultAsync(m => m.id == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // GET: Tips/Detalles/5
        public async Task<IActionResult> DetalleTip(int? id)
        {
            //tips
            var tips = (from t in _contextTip.Tip where t != null select t);
            ViewBag.tips = tips;

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

        // GET: Podcasts/Detalles/5
        public async Task<IActionResult> DetallePodcast(int? id)
        {
            //config
            var config = (from c in _contextConfig.Config where c != null select c).DefaultIfEmpty().First();
            ViewBag.config = config;

            //podcasts
            var podcasts = (from p in _contextPodcast.Podcast where p != null select p);
            ViewBag.podcasts = podcasts;

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
