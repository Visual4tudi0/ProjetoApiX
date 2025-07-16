using ClassLibraryDomain.IRestRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Models.DTO
{
    public class ProdutoDto : ModelBase
    {
        public string Nome { get; set; }
        public decimal PrecoReal { get; set; }
        public int Estoque { get; set; }
        public Guid CategoriaId { get; set; }

    }
}
