using AnilShop.EmailSending.Data;

namespace AnilShop.EmailSending.Endpoints;

public class ListEmailsResponse
{
    public int Count { get; set; }
    public List<EmailOutboxEntity> Emails { get; internal set; } = new();
}