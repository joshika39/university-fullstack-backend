using Microsoft.AspNetCore.Identity;

namespace CardTrader.DataAccess.Models;

public class User : IdentityUser
{
    public Guid? RefreshToken { get; set; }
}
