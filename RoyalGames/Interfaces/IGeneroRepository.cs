using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IGeneroRepository
    {
        List<Genero> Listar();

        Genero ObterPorId(int id);

        bool NomeExiste(string nome, int? GeneroIdAtual = null);

        void Adicionar(Genero Genero);

        void Atualizar(Genero Genero);

        void Remover(int id);
    }
}