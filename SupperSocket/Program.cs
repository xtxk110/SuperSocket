using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupperSocketTest.Custom;

namespace SupperSocketTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestServer server = new TestServer();
            server.NewRequestReceived += Server_NewRequestReceived;
            if (!server.Setup("192.168.0.254", 11111))
            {
                Console.WriteLine("初始化错误,请查看日志");
                return;
            }
            if (!server.Start())
            {
                Console.WriteLine("启动失败,请查看日志");
                return;
            }
            Console.WriteLine("服务启动成功!按任意键退出");
            Console.ReadKey();
            server.Stop();

        }

        private static void Server_NewRequestReceived(TestSession session, MyRequestInfo requestInfo)
        {
            var test = requestInfo.Message;
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"=》收到【" + session.RemoteEndPoint + "】信息：" + test);
            session.Send(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "=》服务器已经收到消息");
        }
    }
}
