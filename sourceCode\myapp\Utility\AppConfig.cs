using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using System.IO;

namespace Utility
{
    public sealed class AppConfig
    {
        private IniFile iniFile = null;

        private static AppConfig theAppConfig = null;
        private AppConfig()
        {
            if (iniFile == null)
            {
                // special case for IDE loading.. only for debug mode
                String filePath = Directory.GetCurrentDirectory() + "\\" + GlobalConstants.APP_CONFIG_FILE_NAME;
                
                if (!File.Exists(filePath))
                {
                    FileStream fs = File.Create(filePath);
                    fs.Close();
                }

                iniFile = new IniFile(filePath);
            }
        }

        public static AppConfig Instance
        {
            get
            {
                if (theAppConfig == null)
                    theAppConfig = new AppConfig();

                return theAppConfig;
            }
        }

        #region DB Config

        private String dbServer;
        public String DBServer
        {
            get
            {
                dbServer = GetStringConfig(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_SERVER_PARAM);
                return dbServer;
            }

            set
            {
                dbServer = value;
                SaveConfiguration(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_SERVER_PARAM, dbServer);
            }
        }

        private String database;
        public String Database
        {
            get
            {
                database = GetStringConfig(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_DATABASE_PARAM);
                return database;
            }

            set
            {
                database = value;
                SaveConfiguration(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_DATABASE_PARAM, database);
            }
        }


        private String dbUser = String.Empty;
        public String DBUser
        {
            get
            {
                dbUser = GetStringConfig(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_USERID_PARAM);
                return dbUser;
            }

            set
            {
                dbUser = value;
                SaveConfiguration(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_USERID_PARAM, dbUser);
            }
        }

        private String dbPassword = String.Empty;
        public String DBPassword
        {
            get
            {
                String encryptedPassword = GetStringConfig(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_PASSWORD_PARAM);
                if (!String.IsNullOrEmpty(encryptedPassword))
                    dbPassword = EncDec.Decrypt(encryptedPassword, GlobalConstants.ENCRYPTION_KEY);
                return dbPassword;
            }

            set
            {
                String password = value;
                if (!String.IsNullOrEmpty(password))
                    dbPassword = EncDec.Encrypt(password, GlobalConstants.ENCRYPTION_KEY);
                SaveConfiguration(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_PASSWORD_PARAM, dbPassword);
            }
        }

        #endregion


        #region Global App Specific Settings

        private String m_appName = "MyApp";
        public String AppName
        {
            get { return m_appName; }
            set { m_appName = value; }
        }

        private String m_dbType = String.Empty;
        public String DBType
        {
            get 
            {
                m_dbType = GetStringConfig(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_DBTYPE_PARAM);
                return m_dbType; 
            }

            set 
            {
                m_dbType = value;
                SaveConfiguration(GlobalConstants.CONFIG_DB_SECTION, GlobalConstants.DB_DBTYPE_PARAM, value);
            }
        }

        #endregion

        #region Email Options

        private String smtpHost = String.Empty;
        public String SmtpHost
        {
            get
            {
                smtpHost = GetStringConfig(GlobalConstants.CONFIG_SMTP_SECTION, GlobalConstants.SMTP_HOST_PARAM);
                return smtpHost;
            }
            set
            {
                smtpHost = value;
                SaveConfiguration(GlobalConstants.CONFIG_SMTP_SECTION, GlobalConstants.SMTP_HOST_PARAM, smtpHost);
            }
        }

        private Int32 smtpPort = 0;
        public Int32 SmtpPort
        {
            get
            {
                smtpPort = GetIntConfig(GlobalConstants.CONFIG_SMTP_SECTION, GlobalConstants.SMTP_PORT_PARAM);
                return smtpPort;
            }
            set
            {
                smtpPort = value;
                SaveConfiguration(GlobalConstants.CONFIG_SMTP_SECTION, GlobalConstants.SMTP_PORT_PARAM, smtpPort.ToString());
            }
        }

