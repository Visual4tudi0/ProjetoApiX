using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibraryDomain.Models
{
    public class Categoria : ModelBase
    {
        public string Nome { get; set; }
        public ICollection<Produto> Produtos { get; set; }

        public Categoria()
        {
            Produtos = new HashSet<Produto>();
        }
    }
}

