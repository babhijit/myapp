using System;
using System.Text;

namespace MyPlugin
{
    // All plugin forms to qualify as a plugin for the application framework
    //  must have this attribute!
    public class MyPluginAttribute : Attribute
    {
        private String m_Name;
        public String Name
        {
            get { return m_Name; }
        }

        private String m_Description;
        public String Description
        {
            get { return m_Description; }
        }

        public MyPluginAttribute(String name, String description = "")
        {
            m_Name = name;
            m_Description = description;
        }
    }
}
