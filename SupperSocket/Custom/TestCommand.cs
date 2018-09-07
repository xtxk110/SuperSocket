using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.Facility.Protocol;

namespace SupperSocketTest.Custom
{
    public class TestCommand : CommandBase<TestSession,MyRequestInfo>
    {
        public override void ExecuteCommand(TestSession session, MyRequestInfo requestInfo)
        {

            var key = requestInfo.Key;
            string mess = requestInfo.Message;

            session.Send("发送接收消息:"+mess);
        }
    }
}
