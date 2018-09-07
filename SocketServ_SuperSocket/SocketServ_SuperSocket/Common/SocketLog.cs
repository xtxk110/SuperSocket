using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServ_SuperSocket
{
   public class SocketLog
    {
        /// <summary>
        /// 日志是否写入文件
        /// </summary>
        public bool FileFlag { get; set; }
        /// <summary>
        /// 非错误日志标志(只针对文件写入有效)
        /// </summary>
        public bool NotErrorFlag { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Log { get; set; }
    }
}
