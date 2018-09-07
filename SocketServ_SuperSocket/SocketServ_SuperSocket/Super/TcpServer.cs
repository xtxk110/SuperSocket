using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace SocketServ_SuperSocket
{
    public class TcpServer : AppServer<TcpSession, MyRequestInfo>
    {
        public TcpServer() :base(new DefaultReceiveFilterFactory<Terminator, MyRequestInfo>()){
            var test = new DefaultReceiveFilterFactory<Terminator, MyRequestInfo>();
            
        }
    }
}
