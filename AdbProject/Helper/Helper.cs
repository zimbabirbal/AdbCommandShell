using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdbProject.Helper
{
    public class Helper
    {
        public static string ExecuteAdbCommand(string adbcommand)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "adb.exe";
            //startInfo.Arguments = "get-serialno";
            startInfo.Arguments = adbcommand;
            //startInfo.Arguments = @"getprop | grep ro.boot.serialno";
            process.StartInfo = startInfo;
            process.Start();
            var output1 = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output1;
        }

        public static void OpenTerminalAndExecuteCommand (string adbcommand)
        {
            System.Diagnostics.Process.Start("CMD.exe",string.Format("/K {0}",adbcommand));
        }

        public static List<string> FilterDeviceList(string rawDeviceList)
        {
            List<string> deviceList = new List<string>();
            if (!rawDeviceList.Contains("\r\n"))
            {
                deviceList.Add("No devices found");
                return deviceList;
            }
            //var startIndex = rawDeviceList.IndexOf("\r\n");
            var temp = rawDeviceList.Substring(0);

            while (temp.Contains("\r\n"))
            {
                var startIndex = temp.IndexOf("\r\n");
                temp = temp.Substring(startIndex + 2);
                if (temp.Contains("\r\n"))
                {
                    if (temp.Length < 5)
                    {
                        temp = string.Empty;
                        continue;
                    }
                    var endIndex = temp.IndexOf("\r\n");
                    var rawData = temp.Substring(0, endIndex);
                    string[] collection = rawData.Split(' ');
                    List<string> newcollection = new List<string>();
                    int i = 0;
                    foreach (var str in collection)
                    {
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            newcollection.Add(str);
                        }
                    }
                    deviceList.Add(newcollection[0] + "  (" + newcollection[3].Remove(0, 6) + ")");
                    continue;
                }
            }

            return deviceList;
        }

        public static string SelectedDeviceSerialNum(string deviceinfo)
        {
            var serialNum = deviceinfo.Split(' ');
            return serialNum[0].Trim();
        }
    }
}
