using System.Windows.Forms;
using Timecards.Application.Commands;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;

namespace Timecards.Client
{
    public partial class FormLogin : Form
    {
        private readonly ILoginCommand _loginCommand;
        private readonly IApiRequestFactory _apiRequestFactory;

        public FormLogin(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();
            _apiRequestFactory = apiRequestFactory;
            _loginCommand = new LoginCommand(apiRequestFactory);

        }

        private void linkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegister formRegister = new FormRegister(_apiRequestFactory);
            this.Hide();
            formRegister.ShowDialog();
        }

        private void buttonSignIn_Click(object sender, System.EventArgs e)
        {
            var loginRequest = new LoginRequest
            {
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Text
            };

            _loginCommand.LoginAsync(loginRequest, (loginResponse) => CallbackProcess(loginResponse));
        }

        private void CallbackProcess(LoginResponse loginResponse)
        {
            if (loginResponse.ResponseState.IsSuccess)
            {
                FormMain formMain = new FormMain();
                this.Hide();
                formMain.ShowDialog();
            }
            else
            {
                MessageBox.Show(loginResponse.RequestFailedState.ErrorMessage, "Login Failed", MessageBoxButtons.OK);
            }
        }

        public void RegisterCallbackProcess(string email)
        {
            textBoxEmail.Text = email;
        }
    }
}
