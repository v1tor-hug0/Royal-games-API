namespace RoyalGames.DTOs.UsuarioDto
{
    public class AtualizarUsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public string Senha { get; set; }

        public bool StatusUsuario { get; set; }
    }
}