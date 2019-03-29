using System;
using System.Text;

namespace Utility
{
    public sealed class RecordLevelEventArgs : EventArgs
    {
        private int m_ID;
        public Int32 ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public RecordLevelEventArgs(Int32 iD) { m_ID = iD; }
        public RecordLevelEventArgs() : this(0) { }

    }
}
