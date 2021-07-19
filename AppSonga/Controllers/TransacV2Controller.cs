using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ApiSonga.Models;
using AppSonga.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Newtonsoft.Json;
namespace AppSonga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacV2Controller : ControllerBase
    {
        private readonly TransacV2Repository _repository;

        public TransacV2Controller(TransacV2Repository repository)
        {
            _repository = repository;
        }



        [HttpPost]
        public async Task<string> GetConsulta([FromBody] ParamModel ParamModel)
        {
            return await _repository.GetTransac(ParamModel);
        }
    }
}
