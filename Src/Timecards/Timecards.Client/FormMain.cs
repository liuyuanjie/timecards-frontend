using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Timecards.Infrastructure;

namespace Timecards.Client
{
    public partial class FormMain : Form
    {
        public FormMain(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
