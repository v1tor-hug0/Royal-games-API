using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly Royal_GamesContext _context;

        public GeneroRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Genero> Listar()
        {
            return _context.Genero.ToList();
        }

        public Genero ObterPorId(int id)
        {
            Genero Genero = _context.Genero.FirstOrDefault(c => c.GeneroID == id);

            return Genero;
        }

        public bool NomeExiste(string nome, int? GeneroIdAtual = null)
        {
            var consulta = _context.Genero.AsQueryable();

            if (GeneroIdAtual.HasValue)
            {
                consulta = consulta.Where(Genero => Genero.GeneroID != GeneroIdAtual.Value);
            }

            return consulta.Any(c => c.Nome == nome);
        }

        public void Adicionar(Genero Genero)
        {
            _context.Genero.Add(Genero);
            _context.SaveChanges();
        }

        public void Atualizar(Genero Genero)
        {
            Genero GeneroBanco = _context.Genero.FirstOrDefault(c => c.GeneroID == Genero.GeneroID);

            if (GeneroBanco == null)
            {
                return;
            }

            GeneroBanco.Nome = Genero.Nome;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Genero GeneroBanco = _context.Genero.FirstOrDefault(c => c.GeneroID == id);

            if (GeneroBanco == null)
            {
                return;
            }

            _context.Genero.Remove(GeneroBanco);
            _context.SaveChanges();
        }
    }
}