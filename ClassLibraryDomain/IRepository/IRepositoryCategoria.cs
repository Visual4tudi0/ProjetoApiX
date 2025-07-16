

using ClassLibraryDomain.Entity;
using ClassLibraryDomain.Models;

namespace ClassLibraryDomain.IRepository
{
    public interface IRepositoryCategoria : IRepositoryBase<CategoriaEntity>
    {

        Task<List<CategoriaEntity>> BuscarTodasCategoriasServicos();
    
    }
}
