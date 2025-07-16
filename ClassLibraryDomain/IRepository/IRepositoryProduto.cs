using ClassLibraryDomain.Entity;
using ClassLibraryDomain.IRepository;
using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibraryDomain.IRepository
{
    public interface IRepositoryProduto : IRepositoryBase<ProdutoEntity>
    {
        public Task<IEnumerable<Produto>> ListarComCategoriaAsync();
    }
}