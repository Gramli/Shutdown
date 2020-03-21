using System.Management;

namespace Shutdown.Model
{
    public static class SystemActions
    {
        private static void Shutdown()
        {
            ManagementBaseObject outParameters = null;
            ManagementClass sysOS = new ManagementClass("Win32_OperatingSystem");
            sysOS.Get();
            // enables required security privilege.
            sysOS.Scope.Options.EnablePrivileges = true;
            // get our in parameters
            ManagementBaseObject inParameters = sysOS.GetMethodParameters("Win32Shutdown");
            // pass the flag of 0 = System Shutdown
            inParameters["Flags"] = "1";
            inParameters["Reserved"] = "0";
            foreach (ManagementObject manObj in sysOS.GetInstances())
                outParameters = manObj.InvokeMethod("Win32Shutdown", inParameters, null);
        }

        private static void Restart()
        {
            ManagementBaseObject outParameters = null;
            ManagementClass sysOS = new ManagementClass("Win32_OperatingSystem");
            sysOS.Get();
            // enables required security privilege.
            sysOS.Scope.Options.EnablePrivileges = true;
            // get our in parameters
            ManagementBaseObject inParameters = sysOS.GetMethodParameters("Win32Shutdown");
            // pass the flag of 0 = System Shutdown
            inParameters["Flags"] = "2";
            inParameters["Reserved"] = "0";
            foreach (ManagementObject manObj in sysOS.GetInstances())
                outParameters = manObj.InvokeMethod("Win32Shutdown", inParameters, null);
        }
    }
}
