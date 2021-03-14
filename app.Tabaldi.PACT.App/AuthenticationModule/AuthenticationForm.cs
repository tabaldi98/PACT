using app.Tabaldi.PACT.App.Commom;
using app.Tabaldi.PACT.Infra.Data.HttpClient.AuthenticationModule;
using app.Tabaldi.PACT.LibraryModels.AuthenticationModule.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.AuthenticationModule
{
    public partial class AuthenticationForm : Form
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationForm()
        {
            InitializeComponent();

            _userRepository = new UserRepository();
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

                var tokenResult = await _userRepository.AuthenticateAsync(new AuthenticateCommand()
                {
                    UserName = txtLogin.Text,
                    Password = txtPassword.Text
                });

                TokenHelper.SetToken(tokenResult.AccessToken);

                var profile = await _userRepository.GetProfileAsync(tokenResult.AccessToken);

                Hide();
                var form2 = new Main(profile);
                form2.Closed += (s, args) => Close();
                form2.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Usuário ou senha inválidos", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.SetNoLoading();
            }
        }
    }
}
