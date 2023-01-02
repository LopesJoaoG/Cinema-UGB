using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public class CinemaContext:DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options)
              : base(options)
        {

        }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Ingresso> Ingressos { get; set; }
        public DbSet<Sala> Salas { get; set;}
        public DbSet<Sessao> Sessaos { get; set; }

    }
}
