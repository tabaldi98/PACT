namespace app.Tabaldi.PACT.LibraryModels.AttendanceModule.Commands
{
    public class AttendanceRemoveCommand
    {
        public int[] IDs { get; set; }

        public void SetIds(int[] ids)
        {
            IDs = ids;
        }
    }
}
