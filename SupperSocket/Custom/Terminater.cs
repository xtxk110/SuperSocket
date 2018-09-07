using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using Newtonsoft.Json;

namespace SupperSocketTest.Custom
{
    public class Terminater : TerminatorReceiveFilter<MyRequestInfo>
    {
        private Encoding gb2312 = Encoding.GetEncoding("936");
        public Terminater():base(Encoding.UTF8.GetBytes(Environment.NewLine)) { }
        
        protected override MyRequestInfo ProcessMatchedRequest(byte[] data, int offset, int length)
        {
            byte[] full = new byte[length];
            Buffer.BlockCopy(data, offset, full, 0, length);
            return new MyRequestInfo { Message = gb2312.GetString(full.ToArray()) };
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
