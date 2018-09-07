using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SupperSocketTest.Custom
{
   public class TestSession:AppSession<TestSession,MyRequestInfo>
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        protected  override void OnSessionStarted()
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "=》【" + RemoteEndPoint + "】连接成功");
            Send(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "=》欢迎【"+RemoteEndPoint+"】");
            base.OnSessionStarted();
        }
        protected  override void OnSessionClosed(CloseReason reason)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "=》【" + RemoteEndPoint + "】断开连接");
            base.OnSessionClosed(reason);
        }
        protected override void HandleUnknownRequest(MyRequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
        }
    }
}
