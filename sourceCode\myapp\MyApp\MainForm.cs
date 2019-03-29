using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utility;
using System.Reflection;
using MyPlugin;
using SupportLib;
namespace MyApp
{
    using PluginList = List<IMyPlugin>;
    using System.IO;

    public partial class MainForm : Form
    {
        private Dictionary<Int32, Form> m_moduleMap = new Dictionary<Int32, Form>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // This is going to be a slow process... in the future progress info is to be shown
            LoadPlugins();

            // if no plugins found ... exit after prompting the due message
            if (this.m_moduleMap.Count == 0)
            {
                MessageBox.Show("No Modules Found!\nApplication Exiting.", AppConfig.Instance.AppName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }
            else
            {
                // By default select the first item
                this.modulesListBox.SelectedIndex = 0;
            }

        }

        private void LoadPlugins()
        {
            PluginList plugins = new PluginList();

            String pluginDirectory = Application.StartupPath;

            // low lookup in plugin directory
            String[] pluginFiles = Directory.GetFiles(pluginDirectory, "*.DLL");

            foreach (String pluginFile in pluginFiles)
            {
                Assembly pluginAssembly = Assembly.LoadFile(pluginFile);

                AttachPlugins(plugins, pluginAssembly);
            }

            AttachPlugins(plugins);
        }

        private void AttachPlugins(PluginList plugins, Assembly pluginAssembly)
        {
            try
            {
                List<Type> availableTypes = new List<Type>();
                availableTypes.AddRange(pluginAssembly.GetTypes());

                // Get the object that implements the IPluginForm interface
                // and PluginAttribute
                List<Type> pluginForms = availableTypes.FindAll(delegate(Type t)
                {
                    List<Type> interfaceTypes = new List<Type>(t.GetInterfaces());
                    Object[] arr = t.GetCustomAttributes(typeof(MyPluginAttribute), true);

                    return !(arr == null || arr.Length == 0) && interfaceTypes.Contains(typeof(IMyPlugin));
                });

                foreach (Type pluginForm in pluginForms)
                {
                    if (typeof(IMyPlugin).IsAssignableFrom(pluginForm))
                    {
                        try
                        {
                            IMyPlugin gsaPlugin = Activator.CreateInstance(pluginForm) as IMyPlugin;
                            if (gsaPlugin != null)
                                plugins.Add(gsaPlugin);
                        }
                        catch (MyAppAccessDeniedException ex)
                        {
                            Logger.Instance.WriteLog(ex);
                            // user does not permission to load this
                        }
                        catch (Exception ex)
                        {
                            MyAppAccessDeniedException accessDeniedException = ex.InnerException as MyAppAccessDeniedException;
                            // suppress MyAppAccessDeniedException
                            if (accessDeniedException == null)
                            {
#if DEBUG
                                MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                                Logger.Instance.WriteLog(accessDeniedException);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.WriteLog(ex.Message);
            }
        }

        private void AttachPlugins(PluginList plugins)
        {
            foreach (IMyPlugin plugin in plugins)
            {
                Form pluginForm = plugin as Form;
                if (pluginForm != null)
                {
                    System.Attribute[] attrs = System.Attribute.GetCustomAttributes(plugin.GetType());
                    String pluginName = String.Empty;
                    foreach (Attribute attr in attrs)
                    {
                        if (attr is MyPluginAttribute)
                        {
                            MyPluginAttribute pluginAttribute = attr as MyPluginAttribute;
                            pluginName = pluginAttribute.Name;
                            break;
                        }
                    }

                    // Attribute Name is given preference
                    if (String.IsNullOrEmpty(pluginName))
                    {
                        pluginName = pluginForm.Name;
                    }

                    if (String.IsNullOrEmpty(pluginName))
                    {
                        throw new MyAppException("Plugin Name can not be empty!");
                    }

                    Int32 index = this.modulesListBox.Items.Add(pluginName);
                    this.m_moduleMap.Add(index, pluginForm);
                    PrepareAndAttachForm(pluginForm);
                }
            }
        }

        // Attach module form to main application panel and
        //  add a menu item dynamically
        private void PrepareAndAttachForm(Form moduleChildForm)
        {
            moduleChildForm.AutoScroll = true;
            moduleChildForm.AutoSize = false;
            moduleChildForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            moduleChildForm.MinimizeBox = false;
            moduleChildForm.ShowIcon = false;
            moduleChildForm.ShowInTaskbar = false;
            moduleChildForm.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            moduleChildForm.Dock = DockStyle.Fill;
            moduleChildForm.TopLevel = false;
            moduleChildForm.Parent = this;
            moduleChildForm.Visible = true;

            this.splitContainer.Panel2.Controls.Add(moduleChildForm);

            // By default add the module form to receive shutdown event
            IMyPlugin plugin = moduleChildForm as IMyPlugin;
            if (plugin != null)
                EventPublisher.Instance.AddSubscriber(Utility.GlobalConstants.APP_SHUTDOWN, plugin);
        }

        private void modulesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // first make all the child forms in pane 2 invisible
            foreach (Control ctrl in this.splitContainer.Panel2.Controls)
            {
                if (ctrl is Form)
                {
                    ctrl.Visible = false;
                }
            }

            // make the corresponding child form visible
            Int32 moduleIndex = modulesListBox.SelectedIndex;
            m_moduleMap[moduleIndex].Visible = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Trigger Shutdown event
            MyEventArg<String> shutdownEvent = new MyEventArg<String>(Utility.GlobalConstants.APP_SHUTDOWN, String.Empty, null, Int32.MinValue, Int32.MaxValue);
            EventPublisher.Instance.DistributeEvent(Utility.GlobalConstants.APP_SHUTDOWN, shutdownEvent);
        }

    }
}
