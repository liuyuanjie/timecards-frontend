using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Timecards.Client
{
    public partial class FormRegister : Form
    {
        private readonly IRegisterCommand _registerCommand;

        public FormRegister(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();
            _registerCommand = new RegisterCommand(apiRequestFactory);
        }

        private void linkSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new FormLogin().ShowDialog();
        }

        private void buttonSignUp_Click(object sender, System.EventArgs e)
        {
            var loginRequest = new RegisterRequest
            {
                FullName = textBoxFullName.Text,
                Email = textBoxEmail.Text,
                Password = textPassword.Text,
                ConfirmPassword = ConfirmPassword.Text,
                RoleType = radioRoleType.Value
            };

            _registerCommand.Login(loginRequest, () => CallbackProcess());
        }

        private void CallbackProcess()
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
    }
}
