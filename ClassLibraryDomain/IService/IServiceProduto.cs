using ClassLibraryDomain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibraryDomain.IService
{
    public interface IServiceProduto
    {
        Task<Produto> AdicionarProduto(Produto produtoModel);
        Task<bool> AtualizarProduto(Produto produtoModel);
        Task<bool> RemoverProduto(Guid id);
        Task<IEnumerable<Produto>> ListarTodosProdutos();
        Task<Produto> BuscarProdutoPorId(Guid id);
    }
}
