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
using System.Text;
using System.Net;
using System.IO;

using System.Xml;
using AppSonga.Clases;

namespace AppSonga.Data
{
    public class TransacImagenRepository
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
        public TransacImagenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }


        public async Task<string> GetTransac([FromForm] string param, [FromForm] string paramImagen)
        {
            ParamModel param2 = JsonConvert.DeserializeObject<ParamModel>(param.ToString());
            ParamModelImagen paramImagen2 = JsonConvert.DeserializeObject<ParamModelImagen>(paramImagen.ToString());
            ///
            string connection = param2.conexion;
            Result Obj = new Result();
            SqlDbType type;
            List<Parameters> ListParam = null;
            ListParam = param2.param;

            String Store = param2.sp;
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
                            if (item.Value.ToString() == "null")
                            {
                                type = (SqlDbType)Enum.Parse(typeof(SqlDbType), item.Type, true);
                                db.AddInParameter(Sql, item.Name, type, null);
                            }
                            else
                            {
                                type = (SqlDbType)Enum.Parse(typeof(SqlDbType), item.Type, true);
                                db.AddInParameter(Sql, item.Name, type, item.Value);
                            }
                        }
                    }

                    if (paramImagen2.param != null)
                    {
                        foreach (ParametersImagen item in paramImagen2.param)
                        {
                            type = (SqlDbType)Enum.Parse(typeof(SqlDbType), item.Type, true);
                            db.AddInParameter(Sql, item.Name, type, item.Value);
                        }
                    }

                    Sql.CommandTimeout = 0;
                    //bool h  = db.SupportsAsync;

                    dt = db.ExecuteDataSet(Sql);
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
            return JsonConvert.SerializeObject(Obj);
            //return new JsonResult(JsonConvert.SerializeObject(Obj));
        }

        public async Task<string> GetRecono([FromForm] string param, [FromForm] string paramImagen)
        {
            //this.infoPopup.ShowOnPageLoad = false;
            try
            {
                string paramImagen2 = JsonConvert.DeserializeObject<string>(paramImagen.ToString());
                System.Net.HttpWebRequest request = null;
                System.Net.HttpWebResponse response = null;
                System.IO.Stream requestStream = null;
                byte[] bytes = null;
                string resultado = "";

                byte[] data = System.Convert.FromBase64String(paramImagen2.ToString());

                string url = "https://api.luxand.cloud/photo/search";


                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;


                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.DefaultRequestHeaders.Add("token", "f579e24b9b094e4a9eaabf6c5477bd1a");
                Stream fileStream = new MemoryStream(data);
                System.Net.Http.HttpContent fileStreamContent = new System.Net.Http.StreamContent(fileStream);

                using (var formData = new System.Net.Http.MultipartFormDataContent())
                {
                    formData.Add(fileStreamContent, "photo", "file1");
                    var response2 = await client.PostAsync(url, formData);
                    if (!response2.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    return await response2.Content.ReadAsStringAsync();
                }


                //bytes = System.Text.Encoding.ASCII.GetBytes("photo=" + data);
                //request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

                //request.Method = "POST";
                //request.Headers.Add("token", "f579e24b9b094e4a9eaabf6c5477bd1a");
                
                //request.ContentLength = bytes.Length;
                //request.ContentType = "multipart/form-data";

                

                //requestStream = request.GetRequestStream();
                //requestStream.Write(bytes, 0, bytes.Length);
                //requestStream.Close();

                //request.Timeout = 15000;

                //response = (System.Net.HttpWebResponse)request.GetResponse();

                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    System.IO.Stream responseStream = response.GetResponseStream();
                //    System.IO.StreamReader reader = new System.IO.StreamReader(responseStream);
                //    resultado = reader.ReadToEnd();
                //    return resultado;
                //}



            }
            catch (Exception ex)
            {
                return "ERROR";
            }

            return "ERROR";
        }



    }
}
