using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.App.DependencyResolution;
using app.Tabaldi.PACT.Infra.Data.HttpClient.AuthenticationModule;
using app.Tabaldi.PACT.Infra.Data.HttpClient.PublicModule;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Commands;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Models;
using Autofac;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.AuthenticationModule
{
    public partial class AuthenticationForm : Form
    {
        private readonly IUserRepository _userRepository = AutofacConfig.Container.Value.Resolve<IUserRepository>();
        private readonly IPublicRepository _publicRepository = AutofacConfig.Container.Value.Resolve<IPublicRepository>();

        public AuthenticationForm()
        {
            InitializeComponent();

            checkSaveCredentials.Checked = Convert.ToBoolean(ConfigHelper.GetValue("SaveCredentials", bool.TrueString));
            txtLogin.Text = ConfigHelper.GetValue("User", string.Empty);
            txtPassword.Text = ConfigHelper.GetValue("Password", string.Empty);

            pictureBox1.Image = Image.FromFile("Properties\\autenticacao.jpeg");
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private async void Login()
        {
            try
            {
                this.SetIsLoading();

                HandleSaveCredentials();

                var tokenResult = await _userRepository.AuthenticateAsync(new AuthenticateCommand()
                {
                    UserName = txtLogin.Text,
                    Password = txtPassword.Text
                });

                TokenHelper.SetToken(tokenResult.AccessToken);

                Process.Start(Process.GetCurrentProcess().MainModule.FileName);

                Application.Exit();
            }
            catch
            {
                MessageBox.Show("Usuário ou senha inválidos", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.SetNoLoading();
            }
        }

        private async void AuthenticationForm_Load(object sender, EventArgs e)
        {
            this.SetIsLoading();

            var token = TokenHelper.Token();

#if DEBUG
            var tokenResult = await _userRepository.AuthenticateAsync(new AuthenticateCommand()
            {
                UserName = txtLogin.Text,
                Password = txtPassword.Text
            });

            TokenHelper.SetToken(tokenResult.AccessToken);

            token = tokenResult.AccessToken;
#endif

            var isAlive = await _publicRepository.IsAliveAsync();

            this.SetNoLoading();

            if (isAlive)
            {
                this.SetIsLoading();

                var profile = await _userRepository.GetProfileAsync(token);

                this.SetNoLoading();

                ShowMain(profile);
            }
        }

        private void ShowMain(ProfileModel profileModel)
        {
            Hide();
            var form2 = new Main(profileModel);
            form2.Closed += (s, args) => Close();
            form2.Show();
        }

        private void HandleSaveCredentials()
        {
            ConfigHelper.SetValue("SaveCredentials", checkSaveCredentials.Checked.ToString());
            ConfigHelper.SetValue("User", checkSaveCredentials.Checked ? txtLogin.Text : string.Empty);
            ConfigHelper.SetValue("Password", checkSaveCredentials.Checked ? txtPassword.Text : string.Empty);
        }
    }
}
