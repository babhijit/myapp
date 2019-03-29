using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Utility
{
    public class Logger
    {
        private static String m_logFileName = "MyApp.log";
        public static String LogFileName
        {
            get { return m_logFileName; }
            set { m_logFileName = value; }
        }

        protected Logger() 
        {
            DateTime dtNow = DateTime.Now;
            String timeString = String.Format("{0}-{1}-{2}", dtNow.Hour, dtNow.Minute, dtNow.Second);
            m_logFileName = String.Format("{0}-{1}-{2}.log", DateTime.Now.ToLongDateString(), timeString, m_logFileName);
            String logFileUrl = String.Format("{0}\\{1}", Directory.GetCurrentDirectory(), m_logFileName);

            m_logWriter = new StreamWriter(logFileUrl, true);
        }

        private static Logger m_logger = null;
        public static Logger Instance
        {
            get
            {
                if (m_logger == null)
                    m_logger = new Logger();

                return m_logger;
            }
        }

        private StreamWriter m_logWriter = null;

        public virtual Boolean WriteLog(String logMessage)
        {
            Boolean writtenLog = false;

            try
            {
                m_logWriter.WriteLine(logMessage);
                m_logWriter.Flush();

                writtenLog = true;
            }
            catch
            {
                // ?? what to do
            }

            return writtenLog;
        }

        public virtual Boolean WriteLog(Exception exception, String logLevel = "Info")
        {
            Boolean writtenLog = false;

            try
            {
                m_logWriter.Write(DateTime.Now.ToLongTimeString());
                m_logWriter.Write("\t\t");
                m_logWriter.WriteLine(exception.Message);
                m_logWriter.Write("\t\t");
                m_logWriter.WriteLine(exception.StackTrace);
                m_logWriter.Flush();

                writtenLog = true;
            }
            catch
            {
            	// ?? what to do
            }

            return writtenLog;
        }
    }
}
