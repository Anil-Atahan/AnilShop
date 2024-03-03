﻿using AnilShop.EmailSending.Conracts;
using AnilShop.EmailSending.EmailBackgroundService;
using AnilShop.SharedKernel;
using MediatR;

namespace AnilShop.EmailSending.Integrations;

internal class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<Guid>>
{
    private readonly ISendEmail _emailSender;

    public SendEmailCommandHandler(ISendEmail emailSender)
    {
        _emailSender = emailSender;
    }
    
    public async Task<Result<Guid>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await _emailSender.SendEmailAsync(request.To,
            request.From,
            request.Subject,
            request.Body);

        return Guid.Empty;
    }
}
