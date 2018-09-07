using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketBase.Config;

namespace SupperSocketTest.Custom
{
    public class TestServer:AppServer<TestSession,MyRequestInfo>
    {
        //public TestServer():base(new CommandLineReceiveFilterFactory(Encoding.Default,new BasicRequestInfoParser(":",",")))
        //{
        //}
        public TestServer():base(new DefaultReceiveFilterFactory<Terminater,MyRequestInfo>()){}

    }
}
