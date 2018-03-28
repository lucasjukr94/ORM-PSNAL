using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Reflection;
using PSNAL.Infra;
using Objects.Geral;

namespace PSNAL.Controllers
{
    public class DBHelperController : Controller
    {
        Inf infra = new Inf();

        public string connectionstring()
        {
            return infra.connectionstring;
        }
        public bool verificaSession()
        {
            return infra.verificaSession;
        }
        
        public void IUD(string query,Dictionary<string,string> parameters)
        {
            SqlConnection sqlConn = new SqlConnection(connectionstring());
            SqlCommand sqlComm = new SqlCommand(query,sqlConn);

            sqlConn.Open();
            sqlComm.Parameters.Clear();
            foreach (var p in parameters)
            {
                sqlComm.Parameters.AddWithValue(p.Key, p.Value);
            }
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }
        public void IUD<T>(string query, T parameters) where T : new()
        {
            SqlConnection sqlConn = new SqlConnection(connectionstring());
            SqlCommand sqlComm = new SqlCommand(query, sqlConn);

            sqlConn.Open();
            sqlComm.Parameters.Clear();
            foreach (var p in parameters.GetType().GetProperties())
            {
                if (p.GetValue(parameters) != null)
                {
                    sqlComm.Parameters.AddWithValue(p.Name, p.GetValue(parameters));
                }
            }
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();
        }
        
