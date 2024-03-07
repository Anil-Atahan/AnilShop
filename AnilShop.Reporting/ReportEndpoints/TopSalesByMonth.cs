using FastEndpoints;

namespace AnilShop.Reporting.ReportEndpoints;

internal class TopSalesByMonth :
    Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
    private readonly ISalesReportService _reportService;

    public TopSalesByMonth(ISalesReportService reportService)
    {
        _reportService = reportService;
    }

    public override void Configure()
    {
        Get("/topsales");
        AllowAnonymous(); // TODO: lock down
    }

    public override async Task HandleAsync(
        TopSalesByMonthRequest request,
        CancellationToken ct = default)
    {
        var report = await _reportService.GetTopBooksByMonthReportAsync(
            request.Month, request.Year);
        var response = new TopSalesByMonthResponse { Report = report };
        await SendAsync(response);
    }

}