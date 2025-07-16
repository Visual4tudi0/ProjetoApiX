using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace ClassLibraryDomain.Models
{

    public class Produto : ModelBase
    {
        public string Nome { get; set; }
        public decimal PrecoReal { get; set; }
        public int Estoque { get; set; }
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}    
