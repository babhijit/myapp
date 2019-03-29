using System;
using System.Text;

namespace DBLib
{
    // Global database constants to be used in generic level
    public class DBConstants
    {
        // We are using the same [out] param name for inserting/updating record through an SP
        public const String SP_INSERT_OUT_INSERTEDID = "InsertedID";
    }
}
