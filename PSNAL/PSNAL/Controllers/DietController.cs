using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Objects.Geral;
using PSNAL.Models;

namespace PSNAL.Controllers
{
    public class DietController : BaseController
    {
        // GET: Diet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CriarDieta()
        {
            Unificador unificador = new Unificador();
            List<SelectListItem> listaFood = new List<SelectListItem>();
            unificador.FoodList = new List<Food>();
            unificador.Food = new Food();
            string queryBuscaFood = @"select * from PSNAL.dbo.tbl_food";
            unificador.FoodList = Selectlist(queryBuscaFood,unificador.Food);
            foreach (var p in unificador.FoodList)
            {
                listaFood.Add(new SelectListItem {Text = p.nome, Value = p.Id.ToString()});
            }
            ViewBag.ListaFood = listaFood;
            return View(unificador);
        }

        [HttpPost]
        public ActionResult AdicionarDieta(Unificador unificador)
        {
            string queryInsereDieta = @"insert into PSNAL.dbo.tbl_dieta(nome,descricao,UsuarioResponsavelId) values(@nome,@descricao,@UsuarioResponsavelId)";
            unificador.dieta.UsuarioResponsavelId = CurrentUser.id;
            IUD(queryInsereDieta,unificador.dieta);

            string queryBuscaDietaId = @"select top 1 Id from PSNAL.dbo.tbl_dieta order by Id desc";
            Dieta diet = new Dieta();
            diet = Select(queryBuscaDietaId, diet);

            foreach (var p in unificador.lista)
            {
                string queryInsereDietaFood = @"insert into PSNAL.dbo.tbl_dieta_food(DietaId,FoodId) values(@DietaId,@FoodId)";
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("@DietaId",diet.Id.ToString());
                parametros.Add("@FoodId",p);
                IUD(queryInsereDietaFood,parametros);
            }

            return RedirectToAction("ListaDieta");
        }

        [HttpPost]
        public ActionResult DeleteDieta(long Id = 0)
        {
            string queryDeletaDieta = @"delete from PSNAL.dbo.tbl_dieta where Id = @Id";
            string queryDeletaDietaFood = @"delete from PSNAL.dbo.tbl_dieta_food where Id = @Id";
            Dictionary<string,string> parametros = new Dictionary<string, string>();
            parametros.Add("@Id", Id.ToString());
            IUD(queryDeletaDieta, parametros);
            IUD(queryDeletaDietaFood, parametros);

            return RedirectToAction("ListaDieta");
        }

        public ActionResult ListaDieta()
        {
            Unificador unificador = new Unificador();
            unificador.DietaList = new List<Dieta>();
            unificador.dieta = new Dieta()
            {
                UsuarioResponsavelId = CurrentUser.id 
            };
            string queryBuscaDietaUser = @"select * from PSNAL.dbo.tbl_dieta where UsuarioResponsavelId = @UsuarioResponsavelId";
            unificador.DietaList = Selectlist(queryBuscaDietaUser, unificador.dieta);

            List<DietaFoodVm> dietaFoodlist = new List<DietaFoodVm>();
            foreach(var p in unificador.DietaList)
            {
                DietaFoodVm dietaFood = new DietaFoodVm();
                string queryBuscaDieta = @"select * from PSNAL.dbo.v_dietafood where DietaId = @DietaId";
                dietaFood.DietaId = p.Id;
                List<DietaFoodVm> dietaFoodlist2 = Selectlist(queryBuscaDieta, dietaFood);
                dietaFood.preco = 0;
                foreach (var z in dietaFoodlist2)
                {
                    dietaFood.calpergram += z.calpergram;
                    dietaFood.preco += z.preco;
                    dietaFood.nutrientes += ","+z.nutrientes;
                    dietaFood.foodNome += "," + z.foodNome;
                }
                dietaFood.dietaNome = p.nome;
                dietaFood.UsuarioResponsavelId = p.UsuarioResponsavelId;
                dietaFood.descricao = p.descricao;
                dietaFoodlist.Add(dietaFood);
            }

            return View(dietaFoodlist);
        }

        public ActionResult AddFood()
        {
            return View();
        }

        public ActionResult ListaFood()
        {
            Unificador unificador = new Unificador();
            unificador.User = CurrentUser;

            string queryBuscaFoodList = @"select * from PSNAL.dbo.tbl_food";
            unificador.FoodList = new List<Food>();
            unificador.FoodList = Selectlist(queryBuscaFoodList, new Food());

            return View(unificador);
        }

        [HttpPost]
        public ActionResult Delete(long Id = 0)
        {
            string queryDeletaFood = @"delete from PSNAL.dbo.tbl_food where Id = @id";
            Food foo = new Food()
            {
                Id = Id
            };
            IUD(queryDeletaFood,foo);


            return RedirectToAction("ListaFood");
        }

        [HttpPost]
        public ActionResult Adicionar(Food food)
        {
            string queryInsereFood = @"insert into PSNAL.dbo.tbl_food(nome,descricao,calpergram,preco,nutrientes) values(@nome,@descricao,@calpergram,@preco,@nutrientes)";
            IUD(queryInsereFood,food);
            return RedirectToAction("ListaFood");
        }
    }
}