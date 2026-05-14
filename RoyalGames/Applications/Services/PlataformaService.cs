using RoyalGames.Domains;
using RoyalGames.DTOs.PlataformaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalGames.Applications.Services
{
    public class PlataformaService
    {
        private readonly IPlataformaRepository _repository;

        public PlataformaService(IPlataformaRepository repository)
        {
            _repository = repository;
        }

        public List<LerPlataformaDto> Listar()
        {
            List<Plataforma> plataformas = _repository.Listar();

            List<LerPlataformaDto> plataformasDto = plataformas.Select(p => new LerPlataformaDto
            {
                PlataformaID = p.PlataformaID,
                Nome = p.Nome
            }).ToList();

            return plataformasDto;
        }

        public LerPlataformaDto ObterPorId(int id)
        {
            Plataforma plataforma = _repository.ObterPorId(id);
            if (plataforma == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }
            LerPlataformaDto plataformaDto = new LerPlataformaDto
            {
                PlataformaID = plataforma.PlataformaID,
                Nome = plataforma.Nome
            };
            return plataformaDto;

        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("O nome da plataforma é obrigatório.");
            }

        }

        public void Adicionar(CriarPlataformaDto criarPlataformaDto)
        {
            ValidarNome(criarPlataformaDto.Nome);
            if (_repository.NomeExiste(criarPlataformaDto.Nome))
            {
                throw new DomainException("Já existe uma plataforma com esse nome.");
            }
            Plataforma plataforma = new Plataforma
            {
                Nome = criarPlataformaDto.Nome
            };
            _repository.Adicionar(plataforma);
        }

        public void Atualizar(int id, CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if (plataformaBanco == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Já existe uma plataforma com esse nome.");
            }
            plataformaBanco.Nome = criarDto.Nome;
            _repository.Atualizar(plataformaBanco);
        }

        public void Remover(int id)
        {
            Plataforma plataformaBanco = _repository.ObterPorId(id);
            if (plataformaBanco == null)
            {
                throw new DomainException("Plataforma não encontrada.");
            }
            _repository.Remover(id);
        }
    }
}
