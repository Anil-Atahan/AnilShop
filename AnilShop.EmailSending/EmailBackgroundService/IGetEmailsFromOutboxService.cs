using AnilShop.EmailSending.Data;
using AnilShop.SharedKernel;

namespace AnilShop.EmailSending.EmailBackgroundService;

internal interface IGetEmailsFromOutboxService
{
    Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntity();
}