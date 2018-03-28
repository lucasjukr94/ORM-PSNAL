using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace PSNAL.Models
{
    public class DietaFoodVm
    {
        public long? Id { get; set; }
        public long? DietaId { get; set; }
        public long? FoodId { get; set; }
        public string dietaNome { get; set; }
        public decimal calpergram { get; set; }
        public string nutrientes { get; set; }
        public decimal? preco { get; set; }
        public string foodNome { get; set; }
        public long? UsuarioResponsavelId { get; set; }
        [ScriptIgnore]
        public string descricao { get; set; }
    }
}