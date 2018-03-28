using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace PSNAL.Models
{
    public class Dieta
    {
        public long? Id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public long? UsuarioResponsavelId { get; set; }
    }
}