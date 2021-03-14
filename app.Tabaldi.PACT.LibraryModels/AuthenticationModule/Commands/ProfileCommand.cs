namespace app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Commands
{
    public class ProfileCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
    }
}
