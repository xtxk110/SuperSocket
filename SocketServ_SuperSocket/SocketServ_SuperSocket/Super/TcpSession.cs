using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;

namespace SocketServ_SuperSocket
{
    public class TcpSession:AppSession<TcpSession,MyRequestInfo>
    {
        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
        }
        protected override void OnSessionClosed(CloseReason reason)
        {
            base.OnSessionClosed(reason);
        }
        protected override void HandleUnknownRequest(MyRequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
        }
    }
}
