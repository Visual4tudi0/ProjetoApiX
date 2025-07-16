using System.Threading.Tasks;

namespace ClassLibraryDomain.IRepository
{
    public interface IUnitOfWork
    {
        IRepositoryProduto Produtos { get; }
        IRepositoryCategoria Categorias { get; }

        Task<int> CommitAsync();
        Task RollbackAsync();
    }
}
