using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServ_SuperSocket
{
    /// <summary>
    /// 连接远程SOCKET服务器
    /// </summary>
    public class SocketClient
    {
        //private static byte[] result = new byte[1024];
        private static string CloudSocketStr = DBHelper.GetCloudSocket();
        private static Socket clientSocket= new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static bool flag = false;
        private static int count = 0;//云SOCKET未打开时,连接次数标志,一般最多试连接3次
        private static readonly object obj = new object();
        /// <summary>
        /// 连接远程SOCKET服务器
        /// </summary>
        private static void ConnectRemote()
        {
            //获取服务器IP地址
            
            if (string.IsNullOrEmpty(CloudSocketStr))
            {
                System.Windows.Forms.MessageBox.Show("远程云SOCKET服务地址为空", "错误提示");
                return;
            }
            if (!CloudSocketStr.Contains(":"))
            {
                System.Windows.Forms.MessageBox.Show("远程云SOCKET服务地址不正确", "错误提示");
                return;
            }
            string[] temp = CloudSocketStr.Split(":".ToCharArray());
            int port_int = 0;
            int.TryParse(temp[1], out port_int);

            try
            {
                IPAddress ip = IPAddress.Parse(temp[0]);
                //clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(new IPEndPoint(ip, port_int)); //配置服务器IP与端口
                lock (obj)
                {
                    flag = true;
                    count = 0;
                }
                //Console.WriteLine("连接服务器成功");
            }
            catch
            {
                //clientSocket.Dispose();
                lock (obj)
                {
                    count++;
                    flag = false;
                }
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message"></param>
        public static bool SendMessage(string message)
        {
            if (!flag)
            {
                if(count<=3)//允许重新连接三次
                    ConnectRemote();
            }
                
            if (!flag)
                return false;
            //通过 clientSocket 发送数据
            byte[] sendBytes = Encoding.UTF8.GetBytes(message);
            try
            { 
                clientSocket.BeginSend(sendBytes,0,sendBytes.Length,SocketFlags.None,null,null);
                return true;
            }
            catch
            {
                lock (obj)
                {
                    count++;
                    flag = false;
                }
                
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                clientSocket.Dispose();
                clientSocket = null;
                // ConnectRemote();
                //clientSocket.Send(sendBytes);
                return false;
            }
        }

    }
}
