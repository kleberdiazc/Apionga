using AppSonga.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppSonga.Data;

namespace AppSonga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly QuerysRepository _repository;

        public QueryController(QuerysRepository repository)
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
        public Task<string> GetConsultas([FromBody] Querys consultas)
        {
            return _repository.GetConsulta(consultas);
        }

    }
}
