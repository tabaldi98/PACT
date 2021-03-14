using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.Mappers;
using app.Tabaldi.PACT.Domain.Seedwork.Specification;
using app.Tabaldi.PACT.LibraryModels.ReportsModule.Models;
using System;
using System.Linq.Expressions;

namespace app.Tabaldi.PACT.Application.ReportsModule.Models
{
    public class ReportsAttendancesModelMapper : IHaveMapper<Attendance, ReportsAttendancesModel>
    {
        public ReportsAttendancesModelMapper(ISpecification<Attendance> specification = null)
        {
            Specification = specification;
        }

        public Expression<Func<Attendance, ReportsAttendancesModel>> Selector => attendance => new ReportsAttendancesModel()
        {
            ClientName = attendance.Client.Name,
            AttendanceDate = attendance.Date,
            Description = attendance.Description,
            EndTime = attendance.HourFinish,
            StartTime = attendance.HourInitial,
        };

        public ISpecification<Attendance> Specification { get; }
    }

}
