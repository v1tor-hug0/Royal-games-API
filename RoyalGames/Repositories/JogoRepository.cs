using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly Royal_GamesContext _context;

        public JogoRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
            List<Jogo> jogos = _context.Jogo.Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Plataforma)
                .Include(jogo => jogo.Usuario)
                .Include(jogo => jogo.ClassificacaoIndicativa)
                .ToList();

            return jogos;
        }

        public Jogo ObterPorId(int id)
        {
            Jogo? jogo = _context.Jogo
                .Include(jogoDb => jogoDb.Genero)
                .Include(jogoDb => jogoDb.Plataforma)
                .Include(jogoDb => jogoDb.Usuario)
                .Include(jogo => jogo.ClassificacaoIndicativa)
                .FirstOrDefault(jogoDb => jogoDb.JogoID == id);

            return jogo;
        }

        public bool NomeExiste(string nome, int? jogoIdAtual = null)
        {
            var jogoConsultado = _context.Jogo.AsQueryable();

            if(jogoIdAtual.HasValue)
            {
                jogoConsultado = jogoConsultado.Where(jogo => jogo.JogoID != jogoIdAtual.Value);
            }

            return jogoConsultado.Any(jogo => jogo.Nome == nome);
        }

        public byte[] ObterImagem(int id)
        {
            var jogo = _context.Jogo
                .Where(jogo => jogo.JogoID == id)
                .Select(jogo => jogo.Imagem)
                .FirstOrDefault();

            return jogo;
        }

        public void Adicionar(Jogo jogo, List<int> GeneroIds, List<int> PlataformaIds)
        {
            List<Genero> generos = _context.Genero
                .Where(genero => GeneroIds.Contains(genero.GeneroID))
                .ToList();

            jogo.Genero = generos;

            List<Plataforma> plataformas = _context.Plataforma
                .Where(plataforma => PlataformaIds.Contains(plataforma.PlataformaID))
                .ToList();

            jogo.Plataforma = plataformas;

            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        // se der algo errado, provavelmente e aqui
        public void Atualizar(Jogo jogo, List<int> GeneroIds, List<int> PlataformaIds)
        {
            Jogo? jogoBanco = _context.Jogo
                .Include(jogo => jogo.Genero)
                .Include(jogo => jogo.Plataforma)
                .FirstOrDefault(j => j.JogoID == jogo.JogoID);

            if (jogoBanco == null)
            {
                return;
            }

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Descricao = jogo.Descricao;

            if (jogo.Imagem != null && jogo.Imagem.Length > 0)
            {
                jogoBanco.Imagem = jogo.Imagem;
            }

            if(jogo.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogo.StatusJogo;
            }

            var generos = _context.Genero
                .Where(genero => GeneroIds.Contains(genero.GeneroID))
                .ToList();

            jogoBanco.Genero.Clear();

            foreach (var genero in generos)
            {
                jogoBanco.Genero.Add(genero);
            }

            var plataformas = _context.Plataforma
                .Where(plataforma => PlataformaIds.Contains(plataforma.PlataformaID))
                .ToList();

            jogoBanco.Plataforma.Clear();

            foreach (var plataforma in plataformas)
            {
                jogoBanco.Plataforma.Add(plataforma);
            }

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Jogo? jogo = _context.Jogo.FirstOrDefault(jogo => jogo.JogoID == id);

            if(jogo == null)
            {
                return;
            }

            _context.Jogo.Remove(jogo);
            _context.SaveChanges();
        }
    }
}

