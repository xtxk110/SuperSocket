using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServ_SuperSocket
{
    public class LiveMessage
    {
        /// <summary>
        /// 外层消息类型
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 直播开关是否打开
        /// </summary>
        public bool IsLive { get; set; }
        /// <summary>
        /// 裁判端发送数据类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 对阵数据
        /// </summary>
        public DataObj Data { get; set; }

        public class DataObj
        {
            public List<TeamVs> DML { get; set; }

            public class TeamVs
            {
                /// <summary>
                /// 两人对阵大比分
                /// </summary>
                public string BS { get; set; }
                /// <summary>
                /// 两人对阵小分
                /// </summary>
                public string SS { get; set; }
                /// <summary>
                /// 人员1
                /// </summary>
                public string UN1 { get; set; }
                /// <summary>
                /// 人员2(双打)
                /// </summary>
                public string UN2 { get; set; }
            }
        }
    }
}
