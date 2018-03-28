using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.SqlClient;
using PSNAL.Infra;
using Objects.Geral;

namespace PSNAL.Controllers
{
    public class BaseController : DBHelperController
    {
        public void verificaUser()
        {
            if (verificaSession())
            {
                Response.Redirect("~/Login/Index");
            }
        }

        public Usuario CurrentUser
        {
            get
            {
                string queryBuscaUser = @"select * from PSNAL.dbo.tbl_user where login = @login and senha = @senha";
                Usuario user = new Usuario();
                try
                {
                    user = Select(queryBuscaUser, new Usuario()
                    {
                        login = Session["Login"].ToString(),
                        senha = Session["Senha"].ToString()
                    });
                }
                catch (Exception)
                {

                }
                return user;
            }
        }
    }
}
