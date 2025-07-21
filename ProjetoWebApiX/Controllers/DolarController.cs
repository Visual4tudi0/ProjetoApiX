using ClassLibraryData1.Rest;
using ClassLibraryDomain.IRestRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoWebApiX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DolarController : ControllerBase
    {
        private readonly IRestRepositoryDolar _restRepositoryDolar;
        public DolarController(IRestRepositoryDolar restRepositoryDolar)
        {
            _restRepositoryDolar = restRepositoryDolar ?? throw new ArgumentNullException(nameof(restRepositoryDolar));
        }
        [HttpGet]
        public async Task<ActionResult<decimal>> GetDolar()
        {
            var dolarHoje = await _restRepositoryDolar.GetCotacaoDolarHoje();
            var dolarOntem = await _restRepositoryDolar.GetCotacaoDolarOntem();
            var variacao = await _restRepositoryDolar.ObterVariacaoDolarAsync();
            var resultado = new
            {
                DolarHoje = "Dolar de hoje é: "+Math.Round(dolarHoje,2),
                DolarOntem = "Dolar de ontem é: "+Math.Round(dolarOntem,2),
                Variacao = "A variação do dolar entre ontem e hoje é: "+Math.Round(variacao, 4)
            };
            return Ok(resultado);
        }
    }
}
