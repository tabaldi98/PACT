using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.LibraryModels.AttendanceModule.Enums;
using app.Tabaldi.PACT.LibraryModels.AttendanceRecurrenceModule.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Features.AttendanceAgg
{
    public partial class AttendanceMonthUserControl : UserControl
    {
        private readonly ViewPeriodType _viewPeriodType;

        public AttendanceMonthUserControl(ViewPeriodType viewPeriodType)
        {
            InitializeComponent();

            dgAttendances.AutoGenerateColumns = true;
            dgAttendances.ReadOnly = true;

            _viewPeriodType = viewPeriodType;

            SetData();
        }

        public void SetData()
        {
            this.SetLoading(true);

            switch (_viewPeriodType)
            {
                case ViewPeriodType.CurrentMonth:
                    var current = new List<AttendancesCurrentMonthModel>()
                    {
                        new AttendancesCurrentMonthModel()
                        {
                            ClientName ="zé",
                            //DayOfAttendance = "Segunda",
                            //DayOffWeekAttendance = DateTime.Now,
                            EndAttendance = DateTime.Now.AddHours(-1),
                            StartAttendance = DateTime.Now,
                        }
                    };
                    dgAttendances.DataSource = current;
                    break;
                case ViewPeriodType.PastMonth:
                    var past = new List<AttendancesPastMonthModel>()
                    {
                        new AttendancesPastMonthModel()
                        {
                            ClientName ="zé",
                            DayOfAttendance = "Segunda",
                            DayOffWeekAttendance = DateTime.Now,
                            EndAttendance = DateTime.Now.AddHours(-1),
                            StartAttendance = DateTime.Now,
                        }
                    };
                    dgAttendances.DataSource = past;
                    break;
                case ViewPeriodType.NextMonth:
                    var next = new List<AttendancesNextMonthModel>()
                    {
                        new AttendancesNextMonthModel()
                        {
                            ClientName ="zé",
                            DayOfAttendance = "Segunda",
                            DayOffWeekAttendance = DateTime.Now,
                            EndAttendance = DateTime.Now.AddHours(-1),
                            StartAttendance = DateTime.Now,
                        }
                    };
                    dgAttendances.DataSource = next;
                    break;
                case ViewPeriodType.Today:
                case ViewPeriodType.CurrentWeek:
                default:
                    throw new Exception($"Invalid {_viewPeriodType}");
            }


            dgAttendances.Columns[nameof(AttendancesCurrentMonthModel.ClientName)].HeaderText = "Paciente";
            dgAttendances.Columns[nameof(AttendancesCurrentMonthModel.DayOffWeekAttendance)].HeaderText = "Data do atendimento";
            dgAttendances.Columns[nameof(AttendancesCurrentMonthModel.DayOffWeekAttendance)].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgAttendances.Columns[nameof(AttendancesCurrentMonthModel.DayOfAttendance)].HeaderText = "Dia da semana";
            dgAttendances.Columns[nameof(AttendancesCurrentMonthModel.StartAttendance)].HeaderText = "Hora de inicio";
            dgAttendances.Columns[nameof(AttendancesCurrentMonthModel.StartAttendance)].DefaultCellStyle.Format = "HH:mm";
            dgAttendances.Columns[nameof(AttendancesCurrentMonthModel.EndAttendance)].HeaderText = "Hora do final";
            dgAttendances.Columns[nameof(AttendancesCurrentMonthModel.EndAttendance)].DefaultCellStyle.Format = "HH:mm";

            this.SetLoading(false);
        }
    }
}
