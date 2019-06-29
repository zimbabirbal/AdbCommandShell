using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdbProject.Constant
{
    public class AdbCommands
    {
        public const string ADB_DEVICES_LIST = "devices -l";
        public const string ADB_LOGCAT = "adb -s {0} logcat";
        public const string ADB_TSAL_LOGCAT = @"adb -s {0} logcat | grep 'TSALog'";
    }
}
