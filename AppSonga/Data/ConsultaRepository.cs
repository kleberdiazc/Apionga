﻿using AppSonga.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlTypes;


namespace AppSonga.Data
{
    public class ConsultaRepository
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
        public ConsultaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        public async Task<string> GetAll()
        {
            //SqlDatabase db = new SqlDatabase(_connectionString);

            //DbCommand sql = db.GetStoredProcCommand("sp_tallas");
            ////SqlDbType typeParam = (SqlDbType)Enum.Parse(typeof(SqlDbType),"Int", true);
            ////db.AddInParameter(sql, "Tipo", typeParam, "0");

            //DataSet dt = db.ExecuteDataSet(sql);
            //string valor = JsonConvert.SerializeObject(dt, Formatting.Indented);

            //return valor;
            //string valor = "";
            //using (SqlConnection sql = new SqlConnection(_connectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("sp_menu_x_usuario", sql))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        //var response = new List<Tallas>();
            //        await sql.OpenAsync();

            //        using (var reader = await cmd.ExecuteReaderAsync())
            //        {
            //            DataTable de = new DataTable();
            //            de.Load(reader);

            //             valor = JsonConvert.SerializeObject(de, Formatting.Indented);
            //        }

            return  "API WORKS";
        
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


        public async Task<string> GetConsulta(Consulta cons)
        {
            string valor = "";
            try
            {

                //DataTable de = new DataTable();
                //DataColumn column = new DataColumn();
                //column.ColumnName = "ERROR";

                //de.Columns.Add(column);
                //column = new DataColumn();
                //column.ColumnName = "RESPUESTA";
                //de.Columns.Add(column);

                string sp = cons.sp;
                string parameters = cons.parameters;
                string connection = cons.connection;
                SqlDbType typeParam;
                SqlParameter parameter;
                //sp;Id:3:Int|Codigo:45:NVarchar|;
                //SqlDatabase db = new SqlDatabase(_connectionString);
                //DbCommand sql = db.GetStoredProcCommand(sp);

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
                                        if(split3[1].ToString() == "null")
                                        {
                                            parameter.Value = null;
                                        }
                                        else {
                                            parameter.Value = split3[1].ToString();
                                        }
                                       
                                        cmd.Parameters.Add(parameter);
                                    }
                                }
                            }

                        }
                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {


                            DataTable de = new DataTable();
                            de.Load(reader);
                            valor = JsonConvert.SerializeObject(de, Formatting.Indented);
                        }


                    }
                }
            }
            catch (SqlException ex)
            {
                valor = JsonConvert.SerializeObject(ex.Message.ToString(), Formatting.Indented);
            }

            return valor;
        }
    }
}
