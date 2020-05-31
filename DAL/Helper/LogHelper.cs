using System;
using System.IO;

namespace DAL
{
    public class LogHelper
    {
        /// <summary>
        /// 将信息写入日志
        /// </summary>
        public static void WriteLog(string msg)
        {
            FileStream fs = new FileStream("/var/log/fatesky.log", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString() + " " + msg);
            sw.Close();
            fs.Close();
        }
    }
}
