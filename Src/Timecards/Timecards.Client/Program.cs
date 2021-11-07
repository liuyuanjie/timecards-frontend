using System;
using System.Configuration;
using System.Windows.Forms;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;

namespace Timecards.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var apiRequestFactory =
                new ApiRequestFactory(new Uri(ConfigurationSettings.AppSettings["baseUrl"]));
            Application.Run(new FormLogin(apiRequestFactory));
        }
    }
}
