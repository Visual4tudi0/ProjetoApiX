using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.IRestRepository
{
    public interface IRestRepositoryDolar
    {
        Task<decimal> GetCotacaoDolarHoje();
        Task<decimal> GetCotacaoDolarOntem();
        Task<decimal> ObterVariacaoDolarAsync();
    }
}
