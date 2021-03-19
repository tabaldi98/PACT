using System;

namespace app.Tabaldi.PACT.Crosscutting.NetCore.Extensions
{
    public static class DateExtensions
    {
        /// <summary>
        /// Compara duas datas, ignorando o horário
        /// </summary>
        public static bool Between(this DateTime value1, DateTime value2)
        {
            var initDate = new DateTime(value2.Year, value2.Month, value2.Day, 0, 0, 0);
            var endDate = new DateTime(value2.Year, value2.Month, value2.Day, 23, 59, 59);

            return value1 >= initDate && value1 <= endDate;
        }
    }
}
