namespace Shop.Domain.Exceptions
{
    public class ExistingUserException(string email) : Exception($"User with provided {email} email address already exist!")
    {

    }
}