        private String smtpUser = String.Empty;
        public String SmtpUser
        {
            get
            {
                smtpUser = GetStringConfig(GlobalConstants.CONFIG_SMTP_SECTION, GlobalConstants.SMTP_USERNAME_PARAM);
                return smtpUser;
            }
            set
            {
                smtpUser = value;
                SaveConfiguration(GlobalConstants.CONFIG_SMTP_SECTION, GlobalConstants.SMTP_USERNAME_PARAM, smtpUser);
            }
        }

        private String smtpPwd = String.Empty;
        public String SmtpPwd
        {
            get
            {
                String smtpPwd = GetStringConfig(GlobalConstants.CONFIG_SMTP_SECTION, GlobalConstants.SMTP_PASSWORD);
                if (!String.IsNullOrEmpty(smtpPwd))
                {
                    smtpPwd = EncDec.Decrypt(smtpPwd, GlobalConstants.ENCRYPTION_KEY);
                }

                return smtpPwd;
            }
            set
            {
                smtpPwd = value;
                if (!String.IsNullOrEmpty(smtpPwd))
                    smtpPwd = EncDec.Encrypt(smtpPwd, GlobalConstants.ENCRYPTION_KEY);
                SaveConfiguration(GlobalConstants.CONFIG_SMTP_SECTION, GlobalConstants.SMTP_PASSWORD, smtpPwd);
            }
        }

        #endregion


        #region Globals

        private Boolean m_notificationFlag = true;
        public Boolean NotificationFlag
        {
            get
            {
                Int32 flag = GetIntConfig(GlobalConstants.APP_CONFIG_GLOBAL_SECTION, GlobalConstants.NOTIFICATION_FLAG);
                m_notificationFlag = (flag != 0);

                return m_notificationFlag;
            }

            set
            {
                SaveConfiguration(GlobalConstants.APP_CONFIG_GLOBAL_SECTION, GlobalConstants.NOTIFICATION_FLAG, value ? "1" : "0");
            }
        }

        private Int32 m_notificationBaloon = 5000;
        public Int32 NotificationBalloonTimeout
        {
            get
            {
                m_notificationBaloon = GetIntConfig(GlobalConstants.APP_CONFIG_GLOBAL_SECTION, GlobalConstants.NOTIFICATION_TIMEOUT, 5000);
                return m_notificationBaloon;
            }

            set
            {
                SaveConfiguration(GlobalConstants.APP_CONFIG_GLOBAL_SECTION, GlobalConstants.NOTIFICATION_TIMEOUT, m_notificationBaloon.ToString());
            }
        }


        private Boolean m_enableSysLog = false;
        public Boolean EnableSysLog
        {
            get
            {
                Int32 flag = GetIntConfig(GlobalConstants.APP_CONFIG_GLOBAL_SECTION, GlobalConstants.SYSLOG_FLAG);
                m_enableSysLog = (flag != 0);

                return m_enableSysLog;
            }

            set
            {
                SaveConfiguration(GlobalConstants.APP_CONFIG_GLOBAL_SECTION, GlobalConstants.SYSLOG_FLAG, value ? "1" : "0");
            }
        }

        private Boolean m_emailFlag = true;
        public Boolean EmailFlag
        {
            get
            {
                Int32 flag = GetIntConfig(GlobalConstants.APP_CONFIG_GLOBAL_SECTION, GlobalConstants.NOTIFICATION_FLAG);
                m_emailFlag = (flag != 0);

                return m_emailFlag;
            }

            set
            {
                SaveConfiguration(GlobalConstants.APP_CONFIG_GLOBAL_SECTION, GlobalConstants.NOTIFICATION_FLAG, value ? "1" : "0");
            }
        }


        #endregion


        #region Save Functions

        private Int32 GetIntConfig(String section, String param, Int32 defaultValue = 0)
        {
            return iniFile.GetInt32(section, param, defaultValue);
        }

        private String GetStringConfig(String section, String param, String defaultValue = "")
        {
            return iniFile.GetString(section, param, defaultValue);
        }

        private void SaveConfiguration(String section, String param, String value)
        {
            iniFile.WriteValue(section, param, value);
        }

        #endregion

    }
}
