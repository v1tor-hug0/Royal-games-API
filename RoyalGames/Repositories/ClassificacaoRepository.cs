using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class ClassificacaoRepository : IClassificacaoRepository
    {
        private readonly Royal_GamesContext _context;

        public ClassificacaoRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<ClassificacaoIndicativa> Listar()
        {
            return _context.ClassificacaoIndicativa.ToList();
        }

        public ClassificacaoIndicativa ObterPorId(int id)
        {
            ClassificacaoIndicativa classificacao = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoIndicativaID == id);

            return classificacao;
        }

        public bool NomeExiste(string nome, int? ClassificacaoIdAtual = null)
        {
            var consulta = _context.ClassificacaoIndicativa.AsQueryable();

            if (ClassificacaoIdAtual.HasValue)
            {
                consulta = consulta.Where(Classificacao => Classificacao.ClassificacaoIndicativaID != ClassificacaoIdAtual.Value);
            }

            return consulta.Any(c => c.Classificacao == nome);
        }

        public void Adicionar(ClassificacaoIndicativa Classificacao)
        {
            _context.ClassificacaoIndicativa.Add(Classificacao);
            _context.SaveChanges();
        }

        public void Atualizar(ClassificacaoIndicativa Classificacao)
        {
            ClassificacaoIndicativa ClassificacaoBanco = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoIndicativaID == Classificacao.ClassificacaoIndicativaID);

            if (ClassificacaoBanco == null)
            {
                return;
            }

            ClassificacaoBanco.Classificacao = Classificacao.Classificacao;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            ClassificacaoIndicativa ClassificacaoBanco = _context.ClassificacaoIndicativa.FirstOrDefault(c => c.ClassificacaoIndicativaID == id);

            if (ClassificacaoBanco == null)
            {
                return;
            }

            _context.ClassificacaoIndicativa.Remove(ClassificacaoBanco);
            _context.SaveChanges();
        }
    }
}