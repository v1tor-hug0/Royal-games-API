using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();
        Jogo ObterPorId(int id);
        byte[] ObterImagem(int id);
        bool NomeExiste(string nome, int? idJogoIdAtual = null); 
        void Adicionar(Jogo jogo, List<int> GeneroIds, List<int> PlataformaIds);
        void Atualizar(Jogo jogo, List<int> GeneroIds, List<int> PlataformaIds);
        void Remover(int id);
    }
}
