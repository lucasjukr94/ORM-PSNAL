using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Objects.Geral
{
    public class Usuario
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Campo login obrigatório")]
        public string login { get; set; }
        [Required(ErrorMessage = "Campo senha obrigatório")]
        public string senha { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string endereco { get; set; }
        public string meta { get; set; }
        public decimal? peso { get; set; }
    }
}
