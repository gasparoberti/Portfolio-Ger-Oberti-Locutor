﻿using Microsoft.EntityFrameworkCore;
using PortfolioCore.Models;

namespace MvcNoticia.Data
{
    public class MvcNoticiaContext : DbContext
    {
        public MvcNoticiaContext (DbContextOptions<MvcNoticiaContext> options)
            : base(options)
        {
        }

        public DbSet<Noticia> Noticia { get; set; }
    }
}