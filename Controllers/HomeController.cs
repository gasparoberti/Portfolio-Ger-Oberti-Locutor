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
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, 
            MvcNoticiaContext context, 
            MvcRelatoContext contextRelato,
            MvcTipContext contextTip, 
            IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _contextRelato = contextRelato;
            _contextTip = contextTip;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Noticia.ToListAsync());
        }

        public async Task<IActionResult> Relatos()
        {
            return View(await _contextRelato.Relato.ToListAsync());
        }
        
        public async Task<IActionResult> Tips()
        {
            return View(await _contextTip.Tip.ToListAsync());
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
    }
}
