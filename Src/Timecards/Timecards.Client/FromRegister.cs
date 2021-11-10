﻿using System.Windows.Forms;
using Timecards.Application.Commands;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Client
{
    public partial class FormRegister : Form
    {
        private readonly IRegisterCommand _registerCommand;
        private readonly IApiRequestFactory _apiRequestFactory;

        public FormRegister(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();
            _apiRequestFactory = apiRequestFactory;
            _registerCommand = new RegisterCommand(apiRequestFactory);
        }

        private void linkSignIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new FormLogin(_apiRequestFactory).ShowDialog();
        }

        private void buttonSignUp_Click(object sender, System.EventArgs e)
        {
            var registerRequest = new RegisterRequest
            {
                FirstName = textBoxFirstName.Text,
                LastName = textBoxLastName.Text,
                UserName = textBoxFullName.Text,
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Text,
                ConfirmPassword = textBoxConfirmPassword.Text,
                RoleType = radioButtonAdmin.Checked ? RoleType.Admin : RoleType.Staff
            };

            _registerCommand.RegisterAsync(registerRequest, (registerResponse) => CallbackProcess(registerResponse));
        }

        private void CallbackProcess(ResponseBase<RegisterResult> registerResponse)
        {
            if (registerResponse.ResponseState.IsSuccess)
            {
                FormLogin fromLogin = new FormLogin(_apiRequestFactory);
                this.Hide();
                fromLogin.CallbackFromRegister(registerResponse.ResponseResult.Email);
                fromLogin.ShowDialog();
            }
            else
            {
                MessageBox.Show(
                    registerResponse.ResponseState.ResponseStateMessage.OutputResponseMessage(),
                    "Register Failed",
                    MessageBoxButtons.OK);
            }
        }
    }
}