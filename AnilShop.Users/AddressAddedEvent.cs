﻿namespace AnilShop.Users;

internal sealed class AddressAddedEvent : DomainEventBase
{
    public AddressAddedEvent(UserStreetAddress newAddress)
    {
        NewAddress = newAddress;
    }
    
    public UserStreetAddress NewAddress { get; }
}