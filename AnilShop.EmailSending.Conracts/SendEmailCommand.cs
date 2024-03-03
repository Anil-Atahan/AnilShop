using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.EmailSending.Conracts;

public class SendEmailCommand : IRequest<Result<Guid>>
{
    public string To { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}