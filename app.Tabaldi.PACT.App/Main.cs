using app.Tabaldi.PACT.App.AuthenticationModule;
using app.Tabaldi.PACT.App.Features.AttendanceAgg;
using app.Tabaldi.PACT.App.Features.ClientsAgg;
using app.Tabaldi.PACT.App.Features.ReportsAgg;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App
{
    public partial class Main : Form
    {
        private readonly ProfileModel _profile;

        public Main(ProfileModel profile)
        {
            InitializeComponent();

            SetUserControlInPanel(new ClientsUserControl());

            _profile = profile;

            lblUser.Text = _profile.FullName;
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

        private void lblUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new ProfileForm(_profile).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetUserControlInPanel(new ReportsUserControl());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TokenHelper.SetToken(string.Empty);

            Process.Start(Process.GetCurrentProcess().MainModule.FileName);

            Application.Exit();
        }
    }
}
