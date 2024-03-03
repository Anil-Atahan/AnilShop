namespace AnilShop.EmailSending.EmailBackgroundService;

internal interface ISendEmailsFromOutboxService
{
    Task CheckForAndSendEmails();
}
