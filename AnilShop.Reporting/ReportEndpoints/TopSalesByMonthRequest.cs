using Microsoft.AspNetCore.Mvc;

namespace AnilShop.Reporting.ReportEndpoints;

internal class TopSalesByMonthRequest
{
    [FromQuery]
    public int Month { get; set; }
    [FromQuery]
    public int Year { get; set; }
}