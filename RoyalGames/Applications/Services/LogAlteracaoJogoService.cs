using RoyalGames.Domains;
using RoyalGames.DTOs.LogJogoDto;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class LogAlteracaoJogoService
    {
        private readonly ILogAlteracaoJogoRepository _repository;

        public LogAlteracaoJogoService(ILogAlteracaoJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogJogoDto> Listar()
        {
            List<Log_AlteracaoJogo> logs = _repository.Listar();

            List<LerLogJogoDto> listaLogJogo = logs.Select(l => new LerLogJogoDto
            {
                LogID = l.Log_AlteracaoJogoID,
                JogoID = l.JogoID,
                NomeAnterior = l.NomeAnterior,
                PrecoAnterior = l.PrecoAnterior,
                DataAlteracao = l.DataAlteracao
            }).ToList();

            return listaLogJogo;
        }

        public List<LerLogJogoDto> ListarPorJogo(int JogoId)
        {
            List<Log_AlteracaoJogo> logs = _repository.ListarPorJogo(JogoId);

            List<LerLogJogoDto> listaLogJogo = logs.Select(l => new LerLogJogoDto
            {
                LogID = l.Log_AlteracaoJogoID,
                JogoID = l.JogoID,
                NomeAnterior = l.NomeAnterior,
                PrecoAnterior = l.PrecoAnterior,
                DataAlteracao = l.DataAlteracao
            }).ToList();

            return listaLogJogo;
        }
    }
}