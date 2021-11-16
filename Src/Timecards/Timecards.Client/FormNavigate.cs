using System.Windows.Forms;
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

            ShowFormMain();
        }
        
        private void ShowTimecards()
        {
            var formMain = new FormTimecardsList(_apiRequestFactory);
            ShowForm(formMain);
        }

        private void ShowFormMain()
        {
            var formMain = new FormMain(_apiRequestFactory);
            ShowForm(formMain);
        }
        
        private void ShowForm(Form formMain)
        {
            panelMain.Controls.Clear();

            formMain.TopLevel = false;
            panelMain.Controls.Add(formMain);
            formMain.FormBorderStyle = FormBorderStyle.None;
            formMain.Dock = DockStyle.Fill;
            formMain.Show();
        }

        private void linkLabelAddTimecards_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowFormMain();
        }

        private void linkLabelOperateTimecards_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowTimecards();
        }
    }
}