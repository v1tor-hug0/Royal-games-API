using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class LogAlteracaoJogoRepository : ILogAlteracaoJogoRepository
    {
        private readonly Royal_GamesContext _context;

        public LogAlteracaoJogoRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Log_AlteracaoJogo> Listar()
        {
            List<Log_AlteracaoJogo> log = _context.Log_AlteracaoJogo
                .OrderByDescending(l => l.DataAlteracao)
                .ToList();

            return log;
        }

        public List<Log_AlteracaoJogo> ListarPorJogo(int JogoId)
        {
            List<Log_AlteracaoJogo> alterecoesJogo = _context.Log_AlteracaoJogo
                .Where(l => l.JogoID == JogoId)
                .OrderByDescending(l => l.DataAlteracao)
                .ToList();

            return alterecoesJogo;
        }
    }
}