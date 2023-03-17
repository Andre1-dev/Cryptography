using Cipra.Model;
using Criptografia.Application.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Criptografia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CritptografiaController : ControllerBase
    {
        private readonly ICryptService cryptService;
        public CritptografiaController(ICryptService _cryptService)
        {
            this.cryptService = _cryptService;
        }
        [HttpPost("cifrar")]
        public IActionResult Cifrar(Modelo model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            byte[] texto_cifrado = cryptService.Cifrar(model.Message, model.Key);
            return Ok(texto_cifrado);
        }
        [HttpPost("decifrar")]
        public IActionResult Decifrar(Modelo model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model)); ;
            }

            string texto_decifrado = cryptService.Decifrar(model.CyphMessage, model.Key);
            return Ok(texto_decifrado);
        }
    }
}
