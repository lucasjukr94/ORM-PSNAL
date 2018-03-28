using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Objects.Geral
{
    public class Exercises
    {
        public long? Id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal calpertime { get; set; }
        public string dificuldade { get; set; }
        public string area { get; set; }
    }
}
