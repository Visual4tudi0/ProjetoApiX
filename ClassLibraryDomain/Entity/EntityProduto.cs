using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Entity
{
    public class ProdutoEntity : EntityBase
    {
        public string Nome { get; set; }
        public decimal PrecoBase { get; set; }
        public int Estoque { get; set; }
        public Guid CategoriaId { get; set; }
        public CategoriaEntity CategoriaEntity { get; set; }
    }
}
