using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBLib;
using Utility;
using MyPlugin;

namespace MyApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppConfig.Instance.AppName = "My App";
            // Logger.LogFileName = Custom log file path;

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(HandleUnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(HandleThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Setup DB Connection
            ConnectToDatabase();

            Application.Run(new MainForm());

            // Close the database connection at the time of exit
            if (GenericServer.ServerInstance.IsOpen)
                GenericServer.ServerInstance.DBConnection.Close();
        }

        private static GenericServer GetDBConnection()
        {
            GenericServer genServer = null;

            switch (AppConfig.Instance.DBType)
            {
                case GlobalConstants.DB_MYSQL:
                    genServer = new MySqlSrvr();
                    break;
                case GlobalConstants.DB_SQLSVR:
                    genServer = new SqlSrvr();
                    break;
                default:
                    throw new MyAppOperationNotAllowed(String.Format("Database Server: {0} not supported", AppConfig.Instance.DBType));
            }

            GetDBSettings();

            return genServer;
        }

        private static void GetDBSettings()
        {
            GenericServer.ServerInstance.ServerName = AppConfig.Instance.DBServer;
            GenericServer.ServerInstance.Database = AppConfig.Instance.Database;
            GenericServer.ServerInstance.UserID = AppConfig.Instance.DBUser;
            GenericServer.ServerInstance.Password = AppConfig.Instance.DBPassword;
        }

        static void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                Logger.Instance.WriteLog(exception, GlobalConstants.SEVERITY_ERROR);
                ShowExceptionDetails(exception);
            }

            Application.Exit();
        }

        private static void HandleThreadException(Object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;

            if (exception != null)
            {
                Logger.Instance.WriteLog(exception, GlobalConstants.SEVERITY_ERROR);
                ShowExceptionDetails(exception);
            }

            Application.Exit();
        }

        static void ShowExceptionDetails(Exception Ex)
        {
            // Do logging of exception details
            MessageBox.Show(Ex.Message, Ex.TargetSite.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void ConnectToDatabase()
        {
            try
            {
                GetDBConnection();
                GenericServer.ServerInstance.DBConnection.Open();
            }
            catch (System.Exception)
            {
                DBSetupForm dbSetupForm = new DBSetupForm();
                if (dbSetupForm.ShowDialog() != DialogResult.OK)
                {
                    throw;
                }
                // This is really poor code... till I find a way to do this check in a loop without setting dependency of DBLib in Utility!
                ConnectToDatabase();    // call it recursively to ensure connection; exception should kill the app.
            }
            
        }

    }
}
