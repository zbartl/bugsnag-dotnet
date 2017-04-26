using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Bugsnag
{
    /// <summary>
    /// Helper class used to profile the machine / device the application is running on
    /// </summary>
    internal static class Diagnostics
    {
        /// <summary>
        /// Enum holding the different types of OS
        /// </summary>
        public enum OsType
        {
            Server,
            Desktop,
            Unknown
        }

        /// <summary>
        /// The version of the operating system the application is running
        /// </summary>
        public static readonly string DetectedOSVersion = RuntimeInformation.OSDescription;

        /// <summary>
        /// Determines if the operation system is 32 bit or 64 bit
        /// </summary>
        public static readonly string OSArchitecture = RuntimeInformation.OSArchitecture.ToString();

        /// <summary>
        /// The number of processors (cores) the device/machine has
        /// </summary>
        public static readonly string ProcessorCount = Environment.ProcessorCount + " core(s)";

        /// <summary>
        /// The name of the machine the app is running on
        /// </summary>
        public static readonly string MachineName = Environment.MachineName;

        /// <summary>
        /// The Common Language Runtime (CLR) the .NET framework is using
        /// </summary>
        public static readonly string ClrVersion = RuntimeInformation.FrameworkDescription;

        /// <summary>
        /// The hostname of the local machine
        /// </summary>
        public static readonly string HostName = Environment.MachineName;

        /// The service pack installed on the operating system
        /// </summary>
        public static readonly string ServicePack = "";

        /// <summary>
        /// Determines if the application is a 32 bit or a 64 bit process
        /// </summary>
        public static readonly string AppArchitecture = "64 bit";

    }
}
