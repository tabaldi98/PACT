using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Models;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Application.AttendanceAgg.Models
{
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
