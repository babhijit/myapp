using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public sealed class GlobalConstants
    {
        internal static readonly String ENCRYPTION_KEY = "MyAppEncryptionStringKey";

        public const String APP_CONFIG_FILE_NAME = "MyApp.Config";

        #region Global Settings

        public const String APP_CONFIG_GLOBAL_SECTION = "Globals";
        public const String NOTIFICATION_FLAG = "Notification";
        public const String NOTIFICATION_TIMEOUT = "NotificationTimeOut";
        public const String SYSLOG_FLAG = "SysLog";
        public const String EMAIL_FLAG = "Email";

        #endregion

        # region DB Connection Settings

        public const String CONFIG_DB_SECTION = "Database";
        public const String CONFIG_SYSLOGDB_SECTION = "SysLogDb";
        public const String DB_DBTYPE_PARAM = "DBType"; // TODO: Implement db server specific configurations
        public const String DB_DATABASE_PARAM = "Database";
        public const String DB_SERVER_PARAM = "Server";
        public const String DB_USERID_PARAM = "UserID";
        public const String DB_PASSWORD_PARAM = "Password";

        #endregion

        #region Supported Databases

        public const String DB_SQLSVR = "SqlServer";
        public const String DB_MYSQL = "MySql";

        #endregion

        #region Smtp Settings

        public const String CONFIG_SMTP_SECTION = "Smtp";
        public const String SMTP_HOST_PARAM = "SmtpHost";
        public const String SMTP_PORT_PARAM = "SmtpPort";
        public const String SMTP_USERNAME_PARAM = "SmtpUser";
        public const String SMTP_PASSWORD = "StmpPwd";

        #endregion

        #region Logging

        public const String SEVERITY_INFO = "Info";
        public const String SEVERITY_WARNING = "Warning";
        public const String SEVERITY_ERROR = "Error";

        #endregion

        #region Global App Event Name constants
        
        public const String APP_SHUTDOWN = "AppShutdown";

        #endregion
    }
}
