using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.SqlClient;
using Objects.Geral;
using PSNAL.Models;

namespace PSNAL.Controllers
{
    public class PerfilController : BaseController
    {
        public ActionResult Index()
        {
            string queryBuscaAgenda = @"select * from PSNAL.dbo.tbl_agenda where UsuarioResponsavelId = @UsuarioResponsavelId";
            Agendas agenda = new Agendas()
            {
                UsuarioResponsavelId = CurrentUser.id.ToString()
            };
            agenda = Select(queryBuscaAgenda, agenda);

            Unificador unificador = new Unificador();
            unificador.User = CurrentUser;
            unificador.agenda = agenda;
            unificador.Exercises = new Exercises();
            unificador.Food = new Food();
            unificador.ExercisesList = new List<Exercises>();
            unificador.FoodList = new List<Food>();
            unificador.UserList = new List<Usuario>();
            return View(unificador);
        }

        public ActionResult Progressos()
        {
            return View();
        }

        public ActionResult Editar()
        {
            Unificador unificador = new Unificador();

            string queryBuscaAgenda = @"select * from PSNAL.dbo.tbl_agenda where UsuarioResponsavelId = @UsuarioResponsavelId";
            Agendas agenda = new Agendas()
            {
                UsuarioResponsavelId = CurrentUser.id.ToString()
            };
            agenda = Select(queryBuscaAgenda, agenda);

            string queryBuscaDieta = @"select top 10 * from PSNAL.dbo.v_dietafood";
            List<DietaFoodVm> listaDieta = Selectlist(queryBuscaDieta, new DietaFoodVm());
            List<SelectListItem> selectlist = new List<SelectListItem>();
            foreach (var p in listaDieta)
            {
                selectlist.Add(new SelectListItem{Text = p.dietaNome,Value = p.dietaNome});
            }
            ViewBag.ListaDieta = selectlist;

            List<SelectListItem> selectlist2 = new List<SelectListItem>();
            string queryBuscaSet = @"select top 10 * from PSNAL.dbo.v_set_exercicio";
            List<SetExercicioVm> listaSet = Selectlist(queryBuscaSet, new SetExercicioVm());
            foreach (var p in listaSet)
            {
                selectlist2.Add(new SelectListItem { Text = p.setNome, Value = p.setNome });
            }
            ViewBag.ListaSet = selectlist2;
            
            unificador.User = CurrentUser;
            unificador.agenda = agenda;
            return View(unificador);
        }

