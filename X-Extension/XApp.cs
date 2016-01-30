using System;
using System.Linq;
using Microsoft.SmallBasic.Library;
using System.Diagnostics;

namespace Jibba
{
    /// <summary>
    /// Provides ways to control apps
    /// </summary>
    [SmallBasicType]
    public static class XApp
    {
        private static string myProcess;

        /// <summary>
        /// Starts a process
        /// </summary>
        /// <param name="fileName">The full file path of the program to start</param> 
        /// <returns>The name of the process</returns>
        /// <example>XApp.StartProcess("C:\Programs\MyApp.exe")</example>       
        public static Primitive StartProcess(Primitive fileName)
        {
            return myProcess = Process.Start(fileName).ToString();
        }

        /// <summary>
        /// Starts a process with start info
        /// </summary>
        /// <param name="fileName">The full file path of the program to start</param>
        /// <param name="windowStyle">Specify the window style as hidden, maximized or minimized.
        /// Use "" to specify normal.</param>
        /// <param name="args">Specify arguments for the process. Use "" to specify nothing.</param>
        ///<returns>The name of the process.</returns>
        /// <example>XApp.StartWithStartInfo("IExplore.exe", "maximized", "www.getjibba.com")</example>
        public static Primitive StartWithStartInfo(Primitive fileName, Primitive windowStyle, Primitive args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(fileName);

            if (windowStyle.ToString().ToLower() == "hidden")
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            else if (windowStyle.ToString().ToLower() == "maximized")
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;

            else if (windowStyle.ToString().ToLower() == "minimized")
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;            
            else
                startInfo.WindowStyle = ProcessWindowStyle.Normal;

            if (args != "")
            {
                startInfo.Arguments = args;
                myProcess = Process.Start(startInfo).ToString();
            }
            else
            {
                myProcess = Process.Start(startInfo).ToString();
            }

            return myProcess;
        }

        /// <summary>
        /// Gets all the processes currently running on the PC
        /// </summary> 
        /// <param name="machineName">Specify the name of the PC</param> 
        /// <returns>an array</returns>
        /// <example>XApp.GetAllProcesses(XApp.MachineName)</example>     
        public static Primitive GetAllProcesses(Primitive machineName)
        {
            int i = 0;
            string arr = "";
            var allProcesses = Process.GetProcesses(machineName);

            foreach (var item in allProcesses)
            {
                i++;
                arr += i.ToString() + "=" + item + ";";
            }

            return arr;
        }

        /// <summary>
        /// Kills a process
        /// </summary>
        /// <param name="process">The name of the process</param> 
        /// <example>XApp.Kill("MyProgram")</example>       
        public static void Kill(Primitive process)
        {
            var allProcesses = Process.GetProcesses().ToArray();

            foreach (var item in allProcesses)
            {
                if (item.ToString() == process)
                {
                    item.Kill();
                }
            }
        }

        /// <summary>
        /// Gets the machine name of this local computer
        /// </summary> 
        /// <example>thisMachineName = XApp.MachineName</example>      
        public static Primitive MachineName
        {
            get { return Environment.MachineName; }
        }
    }
}
