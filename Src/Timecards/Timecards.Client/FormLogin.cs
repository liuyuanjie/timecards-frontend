using System.Threading;
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

        private void CallbackProcess(ResponseBase<LoginResult> loginResponse)
        {
            if (loginResponse.ResponseState.IsSuccess)
            {
                FormMain formMain = new FormMain(_apiRequestFactory);
                this.Hide();
                formMain.ShowDialog();
            }
            else
            {
                MessageBox.Show(
                    loginResponse.ResponseState.ResponseStateMessage.OutputResponseMessage(),
                    "Login Failed",
                    MessageBoxButtons.OK);
            }
        }

        public void CallbackFromRegister(string email)
        {
            textBoxEmail.Text = email;
            textBoxPassword.Focus();
        }
    }
}
