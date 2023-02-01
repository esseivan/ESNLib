using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace ESNLib.Tools
{
    /// <summary>
    /// Manage plugins
    /// </summary>
    [Obsolete("No use and bad implementation. Use another library from the web if you really need")]
    public class PluginsManager
    {
        /// <summary>
        /// The list of registered plugins
        /// </summary>
        public List<Plugin> RegisteredPlugins { get; set; }

        /// <summary>
        /// The caller of the plugins
        /// </summary>
        public PluginsCaller Caller { get; set; }

        public event EventHandler<PluginEventArgs> PluginEventReceived;

        public PluginsManager()
        {
            RegisteredPlugins = new List<Plugin>();
            Caller = new PluginsCaller(RegisteredPlugins, this);
        }

        public PluginsManager(PluginsCaller Caller)
        {
            RegisteredPlugins = new List<Plugin>();
            this.Caller = Caller;
        }

        public PluginsManager(Plugin Plugin)
            : this()
        {
            RegisterPlugin(Plugin);
        }

        /// <summary>
        /// Register a new plugin
        /// </summary>
        public void RegisterPlugin(Plugin Plugin)
        {
            RegisteredPlugins.Add(Plugin);
        }

        /// <summary>
        /// Unregister a pugin
        /// </summary>
        public void UnregisterPlugin(Plugin Plugin)
        {
            RegisteredPlugins.Remove(Plugin);
        }

        /// <summary>
        /// Call event on enabled plugins
        /// </summary>
        public void CallEvent(PluginsCaller.EventType type)
        {
            Caller.CallPluginEvent(type);
        }

        internal void SendEvent(object o, PluginEventArgs e)
        {
            PluginEventReceived?.Invoke(o, e);
        }
    }

    /// <summary>
    /// Plugins caller for the IPlugin interface
    /// </summary>
    [Obsolete("No use and bad implementation. Use another library from the web if you really need")]
    public class PluginsCaller
    {
        /// <summary>
        /// Global enable
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// List of all registered plugins (must be linked to the object from PluginsManager)
        /// </summary>
        public List<Plugin> list { get; internal set; }

        private PluginsManager pluginsManager;

        /// <summary>
        /// List of suppoted events by this caller
        /// </summary>
        public enum EventType
        {
            NONE = 0,

            OnStart,
            OnStop,
            OnResume,
            OnPause,
            OnRestart,
            OnTick,
        }

        /// <summary>
        /// Initialize a new plugin caller to call plugins
        /// </summary>
        /// <param name="list">The list of plugins</param>
        /// <param name="pluginsManager">Manager of the plugins</param>
        public PluginsCaller(List<Plugin> list, PluginsManager pluginsManager)
        {
            this.list = list;
            this.pluginsManager = pluginsManager;
            Enabled = true;
        }

        /// <summary>
        /// Check wheter call should be done
        /// </summary>
        /// <returns></returns>
        private bool CheckValid()
        {
            // If no plugin or disabled, return false
            if (list.Count == 0 || !Enabled)
                return false;
            return true;
        }

        /// <summary>
        /// Call the specified event for all the enabled plugins
        /// </summary>
        public void CallPluginEvent(EventType type)
        {
            if (!CheckValid())
                return;

            foreach (Plugin plugin in list)
            {
                if (!plugin.Enabled)
                    continue;

                switch (type)
                {
                    case EventType.OnStart:
                        plugin.OnStart();
                        break;
                    case EventType.OnStop:
                        plugin.OnStop();
                        break;
                    case EventType.OnResume:
                        plugin.OnResume();
                        break;
                    case EventType.OnPause:
                        plugin.OnPause();
                        break;
                    case EventType.OnRestart:
                        plugin.OnRestart();
                        break;
                    case EventType.OnTick:
                        plugin.OnTick();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Send event to user
        /// </summary>
        internal void SendEvent(object o, PluginEventArgs e)
        {
            pluginsManager.SendEvent(o, e);
        }
    }

    /// <summary>
    /// Plugin event arguments (to user)
    /// </summary>
    [Obsolete("No use and bad implementation. Use another library from the web if you really need")]
    public class PluginEventArgs : EventArgs
    {
        public string Msg;

        public PluginEventArgs()
        {
            this.Msg = string.Empty;
        }

        public PluginEventArgs(string Msg)
        {
            this.Msg = Msg;
        }
    }

    /// <summary>
    /// Interface to be implemented to be reconized as a plugin
    /// </summary>
    [Obsolete("No use and bad implementation. Use another library from the web if you really need")]
    public abstract class Plugin
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; } = "Undefined";

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; } = -1;

        /// <summary>
        /// Enabled
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Caller to send events to user
        /// </summary>
        public PluginsCaller Caller { get; set; }

        public Plugin() { }

        public Plugin(PluginsCaller Caller)
        {
            this.Caller = Caller;
            this.Enabled = true;
        }

        public virtual void OnStart() { }

        public virtual void OnStop() { }

        public virtual void OnResume() { }

        public virtual void OnPause() { }

        public virtual void OnRestart() { }

        public virtual void OnTick() { }

        protected void SendEvent(PluginEventArgs e)
        {
            if (Caller == null)
                return;

            Caller.SendEvent(this, e);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
