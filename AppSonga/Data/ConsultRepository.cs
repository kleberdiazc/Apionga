using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Newtonsoft.Json;
using ApiSonga;
using ApiSonga.Clases;
using ApiSonga.Models;

namespace AppSonga.Data
{
    public class ConsultRepository
    {
        private readonly string _connectionString;
        private IConfiguration _configuration;
        private static object lockObject = new object();
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
        public ConsultRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }



        public async Task<string> GetConsulta(ParamModel paramModel)
        {
            string connection = paramModel.conexion;
            Result Obj = new Result();
            SqlDbType type;
            List<Parameters> ListParam = null;
            ListParam = paramModel.param;

            String Store = paramModel.sp;
            string errorMessages = "";
            DataSet dt = null;
            //string Cadena = "Data Source=SVRSNG04;Initial Catalog=PRODUCCION;User ID=sareportes;pwd=";// ObtenerConexion(Conn);
            SqlDatabase db = new SqlDatabase(getConexion(connection));
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                try
                {
                    DbCommand Sql = db.GetStoredProcCommand(Store);


                    if (ListParam != null)
                    {
                        foreach (Parameters item in ListParam)
                        {
                            type = (SqlDbType)Enum.Parse(typeof(SqlDbType), item.Type, true);
                            db.AddInParameter(Sql, item.Name, type, item.Value);
                        }
                    }

                    Sql.CommandTimeout = 0;
                    //bool h  = db.SupportsAsync;
                    
                    dt =  db.ExecuteDataSet(Sql);
                    Obj.Codigo = true;
                    Obj.Description = "";
                    Obj.Dt = dt;

                    conn.Close();
                }
                catch (SqlException e)
                {
                    Obj.Codigo = false;

                    for (int i = 0; i < e.Errors.Count; i++)
                    {
                        errorMessages = "Message: " + e.Errors[0].Message;
                        if (e.Errors[i].Procedure != "")
                        {
                            errorMessages = errorMessages + " Procedure: " + e.Errors[i].Procedure;
                        }
                    }

                    Obj.Description = errorMessages.ToString();

                    Obj.Dt = null;
                    conn.Close();


                }
            }
             return  JsonConvert.SerializeObject(Obj);
            //return new JsonResult(JsonConvert.SerializeObject(Obj));
        }
    }
}
