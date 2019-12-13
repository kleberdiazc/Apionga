using AppSonga.Models;
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
    public class TallasRepository
    {
        private readonly string _connectionString;

        public TallasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        public string GetAll()
        {
            SqlDatabase db = new SqlDatabase(_connectionString);

            DbCommand sql = db.GetStoredProcCommand("sp_tallas");
            //SqlDbType typeParam = (SqlDbType)Enum.Parse(typeof(SqlDbType),"Int", true);
            //db.AddInParameter(sql, "Tipo", typeParam, "0");

            DataSet dt = db.ExecuteDataSet(sql);
            string valor = JsonConvert.SerializeObject(dt, Formatting.Indented);

            return valor;

            //using (SqlConnection sql = new SqlConnection(_connectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("sp_tallas", sql))
            //    {
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        var response = new List<Tallas>();
            //        await sql.OpenAsync();

            //        using (var reader = await cmd.ExecuteReaderAsync())
            //        {
            //            DataTable de = new DataTable();
            //            de.Load(reader);

            //            while (await reader.ReadAsync())
            //            {
            //                response.Add(MapToValue(reader));
            //            }
            //        }

            //        return response;
            //    }
            //}
        }

        public string GetConsulta(string consulta)
        {
            SqlDatabase db = new SqlDatabase(_connectionString);
            DbCommand sql = db.GetStoredProcCommand(consulta);

            //SqlDbType typeParam = (SqlDbType)Enum.Parse(typeof(SqlDbType), "Int", true);
            //db.AddInParameter(sql, "Tipo", typeParam, "0");

            DataSet dt = db.ExecuteDataSet(sql);
            string valor = JsonConvert.SerializeObject(dt, Formatting.Indented);

            return valor;
        }
    }
}
