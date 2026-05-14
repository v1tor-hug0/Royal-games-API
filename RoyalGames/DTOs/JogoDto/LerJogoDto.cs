namespace RoyalGames.DTOs.Produto
{
    public class LerJogoDto
    {
        public int JogoID { get; set; }
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;
        public bool? StatusJogo { get; set; }

        // generos
        public List<int> GeneroIds { get; set; } = new();
        public List<string> Generos { get; set; } = new();

        // plataformas
        public List<int> PlataformaIds { get; set; } = new();
        public List<string> Plataformas { get; set; } = new();

        // usuario que cadastrou 
        public int? UsuarioID { get; set; }
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }

        // classificacao 
        public int? ClassificacaoID { get; set; }
        public string? ClassificacaoNome { get; set; }
    }
}
