using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using app.Tabaldi.PACT.Domain.AttendanceModule.AttendanceAgg.Models;

namespace app.Tabaldi.PACT.App.Features.AttendanceAgg
{
    public partial class AttendanceUserControl : UserControl
    {
        public AttendanceUserControl()
        {
            InitializeComponent();

            PushCmb();
        }

        private void PushCmb()
        {
            var periods = new Dictionary<ViewPeriodType, string>
            {
                { ViewPeriodType.Today, "Hoje" },
                { ViewPeriodType.CurrentWeek, "Esta semana" },
                { ViewPeriodType.CurrentMonth, "Este mês" },
                { ViewPeriodType.NextMonth, "Próximo mês" },
                { ViewPeriodType.PastMonth, "Mês passado" }
            };

            cmbPeriodType.DataSource = new BindingSource(periods, null);
            cmbPeriodType.DisplayMember = "Value";
            cmbPeriodType.ValueMember = "Key";

            cmbPeriodType.SelectedItem = periods.FirstOrDefault();
        }

        private void cmbPeriodType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selected = (KeyValuePair<ViewPeriodType, string>)cmbPeriodType.SelectedItem;
            switch (selected.Key)
            {
                case ViewPeriodType.Today:
                default:
                    SetUserControlInPanel(new AttendanceCurrentDayUserControl());
                    break;
                case ViewPeriodType.CurrentWeek:
                    SetUserControlInPanel(new AttendanceCurrentWeekUserControl());
                    break;
                case ViewPeriodType.CurrentMonth:
                case ViewPeriodType.PastMonth:
                case ViewPeriodType.NextMonth:
                    SetUserControlInPanel(new AttendanceMonthUserControl(selected.Key));
                    break;
            }
        }

        private void SetUserControlInPanel(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;

            panel.Controls.Clear();

            panel.Controls.Add(userControl);
        }
    }
}
