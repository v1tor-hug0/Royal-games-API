using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.Domains;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogJogoController : ControllerBase
    {
        private readonly LogAlteracaoJogoService _service;

        public LogJogoController(LogAlteracaoJogoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("Jogo/{id}")]
        public ActionResult ListarPorJogo(int id)
        {
            return Ok(_service.ListarPorJogo(id));
        }
    }
}
