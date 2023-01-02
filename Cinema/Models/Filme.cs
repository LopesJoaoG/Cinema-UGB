using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Filme
    {
        public long? FilmeId { get; set; }
        public string Nome { get; set; }
        public string Duracao { get; set; }
        public string Genero { get; set; }
        public string IdadeIndicativa { get; set; }
        public Filme()
        {

        }
    }
}
