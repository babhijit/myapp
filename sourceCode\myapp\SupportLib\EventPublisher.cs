using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPlugin;

namespace SupportLib
{
    using PluginList = List<IMyPlugin>;

    /// <summary>
    /// EventsDistibutor is a thread safe singleton class
    /// that supports routing of events to multiple plugins based on priority
    /// </summary>
    public sealed class EventPublisher
    {
        // when an event is set to GLOBAL_BROADCAST, the event is distributed to all modules
        public const String GLOBAL_BROADCAST_NO_PRIORITY = "GLOBAL_BROADCAST_NO_PRIORITY";
        // when an event is set to GLOBAL_BROADCAST, the event is distributed to all modules (but is priority based)
        public const String GLOBAL_BROADCAST = "GLOBAL_BROADCAST";

        private readonly Object s_subscriberLock = new Object();
        private Dictionary<String, PluginList> m_Subscribers = new Dictionary<String, PluginList>();

        public void AddSubscriber(String eventName, IMyPlugin plugin)
        {
            lock(s_subscriberLock)
            {
                PluginList pluginList;
                if (!m_Subscribers.TryGetValue(eventName, out pluginList))
                {
                    pluginList = new PluginList();
                    m_Subscribers.Add(eventName, pluginList);
                }

                pluginList.Add(plugin);
            }
        }

        public Boolean RemoveSubscriber(String eventName, IMyPlugin plugin)
        {
            Boolean removedSubscriber = false;
            lock(s_subscriberLock)
            {
                PluginList pluginList;
                if (!m_Subscribers.TryGetValue(eventName, out pluginList))
                {
                    throw new Exception(String.Format("{0} is not found!", eventName));
                }

                removedSubscriber = pluginList.Remove(plugin);
            }
            
            return removedSubscriber;
        }

        public void DistributeEvent(String eventName, MyEventArgBase e)
        {
            if (Broadcast(eventName, e))
                return;

            // Not a broadcast; process event
            PluginList pluginList = new PluginList();

            lock (s_subscriberLock)
            {
                PluginList tmpList;
                if (m_Subscribers.TryGetValue(eventName, out tmpList))
                {
                    // Deep copy
                    pluginList.AddRange(tmpList);
                }
            }

            foreach (IMyPlugin plugin in pluginList)
            {
                if ((plugin.EventPriority <= e.CeilingPriority) || (plugin.EventPriority >= e.FloorPriority))
                {
                    // the source should not be involved.
                    if(!plugin.Equals(e.MyPlugin))
                        plugin.HandleEvent(eventName, e);
                }
            }
        }

        private Boolean Broadcast(String eventName, MyEventArgBase e)
        {
            Boolean isBroadcasted = false;

            Boolean isPriorityBased = false;

            switch (eventName)
            {
                case GLOBAL_BROADCAST:
                    isPriorityBased = true;
                    break;
                case GLOBAL_BROADCAST_NO_PRIORITY:
                    isPriorityBased = false;
                    break;
                default:
                    // This is not a broadcast message
                    return false;
            }

            List<PluginList> pluginLists = new List<PluginList>();
            lock(s_subscriberLock)
            {
                List<PluginList> tmpLists;
                tmpLists = m_Subscribers.Values.ToList<PluginList>();

                // Deep Copy
                pluginLists.AddRange(tmpLists);
            }

            Boolean canBroadcast = isPriorityBased;
            foreach (PluginList pluginList in pluginLists)
            {
                foreach (IMyPlugin plugin in pluginList)
                {
                    if (isPriorityBased)
                    {
                        canBroadcast = ((plugin.EventPriority <= e.CeilingPriority) || (plugin.EventPriority >= e.FloorPriority));
                    }

                    if (canBroadcast)
                    {
                        plugin.HandleEvent(eventName, e);
                    }
                }
            }
            

            return isBroadcasted;
        }

        // Providing Singleton Behaviour

        private EventPublisher() { }

        private static readonly Object s_lock = new Object();
        private static volatile EventPublisher s_instance = null;
        public static EventPublisher Instance
        {
            get 
            {
                lock (s_lock)
                {
                    if (s_instance == null)
                    {
                        s_instance = new EventPublisher();
                    }
                }

                return s_instance; 
            }
        }
    }
}
