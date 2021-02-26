namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Commands
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
