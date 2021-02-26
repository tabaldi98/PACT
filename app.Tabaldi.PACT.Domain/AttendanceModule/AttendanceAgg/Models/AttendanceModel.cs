using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models
{
    public class AttendanceModel
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public DateTime HourInitial { get; set; }
        public DateTime HourFinish { get; set; }
        public string Description { get; set; }
    }

    public class AttendanceModelMapper : IHaveMapper<Attendance, AttendanceModel>
    {
        private readonly int _clientId;

        public AttendanceModelMapper(int clientId)
        {
            _clientId = clientId;
        }

        public Expression<Func<Attendance, AttendanceModel>> Selector => attendance => new AttendanceModel()
        {
            ID = attendance.ID,
            Date = attendance.Date,
            Description = attendance.Description,
            HourFinish = attendance.HourFinish,
            HourInitial = attendance.HourInitial,
        };

        public ISpecification<Attendance> Specification => AttendanceSpecifications.RetrieveByClientID(_clientId);
    }
}
