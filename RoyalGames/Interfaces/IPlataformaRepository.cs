using RoyalGames.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalGames.Interfaces
{
    public interface IPlataformaRepository
    {
        List<Plataforma> Listar();
        Plataforma ObterPorId(int id);

        bool NomeExiste(string nome, int? PlataformaIdAtual = null);

        void Adicionar(Plataforma categoria);
        void Atualizar(Plataforma categoria);
        void Remover(int id);
    }
}
