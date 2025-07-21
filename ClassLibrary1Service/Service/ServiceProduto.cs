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
            var produtoEntities = await _unitOfWork.Produtos.ListarComCategoriaAsync();

            var produtoModels = _mapper.Map<IEnumerable<Produto>>(produtoEntities);

            foreach (var produtoModel in produtoModels)
            {
                await AplicarVariacaoDolar(produtoModel);
            };
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

        private async Task AplicarVariacaoDolar(Produto produtoModel)
        {
            var variacaoDolar = await _restCotacaoDolar.ObterVariacaoDolarAsync();
            produtoModel.PrecoReal *= variacaoDolar; 
        }


    }    
}