using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Objects.Geral
{
    public class Food
    {
        public long? Id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public decimal calpergram { get; set; }
        public decimal preco { get; set; }
        public string nutrientes { get; set; }
    }
}
