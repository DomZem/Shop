using Microsoft.AspNetCore.Identity;

namespace Shop.Domain.Entities
{
    public class User : IdentityUser
    {
        // we can add there some additional fields about user which gonna be in database. For example date of birth

        // After add some new field we need to apply migration to database
    }
}
