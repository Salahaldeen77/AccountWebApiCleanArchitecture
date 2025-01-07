using System.Security.Claims;

namespace AccountWeb.Data.Helpers
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create Account","false"),
            new Claim("Edit Account","false"),
            new Claim("Delete Account","false"),

            new Claim("Create User","false"),
            new Claim("Edit User","false"),
            new Claim("Delete User","false"),
        };
    }
}
