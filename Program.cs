// See https://aka.ms/new-console-template for more information
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

Console.WriteLine("Coinmerce to Koinly converter");
var inputFile = "C:\\Users\\Raf\\source\\repos\\coinmerce2koinly\\sample\\orders.csv";
var outputFile = "C:\\Users\\Raf\\source\\repos\\coinmerce2koinly\\sample\\Coinmerce_orders.csv";
var koinlyRecords = new List<KoinlyUniversalCsv>();

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    HasHeaderRecord = true
};

using (var reader = new StreamReader(inputFile))
using (var csv = new CsvReader(reader, config))
{
    var records = csv.GetRecords<CoinmerceCsv>();
    
    foreach(var record in records)
    {
        Console.WriteLine($"{record.Type} {record.Coin} {record.AmountCrypto}");
        var koinlyRecord = new KoinlyUniversalCsv
        {
            Date = record.Date.ToUniversalTime()
        };
        koinlyRecords.Add(koinlyRecord);
    }
}

using (var writer = new StreamWriter(outputFile))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(koinlyRecords);
}

internal class CoinmerceCsv
{
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public string Coin { get; set; }
    [Name("Amount crypto")]
    public decimal AmountCrypto { get; set; }
    [Name("Amount fiat")]
    public decimal AmountFiat { get; set; }
    public decimal Price { get; set; }
}

internal class KoinlyUniversalCsv
{
    public DateTime Date { get; set; }
    [Name("Sent Amount")]
    public decimal SentAmount { get; set; }
    [Name("Sent Currency")]
    public string SentCurrency { get; set; }
    [Name("Received Amount")]
    public decimal ReceivedAmount { get; set; }
    [Name("Received Currency")]
    public string ReceivedCurrency { get; set; }
    [Name("Fee Amount")]
    public decimal FeeAmount { get; set; }
    [Name("Fee Currency")]
    public string FeeCurrency { get; set; }
    [Name("Net Worth Amount")]
    public decimal NetWorthAmount { get; set; }
    [Name("Net Worth Currency")]
    public string NetWorthCurrency { get; set; }
    public string Label { get; set; }
    public string Description { get; set; }
    public string TxHash { get; set; }
}