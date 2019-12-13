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

namespace AppSonga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TallasController : ControllerBase
    {
        private readonly TallasRepository _repository;

        public TallasController(TallasRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Tallas
        [HttpGet]
        public string Get()
        {
            return _repository.GetAll();
        }

        // GET: api/Tallas/5
        [HttpPost]
        public string GetConsultas()
        {
            return _repository.GetConsulta("sp_tallas");
        }
    


    }
}
