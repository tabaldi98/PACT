using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.App.DependencyResolution;
using app.Tabaldi.PACT.Infra.Data.HttpClient.AuthenticationModule;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models;
using Autofac;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.AuthenticationModule
{
    public partial class ProfileForm : Form
    {
        private readonly ProfileModel _profile;
        private readonly IUserRepository _userRepository = AutofacConfig.Container.Value.Resolve<IUserRepository>();

        public ProfileForm(ProfileModel profile)
        {
            InitializeComponent();

            _profile = profile;

            SetData();
        }

        #region Métodos privados

        private void SetData()
        {
            txtFullName.Text = _profile.FullName;
            txtLogin.Text = _profile.UserName;
            txtMail.Text = _profile.Mail;
            txtPassword.Text = _profile.Password;
            checkSendMail.Checked = _profile.SendAlerts;

            lblRegistrationDate.Text += _profile.RegistrationDate.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private bool ValidateAllFields()
        {
            if (txtLogin.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldMessage(lblLogin.Text);

                return false;
            }

            if (txtPassword.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldMessage(lblPassword.Text);

                return false;
            }

            if (txtFullName.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldMessage(lblFullName.Text);

                return false;
            }

            if (txtMail.IsNullOrEmpty())
            {
                MessageBoxExtensions.ShowEmptyFieldMessage(lblMail.Text);

                return false;
            }

            return true;
        }

        #endregion

        #region Eventos

        private void button2_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private async void btnOk_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Será necessário fazer login novamente após salvar as alterações. Deseja continuar mesmo assim?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (!ValidateAllFields()) { return; }

            try
            {
                this.SetIsLoading();

                var command = new ProfileCommand
                {
                    FullName = txtFullName.Text,
                    Mail = txtMail.Text,
                    Password = txtPassword.Text,
                    UserName = txtLogin.Text,
                    SendAlerts = checkSendMail.Checked,
                };

                await _userRepository.UpdateProfileAsync(command);

                Process.Start(Process.GetCurrentProcess().MainModule.FileName);

                Application.Exit();
            }
            catch
            { }
            finally
            {
                this.SetNoLoading();
            }
        }

        #endregion
    }
}
