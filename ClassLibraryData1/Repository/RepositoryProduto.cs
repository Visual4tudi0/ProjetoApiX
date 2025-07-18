using ClassLibraryData1.Context;
using ClassLibraryDomain.Entity;
using ClassLibraryDomain.IRepository;
using ClassLibraryDomain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibraryData1.Repository
{
    public class RepositoryProduto : RepositoryBase<ProdutoEntity>, IRepositoryProduto
    {
        public RepositoryProduto(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ProdutoEntity>> ListarComCategoriaAsync()
        {
            return await _context.Produtos.Include(p => p.CategoriaEntity).ToListAsync();
        }
    }
}