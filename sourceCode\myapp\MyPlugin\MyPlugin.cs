using System;
using System.Text;
using System.Windows.Forms;

namespace MyPlugin
{
    public interface IMyPlugin
    {
        // Plugin "child" form to be attached to the main window form
        Form PluginForm { get; }

        String PluginName { get; set; }
        String Description { get; set; }
        
        // TODO: Method for extracting image


        // Subscription Methods
        Int32 EventPriority { get; set; }
        void HandleEvent(String eventName, EventArgs e);
    }
}
