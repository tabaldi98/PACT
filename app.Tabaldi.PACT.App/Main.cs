using app.Tabaldi.PACT.App.Features.AttendanceAgg;
using app.Tabaldi.PACT.App.Features.ClientsAgg;
using System;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            SetUserControlInPanel(new ClientsUserControl());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetUserControlInPanel(new ClientsUserControl());
        }

        private void SetUserControlInPanel(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;

            panel1.Controls.Clear();

            panel1.Controls.Add(userControl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetUserControlInPanel(new AttendanceUserControl());
        }
    }
}
