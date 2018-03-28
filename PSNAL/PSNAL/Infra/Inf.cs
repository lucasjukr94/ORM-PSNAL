using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Web.Http.Results;

namespace PSNAL.Infra
{
    public class Inf : System.Web.UI.Page
    {
        public string connectionstring
        {
            get
            {
                return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["PSNAL"].ConnectionString;
            }
        }

        public bool verificaSession
        {
            get
            {
                SqlConnection sqlConn = new SqlConnection(connectionstring);
                string queryBuscaLogin = @"select count(*) as n from PSNAL.dbo.tbl_user where login = @login and senha = @senha";
                SqlCommand sqlComm = new SqlCommand(queryBuscaLogin,sqlConn);
                int n = 0;
                try
                {
                    sqlConn.Open();
                    sqlComm.Parameters.Clear();
                    sqlComm.Parameters.AddWithValue("@login", Session["Login"].ToString());
                    sqlComm.Parameters.AddWithValue("@senha", Session["Senha"].ToString());
                    sqlComm.ExecuteNonQuery();

                    SqlDataReader reader = sqlComm.ExecuteReader();
                    while (reader.Read())
                    {
                        n = int.Parse(reader["n"].ToString());
                        reader.NextResult();
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    return true;
                }
                finally
                {
                    sqlConn.Close();
                }
                return ((Session["Login"] != null && Session["Senha"] != null)||n==1) ? false : true;
            }
        }
    }
}