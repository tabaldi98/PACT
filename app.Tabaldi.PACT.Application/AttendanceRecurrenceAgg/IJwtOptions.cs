using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg
{
    public interface IJwtOptions
    {
        string Issuer { get; }
        string Audience { get; }
        int ExpirationMinutes { get; }
        bool RequireHttpsMetadata { get; }
        SymmetricSecurityKey SymmetricSecurityKey { get; }
        SigningCredentials SigningCredentials { get; }
        DateTime IssuedAt { get; }
        DateTime NotBefore { get; }
        DateTime AccessTokenExpiration { get; }
    }

    public class JwtOptions : IJwtOptions
    {
        public string Issuer { get; }
        public string Audience { get; }
        public int ExpirationMinutes { get; }
        public bool RequireHttpsMetadata { get; }
        public SymmetricSecurityKey SymmetricSecurityKey { get; }
        public SigningCredentials SigningCredentials { get; }

        public DateTime IssuedAt => DateTime.UtcNow;
        public DateTime NotBefore => IssuedAt;
        public DateTime AccessTokenExpiration => IssuedAt.AddMinutes(ExpirationMinutes);

        public JwtOptions()
        {
            Issuer = "https://tabaldi.pact.com/";
            Audience = "Audience";
            ExpirationMinutes = 10000;
            RequireHttpsMetadata = false;

            var signingKey = "EB6a@sB96z8!4CCZAw4@9u#H9aF36N7l";
            SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            SigningCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
