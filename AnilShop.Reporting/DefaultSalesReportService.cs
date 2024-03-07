using System.Globalization;
using AnilShop.Reporting.Abstractions.Data;
using Dapper;

namespace AnilShop.Reporting;

internal class DefaultSalesReportService : ISalesReportService
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public DefaultSalesReportService(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<TopBooksByMonthReport> GetTopBooksByMonthReportAsync(int month, int year)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                BookId, Title, Author, UnitsSold as Units, TotalSales as Sales
                from Reporting.MonthlyBookSales
                where Month = @month and Year = @year
                ORDER BY TotalSales DESC
            """;
        
        var results = await connection.QueryAsync<BookSalesResult>(
            sql,
            new
            {
                month,
                year
            });

        var report = new TopBooksByMonthReport
        {
            Year = year,
            Month = month,
            MonthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(month),
            Results = results.ToList()
        };

        return report;
    }
}