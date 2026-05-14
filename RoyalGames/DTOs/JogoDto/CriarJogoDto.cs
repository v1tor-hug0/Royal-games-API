namespace RoyalGames.DTOs.Jogo
{
    public class CriarJogoDto
    {
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public IFormFile Imagem { get; set; } = null!;
        public List<int> GeneroIds { get; set; } = new();
        public List<int> PlataformaIds { get; set; } = new();
        public int ClassificacaoID { get; set; }
    }
}
