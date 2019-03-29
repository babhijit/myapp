using System;
using System.Text;

namespace Utility
{
    /// <summary>
    /// Base Exception Class
    /// </summary>
    public class MyAppException : Exception
    {
        public MyAppException(Boolean writeLog = true) : this(String.Empty, null, writeLog) { }
        public MyAppException(String message, Boolean writeLog = true) : this(message, null, writeLog) { }
        public MyAppException(String message, Exception innerException, Boolean writeLog = true)
            : base(message, innerException)
        {
            if(writeLog)
            {
                Logger.Instance.WriteLog(this);
            }
        }
    }

    public class MyAppRecordNotFound : MyAppException
    {
        public MyAppRecordNotFound() { }
        public MyAppRecordNotFound(String message) : base(message) { }
        public MyAppRecordNotFound(String message, Exception innerException) : base(message, innerException) { }
    }

    public class MyAppRecordNotUnique : MyAppException
    {
        public MyAppRecordNotUnique() { }
        public MyAppRecordNotUnique(String message) : base(message) { }
        public MyAppRecordNotUnique(String message, Exception innerException) : base(message, innerException) { }
    }


    public class MyAppOperationNotAllowed : MyAppException
    {
        public MyAppOperationNotAllowed() { }
        public MyAppOperationNotAllowed(String message) : base(message) { }
        public MyAppOperationNotAllowed(String message, Exception innerException) : base(message, innerException) { }
    }

    public class MyAppNotImplemented : MyAppException
    {
        public MyAppNotImplemented() { }
        public MyAppNotImplemented(String message) : base(message) { }
        public MyAppNotImplemented(String message, Exception innerException) : base(message, innerException) { }
    }

    public class MyAppOperationAborted : MyAppException
    {
        public MyAppOperationAborted() { }
        public MyAppOperationAborted(String message) : base(message) { }
        public MyAppOperationAborted(String message, Exception innerException) : base(message, innerException) { }
    }


    /// <summary>
    /// To be raised when permission is not present for a given operation
    /// </summary>
    public class MyAppAccessDeniedException : MyAppException
    {
        public MyAppAccessDeniedException() { }
        public MyAppAccessDeniedException(String message) : base(message) { }
        public MyAppAccessDeniedException(String message, Exception innerException) : base(message, innerException) { }
    }

}
