using AnilShop.Reporting.ReportEndpoints;

namespace AnilShop.Reporting;

internal interface ISalesReportService
{
    Task<TopBooksByMonthReport> GetTopBooksByMonthReportAsync(int month, int year);

}