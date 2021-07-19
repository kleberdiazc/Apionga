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
    public class TransacV2Repository
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
        public TransacV2Repository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        public async Task<string> GetTransac([FromBody] ParamModel param)
        {
            Result rs = new Result();
            SqlParameter paramsql;
            try
            {
                using (SqlConnection sql = new SqlConnection(getConexion(param.conexion)))
                {
                    if (sql.State != ConnectionState.Open)
                    {
                        sql.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand(param.sp, sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (param.param != null)
                        {
                            foreach (Parameters item in param.param)
                            {
                                paramsql = new SqlParameter(item.Name, item.Type);

                                if (item.Value == "NULL")
                                {
                                    paramsql.Value = null;
                                }
                                else
                                {
                                    paramsql.Value = item.Value;
                                }
                                cmd.Parameters.Add(paramsql);
                            }
                        }

                        cmd.ExecuteNonQuery();
                        rs.Codigo = true;
                        rs.Description = "";
                        rs.Dt = null;
                    }
                }
            }
            catch (Exception ex)
            {
                rs.Codigo = false;
                rs.Description = ex.Message.ToString();
            }
            return JsonConvert.SerializeObject(rs);
        }


    }
}
