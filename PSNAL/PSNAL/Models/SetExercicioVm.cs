using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace PSNAL.Models
{
    public class SetExercicioVm
    {
        public long? SetId { get; set; }
        public long? ExercicioId { get; set; }
        public string setNome { get; set; }
        public decimal? calpertime { get; set; }
        public string exercicioNome { get; set; }
        public string dificuldade { get; set; }
        public long? UsuarioResponsavelId { get; set; }
        public string area { get; set; }
        [ScriptIgnore]
        public string descricao { get; set; }
    }
}