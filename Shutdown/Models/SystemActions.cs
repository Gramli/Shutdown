using System.Management;

namespace Shutdown.Models
{
    public static class SystemActions
    {
        /// <summary>
        /// Shutdown PC with ManagementObject Win32Shutdown method
        /// </summary>
        public static void Shutdown()
        {
            ManagementBaseObject outParameters = null;
            var sysOS = new ManagementClass("Win32_OperatingSystem");
            sysOS.Get();
            // enables required security privilege.
            sysOS.Scope.Options.EnablePrivileges = true;
            // get our in parameters
            var inParameters = sysOS.GetMethodParameters("Win32Shutdown");
            // pass the flag of 0 = System Shutdown
            inParameters["Flags"] = "1";
            inParameters["Reserved"] = "0";
            foreach (ManagementObject manObj in sysOS.GetInstances())
                outParameters = manObj.InvokeMethod("Win32Shutdown", inParameters, null);
        }

        /// <summary>
        /// Restart PC with ManagementObject Win32Shutdown method
        /// </summary>
        public static void Restart()
        {
            ManagementBaseObject outParameters = null;
            var sysOS = new ManagementClass("Win32_OperatingSystem");
            sysOS.Get();
            // enables required security privilege.
            sysOS.Scope.Options.EnablePrivileges = true;
            // get our in parameters
            var inParameters = sysOS.GetMethodParameters("Win32Shutdown");
            // pass the flag of 0 = System Shutdown
            inParameters["Flags"] = "2";
            inParameters["Reserved"] = "0";
            foreach (ManagementObject manObj in sysOS.GetInstances())
                outParameters = manObj.InvokeMethod("Win32Shutdown", inParameters, null);
        }
    }
}
