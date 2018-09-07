using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SocketServ_SuperSocket
{

    /// <summary>
    /// 分割符协议类
    /// </summary>
    public class Terminator : TerminatorReceiveFilter<MyRequestInfo>
    {
        public static string packet_sep = "\n";//此值会在程序启动时,可能更改

        public Terminator():base(Encoding.UTF8.GetBytes(packet_sep)) { }
        protected override MyRequestInfo ProcessMatchedRequest(byte[] data, int offset, int length)
        {
            byte[] full = new byte[length];
            Buffer.BlockCopy(data, offset, full, 0, length);
            return new MyRequestInfo { Message = Encoding.UTF8.GetString(full.ToArray()) };
        }
       
    }
    public class MyRequestInfo : IRequestInfo
    {
        public string Key { get; set; }
        /// <summary>
        /// 获取到的完整数据包的字符串
        /// </summary>
        public string Message { get; set; }
    }
}
