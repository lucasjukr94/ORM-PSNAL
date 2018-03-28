using System.Collections.Generic;
using Objects.Geral;

namespace PSNAL.Models
{
    public class Unificador
    {
        public List<Food> FoodList { get; set; }
        public List<Exercises> ExercisesList { get; set; }
        public List<Usuario> UserList { get; set; }
        public Food Food { get; set; }
        public Exercises Exercises { get; set; }
        public Usuario User { get; set; }
        public Agendas agenda { get; set; }
        public Dieta dieta { get; set; }
        public List<Dieta> DietaList { get; set; }
        public List<string> lista { get; set; }
        public SET set { get; set; }
        public List<SET> SetList { get; set; }
    }
}