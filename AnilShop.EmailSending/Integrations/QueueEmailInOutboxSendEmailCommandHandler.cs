using AnilShop.EmailSending.Conracts;
using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.EmailSending.Integrations;

internal class QueueEmailInOutboxSendEmailCommandHandler :
    IRequestHandler<SendEmailCommand, Result<Guid>>
{
    private readonly IQueueEmailsInOutboxService _outboxService;

    public QueueEmailInOutboxSendEmailCommandHandler(IQueueEmailsInOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public async Task<Result<Guid>> Handle(SendEmailCommand request,
        CancellationToken ct)
    {
        var newEntity = new EmailOutboxEntity
        {
            Body = request.Body,
            Subject = request.Subject,
            To = request.To,
            From = request.From
        };

        await _outboxService.QueueEmailForSending(newEntity);

        return newEntity.Id;
    }
}