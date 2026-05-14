using RoyalGames.Applications.Regras;
using RoyalGames.Domains;
using RoyalGames.DTOs.ClassificacaoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;

namespace RoyalGames.Applications.Services
{
    public class ClassificacaoService
    {
        private readonly IClassificacaoRepository _repository;

        public ClassificacaoService(IClassificacaoRepository classificacaoRepository)
        {
            _repository = classificacaoRepository;
        }

        public List<LerClassificacaoDto> Listar()
        {
            List<ClassificacaoIndicativa> Classificacoes = _repository.Listar();

            List<LerClassificacaoDto> ClassificacaoDto = Classificacoes.Select(Classificacao => new LerClassificacaoDto
            {
                ClassificacaoID = Classificacao.ClassificacaoIndicativaID,
                Nome = Classificacao.Classificacao
            }).ToList();

            return ClassificacaoDto;
        }

        public LerClassificacaoDto ObterPorId(int id)
        {
            ClassificacaoIndicativa Classificacao = _repository.ObterPorId(id);

            if (Classificacao == null)
            {
                throw new DomainException("Classificação não encontrada");
            }

            LerClassificacaoDto ClassificacaoDto = new LerClassificacaoDto
            {
                ClassificacaoID = Classificacao.ClassificacaoIndicativaID,
                Nome = Classificacao.Classificacao
            };

            return ClassificacaoDto;
        }

        public void Adicionar(CriarClassificacaoDto criarDto)
        {
            ValidacaoNome.ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Classificação já existente.");
            }

            ClassificacaoIndicativa Classificacao = new ClassificacaoIndicativa
            {
                Classificacao = criarDto.Nome,
            };

            _repository.Adicionar(Classificacao);
        }

        public void Atualizar(int id, CriarClassificacaoDto criarDto)
        {
            ValidacaoNome.ValidarNome(criarDto.Nome);

            ClassificacaoIndicativa ClassificacaoBanco = _repository.ObterPorId(id);

            if (ClassificacaoBanco == null)
            {
                throw new DomainException("Classificação não encontrada.");
            }

            if (_repository.NomeExiste(criarDto.Nome, classificacaoIdAtual: id))
            {
                throw new DomainException("Já existe outra Classificação com esse nome.");
            }

            ClassificacaoBanco.Classificacao = criarDto.Nome;

            _repository.Atualizar(ClassificacaoBanco);
        }

        public void Remover(int id)
        {
            ClassificacaoIndicativa ClassificacaoBanco = _repository.ObterPorId(id);

            if (ClassificacaoBanco == null)
            {
                throw new DomainException("Classificação não encontrada.");
            }

            _repository.Remover(id);
        }
    }
}
