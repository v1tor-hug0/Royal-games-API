using RoyalGames.Applications.Regras;
using RoyalGames.Exceptions;
using RoyalGames.Applications.Regras;
using RoyalGames.Domains;
using RoyalGames.DTOs.GeneroDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class GeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public List<LerGeneroDto> Listar()
        {
            List<Genero> Generos = _repository.Listar();

            // Converte cada Genero para LerGeneroDto
            List<LerGeneroDto> GeneroDto = Generos.Select(Genero => new LerGeneroDto
            {
                GeneroID = Genero.GeneroID,
                Nome = Genero.Nome
            }).ToList();

            // Retorna a lista já convertida em DTO
            return GeneroDto;
        }

        public LerGeneroDto ObterPorId(int id)
        {
            Genero? Genero = _repository.ObterPorId(id);

            if (Genero == null)
            {
                throw new DomainException("Genero não encontrada.");
            }

            LerGeneroDto GeneroDto = new LerGeneroDto
            {
                GeneroID = Genero.GeneroID,
                Nome = Genero.Nome
            };

            return GeneroDto;
        }

        public void Adicionar(CriarGeneroDto criarDto)
        {
            ValidacaoNome.ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Genero já existente.");
            }

            Genero Genero = new Genero
            {
                Nome = criarDto.Nome
            };

            _repository.Adicionar(Genero);
        }

        public void Atualizar(int id, CriarGeneroDto criarDto)
        {
            ValidacaoNome.ValidarNome(criarDto.Nome);

            Genero GeneroBanco = _repository.ObterPorId(id);

            if (GeneroBanco == null)
            {
                throw new DomainException("Genero não foi encontrada.");
            }

            if (_repository.NomeExiste(criarDto.Nome, GeneroIdAtual: id))
            {
                throw new DomainException("Já existe outra Genero com esse nome.");
            }

            GeneroBanco.Nome = criarDto.Nome;
            _repository.Atualizar(GeneroBanco);
        }

        public void Remover(int id)
        {
            Genero GeneroBanco = _repository.ObterPorId(id);

            if (GeneroBanco == null)
            {
                throw new DomainException("Genero não encontrada");
            }

            _repository.Remover(id);
        }
    }
}