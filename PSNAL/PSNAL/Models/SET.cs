using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSNAL.Models
{
    public class SET
    {
        public long? Id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public long? UsuarioResponsavelId { get; set; }
    }
}