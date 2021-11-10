using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Timecards.Application;
using Timecards.Infrastructure;

namespace Timecards.Client
{
    public partial class FormMain : Form
    {
        public FormMain(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();
            InitializaData();
        }

        private void InitializaData()
        {
            labelFirstName.Text = AccountStore.Account.FirstName;
            labelLastName.Text = AccountStore.Account.LastName;
            labelEmail.Text = AccountStore.Account.Email;
            LabelUserName.Text = AccountStore.Account.UserName;
            labelRole.Text = AccountStore.Account.Role;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }
    }
}
