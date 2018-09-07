using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServ_SuperSocket
{
    public class PacketObj
    {
        public SocketObject SocketObj { get; set; }
        public byte[] Packet { get; set; }
    }
}
