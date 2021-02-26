namespace app.Tabaldi.PACT.Crosscutting.NetCore.Extensions
{
    public static class StringExtensions
    {
        public static bool CustomEquals(this string value, string value2)
        {
            return value == value2;
        }
    }
}
