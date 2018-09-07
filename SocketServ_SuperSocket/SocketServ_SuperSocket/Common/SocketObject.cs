using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SocketServ_SuperSocket
{
  public  class SocketObject:INotifyPropertyChanged,IComparable<SocketObject>
    {
        public string SessionId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public string UserCode { get; set; }

        public Socket ClientSocket { get; set; }
        public EndPoint RemoteEndpoint { get; set; }
        public int BufferSize { get; set; }
        public byte[] Buffer { get; set; }
        public string SendMessage { get; set; }
        public SocketMessage MessObj { get; set; }
        /// <summary>
        /// LoopId对应的裁判登录账号
        /// </summary>
        public string JudgeCode { get; set; }
        /// <summary>
        /// SOCKET是否连接
        /// </summary>
        private bool _isOnline;
        public bool IsOnline { get { return _isOnline; } set { _isOnline = value; if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsOnline"));
                }
            } }
        private List<byte> _partSb = new List<byte>();
        /// <summary>
        /// 存储不完整的数据包字符串
        /// </summary>
        public List<byte> PartList { get { return _partSb; } }

        public string DisplayText { get; set; }

        int IComparable<SocketObject>.CompareTo(SocketObject other)
        {
            int result = 0;
            if (other == null)
                result= 1;
            int x1 = GetNumber(this);
            int x2 = GetNumber(other);
            string w1 = GetString(this);
            string w2 = GetString(other);

            if (w1.CompareTo(w2) != 0)
                result = w1.CompareTo(w2);
            else
            {
                result = x1.CompareTo(x2);
            }
            
            return result;
        }
        private int GetNumber(SocketObject obj)
        {
            int result = 0;
            try
            {
                Match match = Regex.Match(obj.UserCode, @"\d+$");
                if (match != null)
                    result = int.Parse(match.Value);
            }
            catch { }

            return result;
        }
        private string GetString(SocketObject obj)
        {
            string result = string.Empty;
            try
            {
                Match match = Regex.Match(obj.UserCode, @"^[a-zA-Z]+");
                if (match != null)
                    result = match.Value;
            }
            catch { }
            return result;
        }
       
    }
}
