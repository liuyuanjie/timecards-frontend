using System;
using System.Windows.Forms;
using Timecards.Application.Commands;
using Timecards.Application.Commands.Login;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

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
            _loginCommand = new BWLoginCommand(apiRequestFactory);
        }

        private void linkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegister formRegister = new FormRegister(_apiRequestFactory);
            Hide();
            formRegister.ShowDialog();
        }

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            var loginRequest = new LoginRequest
            {
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Text
            };

            _loginCommand.LoginAsync(loginRequest, loginResponse => CallbackProcess(loginResponse));
        }

        private void CallbackProcess(ResponseBase<LoginResult> loginResponse)
        {
            if (!loginResponse.ResponseState.IsSuccess)
            {
                MessageBox.Show(
                    loginResponse.ResponseState.ResponseStateMessage.OutputResponseMessage(),
                    "Login Failed",
                    MessageBoxButtons.OK);
                return;
            }

            ShowFromMain();
        }

        private void ShowFromMain()
        {
            FormNavigate formMain = new FormNavigate(_apiRequestFactory);
            Hide();
            formMain.ShowDialog();
        }

        public void SetEmail(string email)
        {
            textBoxEmail.Text = email;
            textBoxPassword.Focus();
        }
    }
}
