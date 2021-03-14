using System.Collections.Generic;

namespace app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Enums
{
    public static class CustomClaimTypes
    {
        public const string UserID = "usrID";
        public const string Email = "eml";
        public const string FullName = "name";
        public const string Logon = "lgn";
        public const string TokenID = "jti";
        public const string TokenExpiration = "exp";

        public static IList<string> RetrieveAll()
        {
            return new List<string>()
            {
                Email,
                FullName,
                Logon,
                TokenID,
                TokenExpiration,
            };
        }
    }
}
