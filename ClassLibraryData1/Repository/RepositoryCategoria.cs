

using ClassLibraryData1.Context;
using ClassLibraryDomain.Entity;
using ClassLibraryDomain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ClassLibraryData1.Repository
{
    public class RepositoryCategoria : RepositoryBase<CategoriaEntity>, IRepositoryCategoria
    {

        public RepositoryCategoria(AppDbContext context) : base(context) { }

        public async Task<List<CategoriaEntity>> BuscarTodasCategoriasServicos()
        {
            return await _context.Categorias.Include(e => e.Produtos).ToListAsync();
        }
    }
}