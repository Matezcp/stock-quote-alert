using stock_quote_alert.Dto;
using stock_quote_alert.Services;
using System.Globalization;

namespace stock_quote_alert
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string? stockQuote = null;
            double? stockSellPrice = null;
            double? stockBuyPrice = null;

            try
            {
                stockQuote = args[0];
                stockSellPrice = Double.Parse(args[1], CultureInfo.InvariantCulture);
                stockBuyPrice = Double.Parse(args[2], CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine(
                    "Formato Inválido!\n" +
                    "Verifique se o ticker e os preços de negociação de compra estão no formato adequado.\n" +
                    "Exemplo de Formato Adequado: PETR4 22.67 22.59");
                return;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Paramêtro faltante.\n" +
                    "Deve ser passado o ticker, o preço de venda e o preço de compra separados por espaço!\n" +
                    "Exemplo de Formato Adequado: PETR4 22.67 22.59");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Parâmetros Inválidos!\n" + "Error: " + ex.ToString());
                return;
            }

            ApiService apiService = new ApiService();

            //Console.WriteLine(stockQuote);
            //Console.WriteLine(stockSellPrice);
            //Console.WriteLine(stockBuyPrice);

            Console.WriteLine(await apiService.GetStockPrice(stockQuote));
        }
    }
}
