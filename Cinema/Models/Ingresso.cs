using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Ingresso
    {
        public long? IngressoId { get; set; }
        public float Preco { get; set; }
        [ForeignKey("Sessao")]
        public long? SessaoId { get; set; }
        public Sessao Sessao { get; set; }

        public Ingresso(){

        }
    }
}
