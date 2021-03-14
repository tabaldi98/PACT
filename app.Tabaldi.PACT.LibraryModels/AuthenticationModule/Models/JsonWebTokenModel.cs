namespace app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models
{
    public class JsonWebTokenModel
    {
        public string AccessToken { get; private set; }
        public string TokenType => "bearer";
        public long ExpiresIn { get; private set; }

        public JsonWebTokenModel(string accessToken, long expiresIn)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }
    }
}
