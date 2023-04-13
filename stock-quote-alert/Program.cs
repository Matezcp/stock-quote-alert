using stock_quote_alert.Dto;
using stock_quote_alert.Services;
using System.Globalization;

namespace stock_quote_alert
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                StockArgsDto? stockArgsDto = GetArgs(args);

                if (stockArgsDto == null)
                    return;

                //Console.WriteLine(stockQuote);
                //Console.WriteLine(stockSellPrice);
                //Console.WriteLine(stockBuyPrice);

                await MonitorePrice(stockArgsDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro inesperado!\n" + "Error: " + ex.ToString());
            }

            return;
        }

        static StockArgsDto? GetArgs(string[] args)
        {
            StockArgsDto? stockArgsDto = null;

            try
            {
                string stockQuote = args[0];
                double stockSellPrice = Double.Parse(args[1], CultureInfo.InvariantCulture);
                double stockBuyPrice = Double.Parse(args[2], CultureInfo.InvariantCulture);

                stockArgsDto = new StockArgsDto(stockQuote, stockSellPrice, stockBuyPrice);
            }
            catch (FormatException)
            {
                Console.WriteLine(
                    "Formato Inválido!\n" +
                    "Verifique se o ticker e os preços de negociação de compra estão no formato adequado.\n" +
                    "Exemplo de Formato Adequado: PETR4 22.67 22.59");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Paramêtro faltante.\n" +
                    "Deve ser passado o ticker, o preço de venda e o preço de compra separados por espaço!\n" +
                    "Exemplo de Formato Adequado: PETR4 22.67 22.59");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Parâmetros Inválidos!\n" + "Error: " + ex.ToString());
            }

            return stockArgsDto;
        }

        static async Task MonitorePrice(StockArgsDto stockArgsDto)
        {

            ApiService apiService = new ApiService();
            EmailService emailService = new EmailService();

            // Variables to control the email sendings to not spawn the user
            bool sendSellEmail = true;
            bool sendBuyEmail = true;

            // Make First api request to verify if quote is valid
            double? actualPrice = await apiService.GetStockPrice(stockArgsDto.quote);

            if (actualPrice == null)
                return;

            while (true)
            {
                // Can send sell email again
                if(!sendSellEmail && actualPrice < stockArgsDto.sellPrice)
                {
                    sendSellEmail = true;
                }

                // Can send buy email again
                if (!sendBuyEmail && actualPrice > stockArgsDto.buyPrice)
                {
                    sendBuyEmail = true;
                }

                if (sendSellEmail && actualPrice >= stockArgsDto.sellPrice)
                {
                    string subject = $"Recomendação de Venda - {stockArgsDto.quote}";
                    string body = $"RECOMENDAÇÃO DE VENDA - : {stockArgsDto.quote}\n" +
                        $"Preço de Venda Alvo: {stockArgsDto.sellPrice}\n" +
                        $"Preço atual: {actualPrice}";

                    Console.WriteLine("Recomendação de Venda Enviada");
                    emailService.sendEmail(body,subject);

                    sendSellEmail = false;  
                }
                else if (sendBuyEmail && actualPrice <= stockArgsDto.buyPrice)
                {
                    string subject = $"Recomendação de Compra - {stockArgsDto.quote}";
                    string body = $"RECOMENDAÇÃO DE COMPRA - : {stockArgsDto.quote}\n" +
                        $"Preço de Compra Alvo: {stockArgsDto.buyPrice}\n" +
                        $"Preço atual: {actualPrice}";

                    Console.WriteLine("Recomendação de Compra Enviada");
                    emailService.sendEmail(body, subject);

                    sendBuyEmail = false;
                }

                actualPrice = await apiService.GetStockPrice(stockArgsDto.quote);
            }
        }
    }
}
