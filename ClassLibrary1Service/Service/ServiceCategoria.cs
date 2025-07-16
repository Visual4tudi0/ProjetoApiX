using AutoMapper;
using ClassLibrary1Service.Validation;
using ClassLibraryDomain.Entity;
using ClassLibraryDomain.IRepository;
using ClassLibraryDomain.IService;
using ClassLibraryDomain.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibrary1Service.Service
{
    public class CategoriaService : IServiceCategoria
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private object categoria;

        public CategoriaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Categoria> AdicionarCategoria(Categoria categoriaModel)
        {
            ValidationResult validation = new CategoriaValidator().Validate(categoriaModel);
            if (!validation.IsValid)
                throw new Exception(validation.Errors.Count() == 1 ? validation.Errors[0].ErrorMessage : validation.Errors[1].ErrorMessage);

            var categoriaEntity = _mapper.Map<CategoriaEntity>(categoriaModel);

            await _unitOfWork.Categorias.AddAsync(categoriaEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<Categoria>(categoriaModel);
        }

        public async Task<IEnumerable<Categoria>> ListarTodasCategorias()
        {
            var categoriaEntities = await _unitOfWork.Categorias.BuscarTodasCategoriasServicos();

            return _mapper.Map<IEnumerable<Categoria>>(categoriaEntities);
        }

        public async Task<Categoria> BuscarCategoriaPorId(Guid id)
        {
            var categoriaEntity = await _unitOfWork.Categorias.GetByIdAsync(id);
            if (categoriaEntity == null)
            {
                return null; 
            }
            return _mapper.Map<Categoria>(categoriaEntity);
        }
    }
}
