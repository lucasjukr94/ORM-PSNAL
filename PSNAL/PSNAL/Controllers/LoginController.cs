using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.SqlClient;
using PSNAL.Infra;
using Objects.Geral;

namespace PSNAL.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Usuario()
        {
            verificaUser();
            return PartialView(CurrentUser);
        }

        [HttpPost]
        public ActionResult Logar(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            string queryConfereUser = "select * from PSNAL.dbo.tbl_user where login=@login and senha=@senha";
            Usuario user = Select(queryConfereUser, new Usuario()
            {
                login = usuario.login,
                senha = usuario.senha
            });

            if (usuario.login != user.login || usuario.senha != user.senha)
            {
                TempData["Warning"] = "Login ou Senha incorretos";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Login"] = usuario.login;
                Session["Senha"] = usuario.senha;
                return RedirectToAction("MainPage","Home");
            }
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(Usuario cadastroNovo)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Formulário inválido";
                return View("Cadastrar");
            }

            string queryVerificaLoginDisponivel = @"select * from PSNAL.dbo.tbl_user where login = @login";

            try
            {
                Usuario cadastro = Select(queryVerificaLoginDisponivel, new Usuario()
                {
                    login = cadastroNovo.login
                });

                if (cadastro.login == cadastroNovo.login)
                {
                    TempData["Warning"] = "Login não disponível";
                    return View("Cadastrar");
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "Error";
                return RedirectToAction("Cadastrar", "Login");
            }

            string queryInsereUsuario =
                @"insert into PSNAL.dbo.tbl_user(nome,login,senha,email) values(@nome,@login,@senha,@email)";
            Objects.Geral.Usuario usu = new Usuario()
            {
                nome = cadastroNovo.nome,
                login = cadastroNovo.login,
                senha = cadastroNovo.senha,
                email = cadastroNovo.email
            };
            IUD<Usuario>(queryInsereUsuario,usu);

            TempData["Success"] = "Usuário cadastrado com sucesso";
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Logout()
        {
            Session["Login"] = null;
            Session["Senha"] = null;
            return RedirectToAction("Index","Login");
        }
    }
}