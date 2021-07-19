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
using AppSonga.Models;
using AppSonga.Clases;

namespace AppSonga.Data
{
    public class IBMRepository
    {
        private readonly string _connectionString;
        private IConfiguration _configuration;
        private static object lockObject = new object();
        ConexionIBM conIbm = new ConexionIBM();
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
        public IBMRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        public async Task<string> ConexionIBM([FromForm] string param)
        {
            try
            {
                IBMModel param2 = JsonConvert.DeserializeObject<IBMModel>(param.ToString());
                string xml = param2.xml;
                string nombre = param2.nombre;
                Result Obj = new Result();
                HttpWebRequest request = null;
                HttpWebResponse response = null;
                Stream requestStream = null;
                byte[] bytes = null;
                string resultado = "";

                string url = conIbm.URLaccPrd;

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                bytes = Encoding.ASCII.GetBytes("grant_type=urn%3Aibm%3Aparams%3Aoauth%3Agrant-type%3Aapikey&apikey=" + conIbm.apiKeyPrd + "");
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.Accept = "*/*";

                request.ContentType = "application/x-www-form-urlencoded";

                requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    resultado = reader.ReadToEnd();
                    conexionServices(resultado, xml, nombre);
                    Obj.Codigo = true;
                    Obj.Description = "";
                    return JsonConvert.SerializeObject(Obj); ;

                }

                return "false";


                //second

            }
            catch (Exception ex)
            {
                return "false";
                throw;
            }
        }

        protected void conexionServices(string service, string xml, string nombre)
        {
            try
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;
                Stream requestStream = null;
                byte[] bytes = null;
                string resultado = "";
                string url2 = conIbm.URLserPrd;
                bytes = Encoding.ASCII.GetBytes(service);
                request = (HttpWebRequest)WebRequest.Create(url2);

                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.Accept = "*/*";

                request.ContentType = "application/json";

                requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    resultado = reader.ReadToEnd();
                    object[] resul = resultado.Replace("{", "").Replace("}", "").Replace("\"", "").Split(',');
                    string token_resul = resul[0].ToString();
                    object[] tok = token_resul.Split(':');
                    string token = tok[1].ToString();
                    enviar_XML(token, xml, nombre);
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void enviar_XML(string token, string xml, string nombre)
        {
            //this.infoPopup.ShowOnPageLoad = false;
            try
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;
                Stream requestStream = null;
                byte[] bytes = null;
                string resultado = "";

                string url = conIbm.URLxmlPrd;
                //string url = "https://sandbox.food.ibm.com/ift/api/connector/fs/connector/v1/assets";
                //string url = conIbm.URLxmlDess;

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                bytes = Encoding.ASCII.GetBytes(xml);
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";

                request.Headers.Add("Authorization", "Bearer " + token + "");
                request.ContentLength = bytes.Length;
                request.Accept = "*/*";

                request.ContentType = "application/xml";

                requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    //List<string> lista = Session["Lista"]. ;

                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    resultado = reader.ReadToEnd();

                    //ShowMessage("Exitoso: Generado con Exito el XML : " +nombre, MessageType.Error);

                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
