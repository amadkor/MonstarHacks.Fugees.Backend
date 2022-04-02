using System.IdentityModel.Tokens.Jwt;

namespace MonstarHacks.Fugees.Backend.Helpers
{
    public static class IdentityHelpers
    {
        public static string GetSubjectForContext(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"][0].Split(" ")[1];
            var jwt = new JwtSecurityToken(token);
            return jwt.Subject;
        }
    }
}
