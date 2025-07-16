using AutoMapper;
using ClassLibraryDomain.IRestRepository;
using ClassLibraryDomain.IService;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Models.DTO;
using ClassLibraryDomain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json.Serialization;


namespace ProjetoWebApiX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IServiceProduto _serviceProduto;
        private readonly IMapper _mapper;
        private readonly IRestRepositoryDolar _restRepositoryDolar;

        public ProdutoController(IServiceProduto serviceProduto, IMapper mapper, IRestRepositoryDolar restRepositoryDolar)
        {
            _serviceProduto = serviceProduto;
            _mapper = mapper;
            _restRepositoryDolar = restRepositoryDolar;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetTodos()
        {
            var produtos = await _serviceProduto.ListarTodosProdutos();
            return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(produtos));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDto>> GetPorId(Guid id)
        {
            var produto = await _serviceProduto.BuscarProdutoPorId(id);
            if (produto == null) return NotFound();
            return Ok(_mapper.Map<ProdutoDto>(produto));
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(ProdutoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);          
            var produto = _mapper.Map<Produto>(dto);
            await _serviceProduto.AdicionarProduto(produto);
            return CreatedAtAction(nameof(GetPorId), new { id = produto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Atualizar(Guid id, [FromBody] ProdutoDto dto)
        {
            if (id != dto.Id) return BadRequest("ID da URL difere do corpo da requisição");

            var produto = _mapper.Map<Produto>(dto);
            await _serviceProduto.AtualizarProduto(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            await _serviceProduto.RemoverProduto(id);
            return NoContent();
        }
    }
}