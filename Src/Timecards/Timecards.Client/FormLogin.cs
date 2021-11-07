using System.Windows.Forms;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;

namespace Timecards.Client
{
    public partial class FormLogin : Form
    {
        private readonly IApiRequestFactory _apiRequestFactory;

        public FormLogin(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
            InitializeComponent();
        }

        private void linkCreateAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegister formRegister = new FormRegister();
            this.Hide();
            formRegister.ShowDialog();
        }

        private void buttonSignIn_Click(object sender, System.EventArgs e)
        {
            var identityService = new IdentityService(_apiRequestFactory);
            identityService.AsyncLogin(new LoginRequest
            {
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Text
            }, (loginResponse) =>
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
            });

        }
    }
}
