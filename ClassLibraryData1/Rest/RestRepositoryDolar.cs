using ClassLibraryDomain.Configuration;
using ClassLibraryDomain.IRestRepository;
using ClassLibraryDomain.Models.DTO;
using ClassLibraryDomain.Response;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryData1.Rest
{
    //public class RestRepositoryDolar : IRestRepositoryDolar
    //{
    //    private readonly HttpClient _httpClient;
    //    private const string BaseApiUrl = "https://api.exchangerate-api.com/v4/latest/"; // Exemplo de API pública (pode ter limites de uso)
    //    // Você pode substituir por uma API mais robusta ou de testes, como 'https://api.frankfurter.app/latest?from=USD&to=BRL'
    //    // Ou simular com dados fixos para testes.

    //    public RestRepositoryDolar(HttpClient httpClient)
    //    {
    //        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    //        // Configurar a base URL do HttpClient se for sempre a mesma para a API de cotação
    //        _httpClient.BaseAddress = new Uri(BaseApiUrl);
    //    }

    //    public async Task<decimal> GetCotacaoDolarHoje()
    //    {
    //        try
    //        {
    //            // Obter a cotação atual do dólar para BRL
    //            // URL: https://api.exchangerate-api.com/v4/latest/USD
    //            var response = await _httpClient.GetStringAsync("USD");
    //            var apiResponse = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(response);

    //            if (apiResponse != null && apiResponse.Rates != null && apiResponse.Rates.ContainsKey("BRL"))
    //            {
    //                return (decimal)apiResponse.Rates["BRL"];
    //            }
    //            throw new Exception("Não foi possível obter a cotação do dólar (BRL) para hoje.");
    //        }
    //        catch (HttpRequestException httpEx)
    //        {
    //            // Log de erro de rede ou HTTP
    //            Console.WriteLine($"Erro HTTP ao buscar cotação de hoje: {httpEx.Message}");
    //            throw new ApplicationException("Falha na comunicação com a API de cotação do dólar.", httpEx);
    //        }
    //        catch (Exception ex)
    //        {
    //            // Log de outros erros (parsing, etc.)
    //            Console.WriteLine($"Erro ao processar cotação de hoje: {ex.Message}");
    //            throw new ApplicationException("Erro ao processar dados da API de cotação.", ex);
    //        }
    //    }

    //    public async Task<decimal> GetCotacaoDolarOntem()
    //    {
    //        // Para obter a cotação de "ontem", muitas APIs exigem uma data específica.
    //        // A API de exemplo (exchangerate-api.com) geralmente só fornece o "latest".
    //        // Para simular, você pode:
    //        // 1. Usar uma API que permite datas históricas.
    //        // 2. Simular um valor fixo para "ontem" para fins de teste.
    //        // 3. Ajustar a lógica para, por exemplo, buscar a cotação de 24h atrás se a API suportar.

    //        // Para fins de demonstração, vou simular um valor ligeiramente diferente ou usar uma API que suporte.
    //        // A API Frankfurter (https://api.frankfurter.app/2023-01-01..2023-01-02?from=USD&to=BRL)
    //        // É mais adequada para datas históricas.

    //        // Exemplo usando Frankfurter para simular ontem (requer ajuste da BaseApiUrl)
    //        // Ex: "https://api.frankfurter.app/" e then `date/`
    //        try
    //        {
    //            var yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
    //            var url = $"https://api.frankfurter.app/{yesterday}?from=USD&to=BRL";
    //            // Certifique-se de que o _httpClient.BaseAddress não está conflitando se usar uma URL completa
    //            var response = await new HttpClient().GetStringAsync(url); // Usar um novo HttpClient para a URL completa se BaseAddress estiver definida
    //            var apiResponse = JsonConvert.DeserializeObject<FrankfurterApiResponse>(response);

    //            if (apiResponse != null && apiResponse.Rates != null && apiResponse.Rates.ContainsKey(yesterday) && apiResponse.Rates[yesterday].ContainsKey("BRL"))
    //            {
    //                return apiResponse.Rates[yesterday]["BRL"];
    //            }
    //            throw new Exception("Não foi possível obter a cotação do dólar (BRL) para ontem.");
    //        }
    //        catch (HttpRequestException httpEx)
    //        {
    //            Console.WriteLine($"Erro HTTP ao buscar cotação de ontem: {httpEx.Message}");
    //            throw new ApplicationException("Falha na comunicação com a API de cotação do dólar para ontem.", httpEx);
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Erro ao processar cotação de ontem: {ex.Message}");
    //            throw new ApplicationException("Erro ao processar dados da API de cotação para ontem.", ex);
    //        }

    //        // SIMULAÇÃO se a API não suportar data:
    //        // var todayRate = await GetCotacaoDolarHoje();
    //        // return todayRate * 0.99m; // Exemplo: 1% menos que hoje
    //    }

    //    public async Task<decimal> CalcularVariacaoDolar()
    //    {
    //        var cotacaoHoje = await GetCotacaoDolarHoje();
    //        var cotacaoOntem = await GetCotacaoDolarOntem();

    //        if (cotacaoOntem == 0) return 0; // Evita divisão por zero

    //        // Cálculo da variação percentual: ((Hoje - Ontem) / Ontem) + 1
    //        // Se o PrecoReal será *multiplicado* por esse valor, e o preço é PrecoBase * Variacao,
    //        // então PrecoReal deve ser (PrecoBase * (1 + ((Hoje - Ontem) / Ontem))).
    //        // O seu pedido: "multiplicar esse valor ao seu PrecoReal". Isso pode significar
    //        // que PrecoReal = PrecoReal * VariacaoFator, onde VariacaoFator pode ser a própria cotação de hoje,
    //        // ou a variação percentual.
    //        // Considerando "multiplicar o valor da variação da moeda", isso sugere que
    //        // o resultado final já incluiria o fator da variação.
    //        // Se a variação é ((Hoje - Ontem) / Ontem), então o fator de ajuste é (1 + ((Hoje - Ontem) / Ontem)).
    //        // Vamos retornar o fator de ajuste para que PrecoReal seja ajustado.

    //        decimal fatorAjuste = 1 + ((cotacaoHoje - cotacaoOntem) / cotacaoOntem);
    //        return fatorAjuste;
    //    }

    //    // Classes auxiliares para deserialização JSON (dependendo da API)
    //    private class ExchangeRateApiResponse
    //    {
    //        public string Base { get; set; }
    //        public Dictionary<string, double> Rates { get; set; }
    //        public long Time_last_updated { get; set; }
    //    }

    //    private class FrankfurterApiResponse
    //    {
    //        public decimal Amount { get; set; }
    //        public string Base { get; set; }
    //        public DateTime Date { get; set; }
    //        public Dictionary<string, Dictionary<string, decimal>> Rates { get; set; }
    //    }
    //}

    public class RestRepositoryDolar : IRestRepositoryDolar
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ProjetoWebApiXConfig> _options;

        public RestRepositoryDolar(HttpClient httpClient, IOptions<ProjetoWebApiXConfig> options )
        {
            _httpClient = httpClient;
            _options = options;

        }

       public async Task<decimal> GetCotacaoDolarHoje()
        {
            var hoje = DateTime.UtcNow.Date;
            var hojeUrl = $"https://economia.awesomeapi.com.br/json/daily/USD-BRL/1?start_date={hoje:yyyyMMdd}&end_date={hoje:yyyyMMdd}";
            var hojeResponse = await _httpClient.GetFromJsonAsync<List<ResponseDolar>>(hojeUrl);
            if (hojeResponse == null ||  !hojeResponse.Any() )
                return 1;
            decimal hojeValor = decimal.Parse(hojeResponse[0].bid, CultureInfo.InvariantCulture);
            return hojeValor;
        }
        public async Task<decimal> GetCotacaoDolarOntem()
        {
            var hoje = DateTime.UtcNow.Date;
            var ontem = hoje.AddDays(-1);
            var ontemUrl = $"https://economia.awesomeapi.com.br/json/daily/USD-BRL/1?start_date={ontem:yyyyMMdd}&end_date={ontem:yyyyMMdd}";
            var ontemResponse = await _httpClient.GetFromJsonAsync<List<ResponseDolar>>(ontemUrl);
            if (ontemResponse == null || !ontemResponse.Any())
                return 1;
            decimal ontemValor = decimal.Parse(ontemResponse[0].bid, CultureInfo.InvariantCulture);
            return ontemValor;
        }

        public async Task<decimal> ObterVariacaoDolarAsync()
        {
            var hoje = DateTime.UtcNow.Date;
            var ontem = hoje.AddDays(-1);

            var hojeUrl = $"https://economia.awesomeapi.com.br/json/daily/USD-BRL/1?start_date={hoje:yyyyMMdd}&end_date={hoje:yyyyMMdd}";
            var ontemUrl = $"https://economia.awesomeapi.com.br/json/daily/USD-BRL/1?start_date={ontem:yyyyMMdd}&end_date={ontem:yyyyMMdd}";

            var hojeResponse = await _httpClient.GetFromJsonAsync<List<ResponseDolar>>(hojeUrl);
            var ontemResponse = await _httpClient.GetFromJsonAsync<List<ResponseDolar>>(ontemUrl);

            if (hojeResponse == null || ontemResponse == null || !hojeResponse.Any() || !ontemResponse.Any())
                return 1;

            decimal hojeValor = decimal.Parse(hojeResponse[0].bid, CultureInfo.InvariantCulture);
            decimal ontemValor = decimal.Parse(ontemResponse[0].bid, CultureInfo.InvariantCulture);

            return hojeValor / ontemValor;
        }
    }
}
