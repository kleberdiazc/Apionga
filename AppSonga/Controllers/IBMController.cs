using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppSonga.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppSonga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IBMController : ControllerBase
    {
        private readonly IBMRepository  _repository;

        public IBMController(IBMRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<string> Upload([FromForm] string param)
        {
            return await _repository.ConexionIBM(param);
        }
    }
}
