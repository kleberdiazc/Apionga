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
using ApiSonga.Models;

namespace AppSonga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacImagenController : ControllerBase
    {
        private readonly TransacImagenRepository _repository;

        public TransacImagenController(TransacImagenRepository repository)
        {
            _repository = repository;
        }



        [HttpPost]
        public async Task<string> GetConsulta([FromForm] string param, [FromForm] string paramImagen)
        {
            return await _repository.GetTransac(param,paramImagen);
        }

        [HttpPost("Recon")]
        public async Task<string> GetRecon([FromForm] string param, [FromForm] string paramImagen)
        {
            return await _repository.GetRecono(param, paramImagen);
        }

    }
}
