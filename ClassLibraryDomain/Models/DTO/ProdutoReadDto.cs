namespace ClassLibraryDomain.Models.DTO
{
    public class ProdutoReadDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoReal { get; set; }
        public int Estoque { get; set; }
       
    }
}
