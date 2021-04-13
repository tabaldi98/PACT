namespace app.Tabaldi.PACT.LibraryModels.ClientsModule.Commands
{
    public class ClientRemoveCommand
    {
        public int[] IDs { get; set; }

        public void SetIds(int[] ids)
        {
            IDs = ids;
        }
    }
}
