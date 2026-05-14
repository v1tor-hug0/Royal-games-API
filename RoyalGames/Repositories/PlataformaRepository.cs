using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoyalGames.Interfaces;
using RoyalGames.Domains;
using RoyalGames.Contexts;
using RoyalGames.DTOs.PlataformaDto;

namespace RoyalGames.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly Royal_GamesContext _context;

        public PlataformaRepository(Royal_GamesContext context)
        {
            _context = context;
        }

        public List<Plataforma> Listar()
        {
            return _context.Plataforma.ToList();
        }

        public Plataforma ObterPorId(int id)
        {
            Plataforma plataforma = _context.Plataforma.FirstOrDefault(p => p.PlataformaID == id);
            return plataforma;
        }

        public bool NomeExiste(string nome, int? PlataformaIdAtual = null)
        {
            var consulta = _context.Plataforma.AsQueryable();

            if (PlataformaIdAtual.HasValue)
            {
                consulta = consulta.Where(p => p.PlataformaID != PlataformaIdAtual.Value);
            }
            return consulta.Any(c => c.Nome == nome);
        }

        public void Adicionar(Plataforma plataforma)
        {
            _context.Plataforma.Add(plataforma);
            _context.SaveChanges();
        }

        public void Atualizar (Plataforma plataforma)
        {
            Plataforma plataformaBanco = _context.Plataforma.FirstOrDefault(p => p.PlataformaID == plataforma.PlataformaID);

            if (plataformaBanco == null)
            {
                return;
            }

            plataformaBanco.Nome = plataforma.Nome;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Plataforma plataformaBanco = _context.Plataforma.FirstOrDefault(p => p.PlataformaID == id);

            if (plataformaBanco == null)
            {
                return;
            }

            _context.Plataforma.Remove(plataformaBanco);
            _context.SaveChanges();
        }

    }
}
