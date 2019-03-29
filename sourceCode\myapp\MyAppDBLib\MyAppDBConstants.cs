using System;
using System.Text;

namespace MyAppDBLib
{
    public sealed class MyAppDBConstants
    {
        #region "Table : Student"

        public const String TABLE_STUDENT = "Student";

        public const String STUDENT_PK = "ID";
        public const String STUDENT_NAME = "StudentName";
        public const String STUDENT_AGE = "StudentAge";
        public const String STUDENT_STD = "StudentStandard";

        public const String SP_STUDENT_INSERTUPDATE = "InsertUpdateStudent";
        public const String SP_STUDENT_IN_STUDENT_ID = "studentID";


        #endregion



    }
}
