using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlugin;

namespace SupportLib
{
    public class MyEventArgBase : EventArgs
    {
        public String EventName
        {
            get;
            protected set;
        }

        public Int32 FloorPriority
        {
            get;
            protected set;
        }

        public Int32 CeilingPriority
        {
            get;
            protected set;
        }

        // For a broadcast set this property to null
        public IMyPlugin MyPlugin
        {
            get;
            private set;
        }

        public MyEventArgBase(String eventName, IMyPlugin source, Int32 floorPriority = -1, Int32 ceilingPrority = 0)
        {
            EventName = eventName;
            MyPlugin = source;
            FloorPriority = floorPriority;
            CeilingPriority = ceilingPrority;
            
        }
    }

    public sealed class MyEventArg<T> : MyEventArgBase
    {
        public T Value
        {
            get;
            private set;
        }

        public MyEventArg(String eventName, T value, IMyPlugin source = null, Int32 floorPriority = -1, Int32 ceilingPrority = 0) :
            base(eventName, source,floorPriority, ceilingPrority)
        {
            Value = value;
        }
    }
}
