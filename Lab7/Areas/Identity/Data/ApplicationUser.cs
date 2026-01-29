using Microsoft.AspNetCore.Identity;


public class ApplicationUser : IdentityUser 
{
    public long CustomerId {get; set;}
}