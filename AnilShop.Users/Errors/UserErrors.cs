using AnilShop.SharedKernel.Abstractions;

namespace AnilShop.Users.Errors;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.NotFound",
        "The user with the specified identifier was not found");
}