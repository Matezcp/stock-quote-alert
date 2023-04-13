using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Dto
{
    public class StockArgsDto
    {
        public string quote { get; set; }
        public double sellPrice { get; set; }
        public double buyPrice { get; set; }

        public StockArgsDto(string quote, double sellPrice, double buyPrice)
        {
            this.quote = quote;
            this.sellPrice = sellPrice;
            this.buyPrice = buyPrice;
        }
    }

}
