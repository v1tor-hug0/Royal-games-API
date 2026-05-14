using RoyalGames.Exceptions;

namespace RoyalGames.Applications.Regras
{
    public class ValidarDataExpiracao
    {
        public static void ValidarDataExpericao(DateTime dataExpiracao)
        {
            if (dataExpiracao <= DateTime.Now)
            {
                throw new DomainException("Data de expiração deve ser futura.");
            }
        }
    }
}
