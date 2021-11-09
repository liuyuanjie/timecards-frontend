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
            //ConfigurationManager.
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            var apiRequestFactory =
                new ApiRequestFactory(new Uri(ConfigurationManager.AppSettings["baseUrl"]));
            System.Windows.Forms.Application.Run(new FormLogin(apiRequestFactory));
        }
    }
}
