using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Objects.Geral;
using PSNAL.Models;

namespace PSNAL.Controllers
{
    public class ExerciseController : BaseController
    {
        // GET: Exercise
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CriarSet()
        {
            Unificador unificador = new Unificador();
            List<SelectListItem> listaExercicio = new List<SelectListItem>();
            unificador.ExercisesList = new List<Exercises>();
            unificador.Exercises = new Exercises();
            string queryBuscaExercise = @"select * from PSNAL.dbo.tbl_exercicio";
            unificador.ExercisesList = Selectlist(queryBuscaExercise, unificador.Exercises);
            foreach (var p in unificador.ExercisesList)
            {
                listaExercicio.Add(new SelectListItem { Text = p.nome, Value = p.Id.ToString() });
            }
            ViewBag.ListaExercicio = listaExercicio;
            return View(unificador);
        }

        public ActionResult AdicionarSet(Unificador unificador)
        {
            string queryInsereSet = @"insert into PSNAL.dbo.tbl_set(nome,descricao,UsuarioResponsavelId) values(@nome,@descricao,@UsuarioResponsavelId)";
            unificador.set.UsuarioResponsavelId = CurrentUser.id;
            IUD(queryInsereSet, unificador.set);

            string queryBuscaSetId = @"select top 1 Id from PSNAL.dbo.tbl_set order by Id desc";
            SET set = new SET();
            set = Select(queryBuscaSetId, set);

            foreach (var p in unificador.lista)
            {
                string queryInsereSetExercicio = @"insert into PSNAL.dbo.tbl_set_exercicio(SetId,ExercicioId) values(@SetId,@ExercicioId)";
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("@SetId", set.Id.ToString());
                parametros.Add("@ExercicioId", p);
                IUD(queryInsereSetExercicio, parametros);
            }

            return RedirectToAction("ListaSet");
        }

        public ActionResult ListaSet()
        {
            Unificador unificador = new Unificador();
            unificador.ExercisesList = new List<Exercises>();
            unificador.set = new SET()
            {
                UsuarioResponsavelId = CurrentUser.id
            };
            string queryBuscaSetUser = @"select * from PSNAL.dbo.tbl_set where UsuarioResponsavelId = @UsuarioResponsavelId";
            unificador.SetList = Selectlist(queryBuscaSetUser, unificador.set);

            List<SetExercicioVm> setExerciciolist = new List<SetExercicioVm>();
            foreach (var p in unificador.SetList)
            {
                SetExercicioVm setExercicio = new SetExercicioVm();
                string queryBuscaDieta = @"select * from PSNAL.dbo.v_set_exercicio where SetId = @SetId";
                setExercicio.SetId = p.Id;
                List<SetExercicioVm> dietaFoodlist2 = Selectlist(queryBuscaDieta, setExercicio);
                setExercicio.dificuldade = "";
                setExercicio.calpertime = 0;
                foreach (var z in dietaFoodlist2)
                {
                    setExercicio.calpertime += z.calpertime;
                    setExercicio.dificuldade += z.dificuldade;
                    setExercicio.area += "," + z.area;
                    setExercicio.exercicioNome += "," + z.exercicioNome;
                }
                setExercicio.setNome = p.nome;
                setExercicio.UsuarioResponsavelId = p.UsuarioResponsavelId;
                setExercicio.descricao = p.descricao;
                setExerciciolist.Add(setExercicio);
            }

            return View(setExerciciolist);
        }

        [HttpPost]
        public ActionResult DeleteSET(long? Id)
        {
            if (Id == null)
            {
                return RedirectToAction("ListaSet");
            }
            Dictionary<string,string> parametros = new Dictionary<string, string>();
            parametros.Add("Id",Id.ToString());
            string queryDeleta = @"delete from PSNAL.dbo.tbl_set where Id = @Id";
            IUD(queryDeleta,parametros);

            string queryDeletaConexao = @"delete from PSNAL.dbo.tbl_set_exercicio where SetId = @Id";
            IUD(queryDeletaConexao,parametros);

            return RedirectToAction("ListaSet");
        }

        public ActionResult CriarExercicio()
        {
            return View();
        }

        public ActionResult ListaExercicio()
        {
            Unificador unificador = new Unificador();
            unificador.User = CurrentUser;
            unificador.ExercisesList = new List<Exercises>();

            string queryBuscaListaexercicio = @"select * from PSNAL.dbo.tbl_exercicio";
            unificador.ExercisesList = Selectlist(queryBuscaListaexercicio, new Exercises());

            return View(unificador);
        }

        [HttpPost]
        public ActionResult Delete(long Id = 0)
        {
            string queryDeletaExercicio = @"delete from PSNAL.dbo.tbl_exercicio where Id = @Id";
            Exercises ex = new Exercises()
            {
                Id = Id
            };
            IUD<Exercises>(queryDeletaExercicio,ex);

            return RedirectToAction("ListaExercicio");
        }

        [HttpPost]
        public ActionResult Adicionar(Exercises exercise)
        {
            string queryAdicionaExercicio = @"insert into PSNAL.dbo.tbl_exercicio(nome,descricao,calpertime,dificuldade,area) values(@nome,@descricao,@calpertime,@dificuldade,@area)";
            IUD(queryAdicionaExercicio,exercise);
            return RedirectToAction("ListaExercicio");
        }
    }
}