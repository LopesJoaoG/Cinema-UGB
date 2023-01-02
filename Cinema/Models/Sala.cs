using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Sala
    {
        public long? SalaId { get; set; }
        public int Numero { get; set; }
        public float Poltrona { get; set; }

        public Sala()
        {

        }
    }
}
