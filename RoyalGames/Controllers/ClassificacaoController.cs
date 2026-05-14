using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Applications.Services;
using RoyalGames.DTOs.ClassificacaoDto;
using RoyalGames.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificacaoController : ControllerBase
    {
        private readonly ClassificacaoService _service;

        public ClassificacaoController(ClassificacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            List<LerClassificacaoDto> classificacoes = _service.Listar();

            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public ActionResult ObterPorId(int id)
        {
            LerClassificacaoDto? classificacao = _service.ObterPorId(id);

            if (classificacao == null)
            {
                return NotFound();
            }

            return Ok(classificacao);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarClassificacaoDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);

                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Atualizar(int id, CriarClassificacaoDto criarDto)
        {
            try
            {
                _service.Atualizar(id, criarDto);

                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);

                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