        public T Select<T>(string query, T parametros) where T : new()
        {
            SqlConnection sqlConn = new SqlConnection(connectionstring());
            SqlCommand sqlComm = new SqlCommand(query, sqlConn);

            T objeto = new T();

            sqlConn.Open();
            sqlComm.Parameters.Clear();
            foreach (var p in parametros.GetType().GetProperties())
            {
                if (p.GetValue(parametros) != null)
                {
                    sqlComm.Parameters.AddWithValue(p.Name, p.GetValue(parametros));    
                }
            }
            sqlComm.ExecuteNonQuery();

            SqlDataReader reader = sqlComm.ExecuteReader();
            while (reader.Read())
            {
                foreach (PropertyInfo propertyInfo in objeto.GetType().GetProperties())
                {
                    try
                    {
                        propertyInfo.SetValue(objeto, int.Parse(reader[propertyInfo.Name].ToString()));
                    }
                    catch (Exception)
                    {
                        try
                        {
                            propertyInfo.SetValue(objeto, DateTime.Parse(reader[propertyInfo.Name].ToString()));
                        }
                        catch (Exception)
                        {
                            try
                            {
                                propertyInfo.SetValue(objeto, decimal.Parse(reader[propertyInfo.Name].ToString()));
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    propertyInfo.SetValue(objeto, double.Parse(reader[propertyInfo.Name].ToString()));
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        propertyInfo.SetValue(objeto, long.Parse(reader[propertyInfo.Name].ToString()));
                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            propertyInfo.SetValue(objeto, reader[propertyInfo.Name].ToString());
                                        }
                                        catch (Exception)
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            sqlConn.Close();

            return objeto;
        }
        public T select<T>(string query,Dictionary<string,string> parametros) where T : new()
        {
            SqlConnection sqlConn = new SqlConnection(connectionstring());
            SqlCommand sqlComm = new SqlCommand(query,sqlConn);

            T objeto = new T();

            sqlConn.Open();
            sqlComm.Parameters.Clear();
            foreach (var p in parametros)
            {
                sqlComm.Parameters.AddWithValue(p.Key, p.Value);
            }
            sqlComm.ExecuteNonQuery();

            SqlDataReader reader = sqlComm.ExecuteReader();
            while (reader.Read())
            {
                foreach (PropertyInfo propertyInfo in objeto.GetType().GetProperties())
                {
                    try
                    {
                        propertyInfo.SetValue(objeto, int.Parse(reader[propertyInfo.Name].ToString()));
                    }
                    catch (Exception)
                    {
                        try
                        {
                            propertyInfo.SetValue(objeto, DateTime.Parse(reader[propertyInfo.Name].ToString()));
                        }
                        catch (Exception)
                        {
                            try
                            {
                                propertyInfo.SetValue(objeto, decimal.Parse(reader[propertyInfo.Name].ToString()));
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    propertyInfo.SetValue(objeto, double.Parse(reader[propertyInfo.Name].ToString()));
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        propertyInfo.SetValue(objeto, long.Parse(reader[propertyInfo.Name].ToString()));
                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            propertyInfo.SetValue(objeto, reader[propertyInfo.Name].ToString());
                                        }
                                        catch (Exception)
                                        {

                                        }
                                    }
                                }
                            }
                        }   
                    }
                }
            }
            reader.Close();
            sqlConn.Close();

            return objeto;
        }

        public List<T> Selectlist<T>(string query, T parametros) where T : new()
        {
            SqlConnection sqlConn = new SqlConnection(connectionstring());
            SqlCommand sqlComm = new SqlCommand(query, sqlConn);

            List<T> listobjeto = new List<T>();

            sqlConn.Open();
            sqlComm.Parameters.Clear();
            foreach (var p in parametros.GetType().GetProperties())
            {
                if (p.GetValue(parametros) != null)
                {
                    sqlComm.Parameters.AddWithValue(p.Name, p.GetValue(parametros));
                }
            }
            sqlComm.ExecuteNonQuery();

            SqlDataReader reader = sqlComm.ExecuteReader();
            while (reader.Read())
            {
                T objeto = new T();
                foreach (PropertyInfo propertyInfo in objeto.GetType().GetProperties())
                {
                    try
                    {
                        propertyInfo.SetValue(objeto, int.Parse(reader[propertyInfo.Name].ToString()));
                    }
                    catch (Exception)
                    {
                        try
                        {
                            propertyInfo.SetValue(objeto, DateTime.Parse(reader[propertyInfo.Name].ToString()));
                        }
                        catch (Exception)
                        {
                            try
                            {
                                propertyInfo.SetValue(objeto, decimal.Parse(reader[propertyInfo.Name].ToString()));
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    propertyInfo.SetValue(objeto, double.Parse(reader[propertyInfo.Name].ToString()));
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        propertyInfo.SetValue(objeto, long.Parse(reader[propertyInfo.Name].ToString()));
                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            propertyInfo.SetValue(objeto, reader[propertyInfo.Name].ToString());
                                        }
                                        catch (Exception)
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                listobjeto.Add(objeto);
            }
            reader.Close();
            sqlConn.Close();

            return listobjeto;
        }
        public List<T> selectlist<T>(string query, Dictionary<string, string> parametros) where T : new()
        {
            SqlConnection sqlConn = new SqlConnection(connectionstring());
            SqlCommand sqlComm = new SqlCommand(query, sqlConn);

            List<T> listobjeto = new List<T>();

            sqlConn.Open();
            sqlComm.Parameters.Clear();
            foreach (var p in parametros)
            {
                sqlComm.Parameters.AddWithValue(p.Key, p.Value);
            }
            sqlComm.ExecuteNonQuery();

            SqlDataReader reader = sqlComm.ExecuteReader();
            while (reader.Read())
            {
                T objeto = new T();
                foreach (PropertyInfo propertyInfo in objeto.GetType().GetProperties())
                {
                    try
                    {
                        propertyInfo.SetValue(objeto, int.Parse(reader[propertyInfo.Name].ToString()));
                    }
                    catch (Exception)
                    {
                        try
                        {
                            propertyInfo.SetValue(objeto, DateTime.Parse(reader[propertyInfo.Name].ToString()));
                        }
                        catch (Exception)
                        {
                            try
                            {
                                propertyInfo.SetValue(objeto, decimal.Parse(reader[propertyInfo.Name].ToString()));
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    propertyInfo.SetValue(objeto, double.Parse(reader[propertyInfo.Name].ToString()));
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        propertyInfo.SetValue(objeto, long.Parse(reader[propertyInfo.Name].ToString()));
                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            propertyInfo.SetValue(objeto, reader[propertyInfo.Name].ToString());
                                        }
                                        catch (Exception)
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                listobjeto.Add(objeto);
            }
            reader.Close();
            sqlConn.Close();

            return listobjeto;
        }
    }
}