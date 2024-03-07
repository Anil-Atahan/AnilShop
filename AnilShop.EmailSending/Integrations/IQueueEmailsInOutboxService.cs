using AnilShop.EmailSending.Data;

namespace AnilShop.EmailSending.Integrations;

internal interface IQueueEmailsInOutboxService
{
    Task QueueEmailForSending(EmailOutboxEntity entity);
}