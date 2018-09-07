using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServ_SuperSocket
{
    public class SocketMessage
    {
        public string Action { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// SOCKET返回的消息字段
        /// </summary>
        public string ServerMessage { get; set; }
        public bool IsLive { get; set; }
        /// <summary>
        /// 裁判端发送数据类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 对阵数据
        /// </summary>
        public DataObj Data { get; set; }
        /// <summary>
        /// SOCKET 裁判端发送的数据格式
        /// </summary>
        public class DataObj
        {
            /// <summary>
            /// 是否是拆场
            /// </summary>
            public bool IsSplit { get; set; }
            /// <summary>
            /// 是否是团队
            /// </summary>
            public bool IsTeam { get; set; }            
            /// <summary>
            /// 对阵ID
            /// </summary>
            public string LoopId { get; set; }

        }
    }

}
