﻿using AnilShop.SharedKernel;
using MongoDB.Driver;

namespace AnilShop.EmailSending.EmailBackgroundService;

internal class MongoDbGetEmailsFromOutboxService : IGetEmailsFromOutboxService
{
    private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;

    public MongoDbGetEmailsFromOutboxService(
        IMongoCollection<EmailOutboxEntity> emailCollection)
    {
        _emailCollection = emailCollection;
    }

    public async Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntity()
    {
        var filter = Builders<EmailOutboxEntity>.Filter.Eq(entity =>
            entity.DateTimeUtcProcessed, null);
        var unsentEmailEntity = await _emailCollection
            .Find(filter)
            .FirstOrDefaultAsync();

        if (unsentEmailEntity == null) return Result.Failure<EmailOutboxEntity>(Error.None);

        return unsentEmailEntity;
    }
}