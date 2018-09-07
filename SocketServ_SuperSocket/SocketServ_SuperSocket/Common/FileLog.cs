using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace SocketServ_SuperSocket
{
    class FileLog
    {
        public static void WriteLog(string message,bool isErrorLog=true)
        {
            string dirPath = Application.StartupPath.TrimEnd(@"\".ToCharArray()) + @"\log";
            string logPath = string.Empty;
            if (isErrorLog)
            {
                CreateDir(dirPath);
                logPath = dirPath + @"\error_" + DateTime.Now.ToString("yyyyMMdd") + @".txt";
            }
            else
            {
                dirPath += @"\" + DateTime.Now.ToString("yyyyMMdd");
                CreateDir(dirPath);
                int hour = DateTime.Now.Hour;
                logPath =dirPath+@"\data_"+(hour%2==0?hour.ToString().PadLeft(2,'0'):(hour-1).ToString().PadLeft(2, '0')) + @".txt";
            }
            try
            {
                using (FileStream fs = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：" + message);
                    }
                }
            }catch(Exception e) {  }
            dirPath = null;logPath = null;
        }
        private static void CreateDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }
    }
}
