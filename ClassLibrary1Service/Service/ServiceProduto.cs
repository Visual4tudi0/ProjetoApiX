using AutoMapper;
using ClassLibrary1Service.Validation;
using ClassLibraryData1.Repository;
using ClassLibraryDomain.Entity;
using ClassLibraryDomain.IRepository;
using ClassLibraryDomain.IRestRepository;
using ClassLibraryDomain.IService;
using ClassLibraryDomain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibrary1Service.Service
{
    public class ServiceProduto : IServiceProduto
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRestRepositoryDolar _restCotacaoDolar;

        public ServiceProduto(IUnitOfWork unitOfWork, IMapper mapper, IRestRepositoryDolar restCotacaoDolar)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _restCotacaoDolar = restCotacaoDolar ?? throw new ArgumentNullException(nameof(restCotacaoDolar));
        }

        public async Task<Produto> AdicionarProduto(Produto produtoModel)
        {
            ValidationResult validation = new ProdutoValidator().Validate(produtoModel);
            if (!validation.IsValid)
                throw new Exception(validation.Errors.Count() == 1 ? validation.Errors[0].ErrorMessage : validation.Errors[1].ErrorMessage);

            var produtoEntity = _mapper.Map<ProdutoEntity>(produtoModel);
            await _unitOfWork.Produtos.AddAsync(produtoEntity);
            await _unitOfWork.CommitAsync();

            var addedProdutoModel = _mapper.Map<Produto>(produtoModel);
            await AplicarVariacaoDolar(addedProdutoModel);
            
            return addedProdutoModel;
        }

        public async Task<bool> AtualizarProduto(Produto produtoModel)
        {
            var existingProduto = await _unitOfWork.Produtos.GetByIdAsync(produtoModel.Id);
            if (existingProduto == null)
            {
                return false;
            }
            _mapper.Map(produtoModel, existingProduto);
            await _unitOfWork.Produtos.UpdateAsync(existingProduto);

            var affectedRows = await _unitOfWork.CommitAsync();
            return affectedRows > 0;
        }

        public async Task<bool> RemoverProduto(Guid id)
        {
            await _unitOfWork.Produtos.DeleteAsync(id);
            var affectedRows = await _unitOfWork.CommitAsync();
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Produto>> ListarTodosProdutos()
        {
            var produtoEntities = await _unitOfWork.Produtos.GetAllAsync();

            var produtoModels = _mapper.Map<IEnumerable<Produto>>(produtoEntities);

            foreach (var produtoModel in produtoModels)
            {
                await AplicarVariacaoDolar(produtoModel);
            }

            return produtoModels;
        }

        public async Task<Produto> BuscarProdutoPorId(Guid id)
        {
            var produtoEntity = await _unitOfWork.Produtos.GetByIdAsync(id);
            if (produtoEntity == null)
            {
                return null; 
            }
            var produtoModel = _mapper.Map<Produto>(produtoEntity);

            await AplicarVariacaoDolar(produtoModel);

            return produtoModel;
        }

        /// <summary>
        /// Método privado para calcular e aplicar a variação do dólar ao PrecoReal.
        /// </summary>
        /// <param name="produtoModel">O modelo do produto para aplicar a variação.</param>
        private async Task AplicarVariacaoDolar(Produto produtoModel)
        {
            var variacaoDolar = await _restCotacaoDolar.ObterVariacaoDolarAsync();
            // Multiplicar o valor da variação da moeda ao PrecoReal
            // Assumindo que PrecoBase é a base para o cálculo e PrecoReal é o que será exibido.
            // Se PrecoReal já tiver um valor, ajuste a lógica. Aqui, estou usando PrecoBase da entity.
            // Para isso, precisaria buscar o PrecoBase da entity original ou passar na model.
            // Para simplificar, vou assumir que PrecoReal na Model é o PrecoBase da Entity no momento da chamada,
            // ou que PrecoBase é um campo que você também está mapeando para a Model e será a base.
            // Vamos usar o PrecoBase da entidade mapeada (PrecoBase da entity).
            // IMPORTANTE: Para o PrecoReal ser calculado, o mapeamento do AutoMapper para ProdutoModel
            // precisaria não ignorar PrecoReal, ou você precisaria do PrecoBase da entity.
            // Considerando o escopo, PrecoReal será calculado *a partir* do PrecoBase da entidade.

            // Primeiro, obtenha o PrecoBase da entidade correspondente ao Model.
            // Isso requer que a entidade base esteja disponível ou que o ProdutoModel
            // contenha o PrecoBase da entidade.
            // Para simplificar, vamos assumir que o ProdutoModel tem um PrecoBase
            // que é mapeado da Entity, e PrecoReal é derivado dele.
            // Ou que a operação PrecoReal * variacaoDolar já é o que se espera.

            // Vou usar o PrecoBase que estaria na entidade original antes do mapeamento para Model.
            // Uma forma mais direta seria ter o PrecoBase no ProdutoModel para esse cálculo.
            // Vou assumir que o ProdutoModel tem um PrecoBase ou que PrecoReal é a base inicial.
            // Baseado na sua definição: "Com o valor da variação da moeda em mãos, voce irá multiplicar esse valor ao seu PrecoReal (propriedade de ProdutoModel) toda vez que exibir os dados ao usuario."
            // Isso implica que PrecoReal já tem um valor inicial para ser multiplicado.
            // Se PrecoReal não tiver um valor inicial, ele deveria ser calculado a partir de PrecoBase.

            // Assumindo que PrecoReal no ProdutoModel É o PrecoBase que veio da Entity ou foi setado:
            produtoModel.PrecoReal *= variacaoDolar; // Multiplica o PrecoReal pelo fator de variação
        }


    }    
}