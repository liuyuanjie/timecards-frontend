using System;
using System.Configuration;
using System.Windows.Forms;
using Timecards.Infrastructure;

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
            AppDomain.CurrentDomain.UnhandledException += SystemExceptionHandler;
            var apiRequestFactory =
                new ApiRequestFactory(new Uri(ConfigurationManager.AppSettings["baseUrl"]));
            System.Windows.Forms.Application.Run(new FormLogin(apiRequestFactory));
        }

        static void SystemExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception) args.ExceptionObject;
            MessageBox.Show(e.ToString(), "System Error", MessageBoxButtons.OK);
        }
    }
}