        [HttpPost]
        public ActionResult Update(Unificador unificador)
        {
            string queryUpdate = @"update PSNAL.dbo.tbl_user set nome=@nome,login=@login,email=@email,endereco=@endereco,meta=@meta where Id = @Id";
            Usuario user = new Usuario()
            {
                login = unificador.User.login,
                nome = unificador.User.nome,
                email = unificador.User.email,
                meta = unificador.User.meta,
                endereco = unificador.User.endereco,
                id = unificador.User.id
            };
            IUD<Usuario>(queryUpdate,user);

            string queryBuscaAgenda = @"select * from PSNAL.dbo.tbl_agenda where UsuarioResponsavelId = @UsuarioResponsavelId";
            Agendas agend = new Agendas()
            {
                UsuarioResponsavelId = CurrentUser.id.ToString()
            };
            agend = Select(queryBuscaAgenda, agend);

            Dictionary<string, string> parametros = new Dictionary<string, string>();

            parametros.Add("@UsuarioResponsavelId", CurrentUser.id.ToString());
            parametros.Add("@DMSeg", unificador.agenda.DMSeg);
            parametros.Add("@DMTer", unificador.agenda.DMTer);
            parametros.Add("@DMQuar", unificador.agenda.DMQuar);
            parametros.Add("@DMQuin", unificador.agenda.DMQuin);
            parametros.Add("@DMSex", unificador.agenda.DMSex);
            parametros.Add("@DMSab", unificador.agenda.DMSab);
            parametros.Add("@DMDom", unificador.agenda.DMDom);
            parametros.Add("@DTSeg", unificador.agenda.DTSeg);
            parametros.Add("@DTTer", unificador.agenda.DTTer);
            parametros.Add("@DTQuar", unificador.agenda.DTQuar);
            parametros.Add("@DTQuin", unificador.agenda.DTQuin);
            parametros.Add("@DTSex", unificador.agenda.DTSex);
            parametros.Add("@DTSab", unificador.agenda.DTSab);
            parametros.Add("@DTDom", unificador.agenda.DTDom);
            parametros.Add("@DNSeg", unificador.agenda.DNSeg);
            parametros.Add("@DNTer", unificador.agenda.DNTer);
            parametros.Add("@DNQuar", unificador.agenda.DNQuar);
            parametros.Add("@DNQuin", unificador.agenda.DNQuin);
            parametros.Add("@DNSex", unificador.agenda.DNSex);
            parametros.Add("@DNSab", unificador.agenda.DNSab);
            parametros.Add("@DNDom", unificador.agenda.DNDom);
            parametros.Add("@SMSeg", unificador.agenda.SMSeg);
            parametros.Add("@SMTer", unificador.agenda.SMTer);
            parametros.Add("@SMQuar", unificador.agenda.SMQuar);
            parametros.Add("@SMQuin", unificador.agenda.SMQuin);
            parametros.Add("@SMSex", unificador.agenda.SMSex);
            parametros.Add("@SMSab", unificador.agenda.SMSab);
            parametros.Add("@SMDom", unificador.agenda.SMDom);
            parametros.Add("@STSeg", unificador.agenda.STSeg);
            parametros.Add("@STTer", unificador.agenda.STTer);
            parametros.Add("@STQuar", unificador.agenda.STQuar);
            parametros.Add("@STQuin", unificador.agenda.STQuin);
            parametros.Add("@STSex", unificador.agenda.STSex);
            parametros.Add("@STSab", unificador.agenda.STSab);
            parametros.Add("@STDom", unificador.agenda.STDom);
            parametros.Add("@SNSeg", unificador.agenda.SNSeg);
            parametros.Add("@SNTer", unificador.agenda.SNTer);
            parametros.Add("@SNQuar", unificador.agenda.SNQuar);
            parametros.Add("@SNQuin", unificador.agenda.SNQuin);
            parametros.Add("@SNSex", unificador.agenda.SNSex);
            parametros.Add("@SNSab", unificador.agenda.SNSab);
            parametros.Add("@SNDom", unificador.agenda.SNDom);

            if (agend.UsuarioResponsavelId == null)
            {
                string queryInsereAgenda = @"insert into PSNAL.dbo.tbl_agenda(
UsuarioResponsavelId,
DMSeg ,
DMTer ,
DMQuar,
DMQuin,
DMSex ,
DMSab ,
DMDom ,
      
DTSeg ,
DTTer ,
DTQuar,
DTQuin,
DTSex ,
DTSab ,
DTDom ,
      
DNSeg ,
DNTer ,
DNQuar,
DNQuin,
DNSex ,
DNSab ,
DNDom ,
      
SMSeg ,
SMTer ,
SMQuar,
SMQuin,
SMSex ,
SMSab ,
SMDom ,
      
STSeg ,
STTer ,
STQuar,
STQuin,
STSex ,
STSab ,
STDom ,
      
SNSeg ,
SNTer ,
SNQuar,
SNQuin,
SNSex ,
SNSab ,
SNDom
) values(
@UsuarioResponsavelId,
@DMSeg ,
@DMTer ,
@DMQuar,
@DMQuin,
@DMSex ,
@DMSab ,
@DMDom ,
@DTSeg ,
@DTTer ,
@DTQuar,
@DTQuin,
@DTSex ,
@DTSab ,
@DTDom ,
@DNSeg ,
@DNTer ,
@DNQuar,
@DNQuin,
@DNSex ,
@DNSab ,
@DNDom ,
@SMSeg ,
@SMTer ,
@SMQuar,
@SMQuin,
@SMSex ,
@SMSab ,
@SMDom ,
@STSeg ,
@STTer ,
@STQuar,
@STQuin,
@STSex ,
@STSab ,
@STDom ,
@SNSeg ,
@SNTer ,
@SNQuar,
@SNQuin,
@SNSex ,
@SNSab ,
@SNDom 
)";
                IUD(queryInsereAgenda,parametros);
            }
            else
            {
                string queryUpdateAgenda = @"update PSNAL.dbo.tbl_agenda set UsuarioResponsavelId =@UsuarioResponsavelId,
                                            DMSeg =@DMSeg,
                                            DMTer =@DMTer,
                                            DMQuar =@DMQuar,
                                            DMQuin =@DMQuin,
                                            DMSex =@DMSex,
                                            DMSab =@DMSab,
                                            DMDom =@DMDom,

                                            DTSeg =@DTSeg,
                                            DTTer =@DTTer,
                                            DTQuar =@DTQuar,
                                            DTQuin =@DTQuin,
                                            DTSex =@DTSex,
                                            DTSab =@DTSab,
                                            DTDom =@DTDom,

                                            DNSeg =@DNSeg,
                                            DNTer =@DNTer,
                                            DNQuar =@DNQuar,
                                            DNQuin =@DNQuin,
                                            DNSex =@DNSex,
                                            DNSab =@DNSab,
                                            DNDom =@DNDom,

                                            SMSeg =@SMSeg,
                                            SMTer =@SMTer,
                                            SMQuar =@SMQuar,
                                            SMQuin =@SMQuin,
                                            SMSex =@SMSex,
                                            SMSab =@SMSab,
                                            SMDom =@SMDom,

                                            STSeg =@STSeg,
                                            STTer =@STTer,
                                            STQuar =@STQuar,
                                            STQuin =@STQuin,
                                            STSex =@STSex,
                                            STSab =@STSab,
                                            STDom =@STDom,

                                            SNSeg =@SNSeg,
                                            SNTer =@SNTer,
                                            SNQuar =@SNQuar,
                                            SNQuin =@SNQuin,
                                            SNSex =@SNSex,
                                            SNSab =@SNSab,
                                            SNDom =@SNDom

                                            where UsuarioResponsavelId = @UsuarioResponsavelId
                                            ";
                IUD(queryUpdateAgenda, parametros);
            }

            return RedirectToAction("Index");
        }
    }
}
