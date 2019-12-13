using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppSonga.Context;
using AppSonga.Models;
using AppSonga.Data;
using Newtonsoft.Json;

namespace AppSonga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly ConsultaRepository _repository;

        public ConsultasController(ConsultaRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Consultas
        [HttpGet]
        public Task<string> Get()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}")]
        public Task<string> Get(string id)
        {
            return _repository.GetById(id);
        }

        [HttpPost]
        //[Consumes("application/x-www-form-urlencoded")]
        public Task<string> GetConsultas([FromBody] Consulta consultas)
        { 
            return _repository.GetConsulta(consultas);
        }

    }
}
