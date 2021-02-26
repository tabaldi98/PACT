namespace app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg.Commands
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
