using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Entity
{
    public class CategoriaEntity : EntityBase
    {
        public string Nome { get; set; }
        public ICollection<ProdutoEntity> Produtos { get; set; }

        public CategoriaEntity()
        {
            Produtos = new HashSet<ProdutoEntity>();
        }
    }
}
