using ESNLib.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examples
{
    public partial class ex_plugins : Form
    {
        public ex_plugins()
        {
            InitializeComponent();
        }

        Plugin1 plugin1;
        PluginsManager manager;

        private void Ex_plugins_Load(object sender, EventArgs e)
        {
            manager = new PluginsManager();
            plugin1 = new Plugin1(manager.Caller);
            manager.PluginEventReceived += Manager_PluginEventReceived;
            manager.RegisterPlugin(plugin1);
        }

        private void Manager_PluginEventReceived(object sender, PluginEventArgs e)
        {
            Console.WriteLine(sender + " | " + e);
            label1.Text = e.Msg;
        }

        public class Plugin1 : Plugin
        {
            public Plugin1(PluginsCaller Caller) : base(Caller)
            {

            }
            
            public override void OnPause()
            {
                Console.WriteLine("Plugin1 : OnPause");
            }

            public override void OnRestart()
            {
                Console.WriteLine("Plugin1 : OnRestart");
            }

            public override void OnResume()
            {
                Console.WriteLine("Plugin1 : OnResume");
            }

            public override void OnStart()
            {
                Console.WriteLine("Plugin1 : OnStart");
                SendEvent(new PluginEventArgs("Plugin1 : OnStart event"));
            }

            public override void OnStop()
            {
                Console.WriteLine("Plugin1 : OnStop");
                SendEvent(new PluginEventArgs("Plugin1 : OnStop event"));
            }

            public override void OnTick()
            {
                Console.WriteLine("Plugin1 : OnStop");
                SendEvent(new PluginEventArgs("Plugin1 : OnStop event"));
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            manager.CallEvent(PluginsCaller.EventType.OnStart);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            manager.CallEvent(PluginsCaller.EventType.OnStop);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            manager.CallEvent(PluginsCaller.EventType.OnPause);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            manager.CallEvent(PluginsCaller.EventType.OnResume);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            manager.CallEvent(PluginsCaller.EventType.OnRestart);
        }
    }
}
