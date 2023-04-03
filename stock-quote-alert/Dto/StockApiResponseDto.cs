using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Dto
{
    public class stockApiInfoDto
    {
        public string symbol { get; set; }
        public string shortName { get; set; }
        public string longName { get; set; }
        public string currency { get; set; }
        public decimal regularMarketPrice { get; set; }
        public decimal regularMarketDayHigh { get; set; }
        public decimal regularMarketDayLow { get; set; }
        public string regularMarketDayRange { get; set; }
        public decimal regularMarketChange { get; set; }
        public decimal regularMarketChangePercent { get; set; }
        public string regularMarketTime { get; set; }
        public long marketCap { get; set; }
        public long regularMarketVolume { get; set; }
        public decimal regularMarketPreviousClose { get; set; }
        public decimal regularMarketOpen { get; set; }
        public long averageDailyVolume10Day { get; set; }
        public long averageDailyVolume3Month { get; set; }
        public decimal fiftyTwoWeekLowChange { get; set; }
        public decimal fiftyTwoWeekLowChangePercent { get; set; }
        public string fiftyTwoWeekRange { get; set; }
        public decimal fiftyTwoWeekHighChange { get; set; }
        public decimal fiftyTwoWeekHighChangePercent { get; set; }
        public decimal fiftyTwoWeekLow { get; set; }
        public decimal fiftyTwoWeekHigh { get; set; }
        public decimal twoHundredDayAverage { get; set; }
        public decimal twoHundredDayAverageChange { get; set; }
        public decimal twoHundredDayAverageChangePercent { get; set; }
    }

    public class stockApiResponseDto
    {
        public List<stockApiInfoDto> results { get; set; }
        public string requestedAt { get; set; }
    }

}
