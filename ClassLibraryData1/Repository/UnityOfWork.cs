using ClassLibraryData1.Context;
using ClassLibraryDomain.IRepository;
using System.Threading.Tasks;

namespace ClassLibraryData1.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepositoryProduto _repositoryproduto;
        private IRepositoryCategoria _repositorycategoria;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepositoryProduto Produtos => _repositoryproduto ??= new RepositoryProduto(_context);
        public IRepositoryCategoria Categorias => _repositorycategoria ??= new RepositoryCategoria(_context);
        
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}