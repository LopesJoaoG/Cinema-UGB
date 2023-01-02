using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Sessao
    {
        public long? SessaoId { get; set; }

        public int Numero { get; set; }
        public DateTime DataHorario { get; set; }
        public int Vagas { get; set; }
        [ForeignKey("Filme")]
        public long? FilmeId { get; set; }
        public Filme Filme { get; set; }

        [ForeignKey("Sala")]
        public long? SalaId { get; set; }
        public Sala Sala { get; set; }
        public Sessao()
        {

        }

    }
}
