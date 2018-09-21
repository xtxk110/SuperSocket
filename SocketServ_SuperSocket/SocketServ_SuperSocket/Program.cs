using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SocketServ_SuperSocket
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        private static SuperSocketServ server = null;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            server = new SuperSocketServ();
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(server);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            server.DoLog(ex.TargetSite + "->" + ex.Message, true);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            server.DoLog(e.Exception.TargetSite + "->" + e.Exception.Message, true);
        }
    }
}
