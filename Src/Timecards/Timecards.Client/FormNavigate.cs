using System.Windows.Forms;
using Timecards.Application;
using Timecards.Infrastructure;

namespace Timecards.Client
{
    public partial class FormNavigate : Form
    {
        private readonly IApiRequestFactory _apiRequestFactory;

        public FormNavigate(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;

            InitializeComponent();
            InitialUISetting();
        }

        private void InitialUISetting()
        {
            linkLabelOperateTimecards.Visible = AccountStore.Account.IsAdmin;

            linkLabelAddTimecards.LinkClicked += (s, e) => ShowFormMain();
            linkLabelOperateTimecards.LinkClicked += (s, e) => ShowTimecards();;

            ShowFormMain();
        }

        private void ShowTimecards()
        {
            var formMain = new FormTimecardsList.FormTimecardsList(_apiRequestFactory);
            ShowForm(formMain);
        }

        private void ShowFormMain()
        {
            var formMain = new FormMain(_apiRequestFactory);
            ShowForm(formMain);
        }
        
        private void ShowForm(Form form)
        {
            panelMain.Controls.Clear();

            form.TopLevel = false;
            panelMain.Controls.Add(form);
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();
        }
    }
}