using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Template.Services.Auth
{
    /// <summary>
    /// Lee claims basicos de un JWT sin validar firma (solo cliente WASM).
    /// </summary>
    internal static class JwtTokenParser
    {
        public static ClaimsPrincipal CreatePrincipal(string token)
        {
            var claims = ParseClaims(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            return new ClaimsPrincipal(identity);
        }

        public static bool IsExpired(string token)
        {
            var claims = ParseClaims(token);
            var expClaim = claims.FirstOrDefault(claim => claim.Type == "exp")?.Value;

            if (!long.TryParse(expClaim, out var expSeconds))
            {
                return false;
            }

            var expiresAt = DateTimeOffset.FromUnixTimeSeconds(expSeconds);
            return expiresAt <= DateTimeOffset.UtcNow;
        }

        private static IEnumerable<Claim> ParseClaims(string token)
        {
            var parts = token.Split('.');

            if (parts.Length < 2)
            {
                return [new Claim(ClaimTypes.Name, "user")];
            }

            try
            {
                var payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(parts[1]));
                var payload = JObject.Parse(payloadJson);
                var claims = new List<Claim>();

                AddClaimIfPresent(claims, ClaimTypes.NameIdentifier, payload, "sub", "id", "userId");
                AddClaimIfPresent(claims, ClaimTypes.Email, payload, "email", "unique_name");
                AddClaimIfPresent(claims, ClaimTypes.Name, payload, "name", "fullName", "preferred_username");

                if (payload["exp"] != null)
                {
                    claims.Add(new Claim("exp", payload["exp"]!.ToString()));
                }

                if (claims.Count == 0)
                {
                    claims.Add(new Claim(ClaimTypes.Name, "user"));
                }

                return claims;
            }
            catch
            {
                return [new Claim(ClaimTypes.Name, "user")];
            }
        }

        private static void AddClaimIfPresent(
            ICollection<Claim> claims,
            string claimType,
            JObject payload,
            params string[] keys)
        {
            foreach (var key in keys)
            {
                var value = payload[key]?.ToString();

                if (!string.IsNullOrWhiteSpace(value))
                {
                    claims.Add(new Claim(claimType, value));
                    return;
                }
            }
        }

        private static byte[] Base64UrlDecode(string input)
        {
            var padded = input.Replace('-', '+').Replace('_', '/');

            switch (padded.Length % 4)
            {
                case 2:
                    padded += "==";
                    break;
                case 3:
                    padded += "=";
                    break;
            }

            return Convert.FromBase64String(padded);
        }
    }
}
