using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibraryDomain.IService
{
    public interface IServiceCategoria
    {
        Task<Categoria> AdicionarCategoria(Categoria categoriaModel);
        Task<IEnumerable<Categoria>> ListarTodasCategorias();
        Task<Categoria> BuscarCategoriaPorId(Guid id);
        
    }
}

