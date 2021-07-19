using AppSonga.Clases;
using AppSonga.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppSonga.Data
{
    public class QuerysRepository
    {
        private readonly string _connectionString;
        private IConfiguration _configuration;

        public string getConexion(string conn)
        {
            string conexion = "";
            switch (conn)
            {
                case "PRODUCCION":
                    conexion = _configuration.GetConnectionString("DefaultConnectionString");
                    break;
                case "RRHH":
                    conexion = _configuration.GetConnectionString("ConnectionStringRRHH");
                    break;
                case "SONG":
                    conexion = _configuration.GetConnectionString("ConnectionStringSONG");
                    break;
                case "PROYECTO":
                    conexion = _configuration.GetConnectionString("ConnectionStringPROYECTO");
                    break;
                case "DESAPRODUCCION":
                    conexion = _configuration.GetConnectionString("ConnectionStringDesaProduccion");
                    break;
                case "DESARRHH":
                    conexion = _configuration.GetConnectionString("ConnectionStringDesaRRHH");
                    break;
                case "DESSONG":
                    conexion = _configuration.GetConnectionString("ConnectionStringDesaSONG");
                    break;
                case "DESPROYECTO":
                    conexion = _configuration.GetConnectionString("ConnectionStringDesaPROYECTO");
                    break;
                case "PREPRO":
                    conexion = _configuration.GetConnectionString("ConnectionStringPrePro");
                    break;
                case "PRERRHH":
                    conexion = _configuration.GetConnectionString("ConnectionStringPreRRHH");
                    break;
                case "PRESONG":
                    conexion = _configuration.GetConnectionString("ConnectionStringPreSONG");
                    break;


            }
            return conexion;
        }
        public QuerysRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        public async Task<string> GetAll()
        {


            return "API WORKS";

        }


        public async Task<string> GetById(string Id)
        {
            string valor = "";
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_tallas_num", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //SqlDbType typeParam = (SqlDbType)Enum.Parse(typeof(SqlDbType),, true);
                    SqlParameter parameter = new SqlParameter("@Id", SqlDbType.VarChar);
                    parameter.Value = Id;
                    cmd.Parameters.Add(parameter);
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        DataTable de = new DataTable();
                        de.Load(reader);

                        valor = JsonConvert.SerializeObject(de, Formatting.Indented);
                    }

                    return valor;
                }
            }
        }


        public async Task<string> GetConsulta(Querys cons)
        {
            string valor = "";
            string result = "";
            Resultado rsl = new Resultado();

            try
            {



                string sp = cons.sp;
                string parameters = cons.parameters;
                string connection = cons.connection;
                SqlDbType typeParam;
                SqlParameter parameter;

                using (SqlConnection sql = new SqlConnection(getConexion(connection)))
                {
                    using (SqlCommand cmd = new SqlCommand(sp, sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        var split2 = parameters.ToString().Split('|');
                        if (split2.Count() > 0)
                        {
                            for (int i = 0; i < split2.Count(); i++)
                            {
                                if (split2[i].ToString().Trim() != "")
                                {
                                    var split3 = split2[i].ToString().Split(':');
                                    if (split3.Count() > 0)
                                    {
                                        typeParam = (SqlDbType)Enum.Parse(typeof(SqlDbType), split3[2].ToString(), true);
                                        //db.AddInParameter(sql, split3[0].ToString(), typeParam, split3[1].ToString());
                                        parameter = new SqlParameter(split3[0].ToString(), typeParam);
                                        parameter.Value = split3[1].ToString();
                                        cmd.Parameters.Add(parameter);
                                    }
                                }
                            }

                        }
                        await sql.OpenAsync();
                        SqlDataAdapter ss = new SqlDataAdapter(cmd);
                        DataSet de = new DataSet();
                        ss.Fill(de);
                        

                        valor = JsonConvert.SerializeObject(de, Formatting.Indented);

                        rsl.Mensaje = "OK";
                        rsl.Error = "False";
                        rsl.DT = de;

                        result = JsonConvert.SerializeObject(rsl);
                        //using (var reader = await cmd.ExecuteReaderAsync())
                        //{


                        //    DataSet de = new DataSet();
                        //    reader.fil

                        //    //de.Load(reader);
                        //    valor = JsonConvert.SerializeObject(de, Formatting.Indented);
                        //}


                    }
                }
            }
            catch (SqlException ex)
            {
                //de.Rows.Add("true", ex.Message.ToString());
                //valor = JsonConvert.SerializeObject(de, Formatting.Indented);
                //valor = JsonConvert.SerializeObject(ex.Message.ToString(), Formatting.Indented);
                rsl.Mensaje = ex.Message.ToString();
                rsl.Error = "True";
                rsl.DT = null;
                result = JsonConvert.SerializeObject(rsl);
            }

            return result;
        }
    }
}
