using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESNLib.Tools.WinForms
{
    public abstract class MiscTools
    {

        /// <summary>
        /// Open the process with admin rights
        /// </summary>
        /// <param name="process"></param>
        public static void RunAsAdmin(Process process)
        {
            // Do nothing
            if (Tools.MiscTools.HasAdminPrivileges())
                return;

            // Vista or higher check
            if (Environment.OSVersion.Version.Major >= 6)
            {
                if (process.StartInfo.Verb == string.Empty)
                    process.StartInfo.Verb = "runas";
                else
                    process.StartInfo.Verb += "runas";

                process.Start();
            }
            else
                throw new SystemException("OS version not supported");
        }

        /// <summary>
        /// Close the app and run the selected one
        /// </summary>
        public static void RunAsAdmin(IAdminForm app)
        {
            RunAsAdmin(app, default);
        }

        /// <summary>
        /// Close the app and run the selected one
        /// </summary>
        public static void RunAsAdmin(IAdminForm app, string arguments)
        {
            // Do nothing
            if (Tools.MiscTools.HasAdminPrivileges())
                return;

            //Vista or higher check
            if (Environment.OSVersion.Version.Major >= 6)
            {
                // Get path
                string path = app.GetAppPath();
                Console.WriteLine(path);

                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(path, arguments);
                p.StartInfo = psi;

                if (p.StartInfo.Verb == string.Empty)
                    p.StartInfo.Verb = "runas";
                else
                    p.StartInfo.Verb += "runas";

                p.Start();
                Application.Exit();
            }
            else
                throw new SystemException("OS version not supported");
        }

        public interface IAdminForm
        {
            string GetAppPath();
        }

    }
}
