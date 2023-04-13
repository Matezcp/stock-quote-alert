using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stock_quote_alert.Dto;

namespace stock_quote_alert.Services
{
    public class ApiService
    {
        public async Task<double?> GetStockPrice(string stockQuote)
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://brapi.dev/api/quote/" + stockQuote);

            double? price = null;

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<dynamic>(content);
                price = Double.Parse(jsonObject?.results[0].regularMarketPrice.ToString());
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("Código de negociação não encontrado, verifique o código passado ao sistema!");
            }
            else
            {
                Console.WriteLine("Consulta falhou com código de erro: " + response.StatusCode);
            }

            return price;
        }
    }
}
