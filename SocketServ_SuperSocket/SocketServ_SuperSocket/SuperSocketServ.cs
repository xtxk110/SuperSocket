using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;

namespace SocketServ_SuperSocket
{
    public partial class SuperSocketServ : Form
    {
        private static object obj = new object();//同步操作标志
        private Socket serverSocket = null;
        private System.Timers.Timer logTimer = null;//日志计时器
        private System.Timers.Timer packetTimer;
        private ConcurrentQueue<SocketLog> logQueue = new ConcurrentQueue<SocketLog>();//存储日志对象
        public static ConcurrentQueue<SocketLog> dataQueue = new ConcurrentQueue<SocketLog>();//存储接收到的数据对象,写入文件
        public static ConcurrentQueue<PacketObj> packetQueue = new ConcurrentQueue<PacketObj>();//socket接收数据包集合
        private List<SocketObject> socketObjList = new List<SocketObject>();//存储客户端SOCKET对象
        private BindingSource bs = new BindingSource();
        private bool flag = true;//循环检测
        private static int logCount = 0;//日志条数计数器
        private static int logMaxCount = 1000;//默认最大日志条数(假如配置文件读取错误)
        private static bool IsShowLog = false;//是否在主界面显示日志,true显示,false,不显示
        public static List<TVConfig> tvConfigListAll = new List<TVConfig>();//账号绑定的TV 关系缓存
        private Config socketConf = new Config();//socket配置对象
        private bool heartFlag = false;//心跳包数据处理标志
        private AESOperator aes = AESOperator.GetInstance();//加解密对象
        TcpServer server = null;//supersocket 服务器实例
        public SuperSocketServ()
        {
            InitializeComponent();
        }
        #region  设置SOCKET下拉框数据
        private delegate void DelegateData();
        /// <summary>
        /// 设置下拉列表数据源
        /// </summary>
        private void SetDataSource()
        {
            if (list_SocketClient.InvokeRequired)
            {
                BeginInvoke(new DelegateData(SetDataSource));
            }
            else
                bs.ResetBindings(false);

        }

        #endregion
        private void SocketServ_Load(object sender, EventArgs e)
        {
            ShowSocketConf();//显示SOCKET配置
            try
            {
                int.TryParse(ConfigurationManager.AppSettings["LOG_MAX_COUNT"], out logMaxCount);
                if(!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PACKET_SEP"]))
                {
                    Terminator.packet_sep= ConfigurationManager.AppSettings["PACKET_SEP"];
                    Terminator.packet_sep = Terminator.packet_sep.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
                }
               
            }
            catch (Exception e1) { }
            /////////////////////列表数据绑定////////////////////
            bs.DataSource=socketObjList;
            list_SocketClient.DataSource = bs;
            list_SocketClient.DisplayMember = "DisplayText";
            //socketObjList.Sort();//类里实现的比较器

        }

        /// <summary>
        /// 开启Socket监听
        /// </summary>
        private void StartSocket()
        {
            InitSocket();
            flag = true;

            logTimer = new System.Timers.Timer();
            logTimer.Interval = 150;//计时器间隔毫秒 
            logTimer.AutoReset = true;//计时器可循环使用
            logTimer.Elapsed += LogTimer_Elapsed;
            logTimer.Start();
            btn_log.Enabled = true;

            //packetTimer = new System.Timers.Timer();//SOCKET原始数据包处理计时器
            //packetTimer.Elapsed += (sender, elapsedEvent) =>
            //{
            //    PacketObj obj = null;
            //    bool result = packetQueue.TryDequeue(out obj);
            //    if (result)
            //    {
            //        string outError = string.Empty;
            //        //packetAnalyze.SeparatorAnalyze(obj.SocketObj, obj.Packet, obj.SocketObj.PartList, packet_sep, out outError);//找到完整数据包,通过事件触发,再查找余下包
            //    }
            //    packetTimer.Enabled = true;
            //};
            //packetTimer.Enabled = true;

        }

        private void LogTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            logTimer.Stop();
            ///日志
            SocketLog log = null;
            bool flag = logQueue.TryDequeue(out log);
            if (flag)//界面显示
            {
                DoLog(log.Log, log.FileFlag, log.NotErrorFlag);
                log = null;
            }
            flag = dataQueue.TryDequeue(out log);
            if (flag)//数据写入文件
            {
                DoLog(log.Log, log.FileFlag, log.NotErrorFlag);
                log = null;
            }
            logTimer.Start();
        }

        /// <summary>
        /// 初始化SUPERSOCKET,并启动监听服务
        /// </summary>
        private void InitSocket()
        {
            //////////获取地址及端口//////////////
            string endpointStr = string.Empty;
            try
            {
                endpointStr = socketConf.IntranetSocketIpAndPort;//获取SOCKET监听的地址
                tvConfigListAll = DBHelper.GetTvConfig();//获取TV SOCKET配置列表
            }
            catch (Exception e)
            {
                DoLog(e.Source + "->" + e.TargetSite + "->" + e.Message);
                return;
            }


            if (string.IsNullOrEmpty(endpointStr))
            {
                DoLog("获取的SOCKET地址为空");
                return;
            }
            else if (!endpointStr.Contains(":"))
            {
                DoLog("SOCKET地址不正确");
                return;
            }

            string[] temp = endpointStr.Split(':');
            string servIp = temp[0];
            int servPort = 0;
            int.TryParse(temp[1], out servPort);

            //////////////////SuperSocket Server初始化及启动/////////////////////////////
            server = new TcpServer();
            bool isSetup = server.Setup(new ServerConfig
            {
                Ip = servIp,
                Port = servPort,
                Mode = SocketMode.Tcp,
                SyncSend = true,
                MaxConnectionNumber=10000,
                MaxRequestLength=4000
                 
            });
            if (!isSetup)
            {
                DoLog("SOCKET初始化地址端口错误,请检查");
                server = null;
                return;
            }
            server.NewSessionConnected += Server_NewSessionConnected;
            server.NewRequestReceived += Server_NewRequestReceived;
            server.SessionClosed += Server_SessionClosed;
            if (!server.Start())
            {
                DoLog("SOCKET启动失败,请检查Config里的错误日志");
                server = null;
                return;
            }
            else
            {
                DoLog(endpointStr + "监听中......");
                this.Text = "SOCKET【" + endpointStr + "】";
            }

        }
        /// <summary>
        /// SUPERSOCKET 连接关闭事件
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        private void Server_SessionClosed(TcpSession session, SuperSocket.SocketBase.CloseReason value)
        {
            for(int i = 0; i < socketObjList.Count;i++)
            {
                var obj = socketObjList[i];
                if (obj.SessionId == session.SessionID)
                {
                    obj.IsOnline = false;
                    obj.DisplayText = obj.UserCode + "\t离线";
                    break;
                }
            }
            SetDataSource();
            //DoLog(value.ToString(), true);
        }

        /// <summary>
        /// SUPERSOCKET接收到新完整数据请求,这里是已处理粘包情况
        /// </summary>
        /// <param name="session"></param>
        /// <param name="requestInfo"></param>
        private void Server_NewRequestReceived(TcpSession session, MyRequestInfo requestInfo)
        {
            AnalyzePacket(requestInfo.Message, session);
        }

        /// <summary>
        /// SUPERSOCKET 连接成功事件
        /// </summary>
        /// <param name="session"></param>
        private void Server_NewSessionConnected(TcpSession session)
        {
            //UpdateSocket(socketObjList, session);
            
        }
        /// <summary>
        /// 有新连接时,更新集合里的SOCKET链接
        /// </summary>
        /// <param name="list">SOCKET集合</param>
        /// <param name="session">TcpSession,每一个链接</param>
        private void UpdateSocket(List<SocketObject> list,TcpSession session)
        {
            //SocketObject obj = new SocketObject();
            //obj.RemoteEndpoint = session.SocketSession.RemoteEndPoint;
            //obj.IsOnline = true;
            //obj.SessionId = session.SessionID;
            //list.Add(obj);
        }
        /// <summary>
        /// 解析完整数据包
        /// </summary>
        /// <param name="fullPacket">完整数据字符串</param>
        /// <param name="session">TcpSession,每一个链接</param>
        private void AnalyzePacket(string fullPacket,TcpSession session)
        {
            SocketMessage messObj = null;
            try
            {
                messObj = JsonConvert.DeserializeObject<SocketMessage>(fullPacket);
            }catch(Exception e) { DoLog(e.Message + "->" + fullPacket, true); }
            
            AnalyzeMessage(fullPacket, messObj, session);
        }
        /// <summary>
        /// 具体数据业务操作
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messObj"></param>
        /// <param name="session"></param>
        private void AnalyzeMessage(string message,SocketMessage messObj,TcpSession session)
        {
            switch (messObj.Action)
            {
                case "Login"://登录
                    LoginOperation(messObj, session);
                    break;
                case "Talk"://比分数据传入
                    TalkOperation(message, messObj, session);
                    break;
                case "RefreshTVConfigCache":
                    tvConfigListAll = DBHelper.GetTvConfig();//刷新 账号绑定TV关系 缓存
                    break;
                case "HeartBeat"://心跳维持
                    HeartOperation(messObj, session);
                    break;
            }
        }
        /// <summary>
        /// 登录具体操作
        /// </summary>
        /// <param name="messObj"></param>
        /// <param name="obj"></param>
        private void LoginOperation(SocketMessage messObj, TcpSession session)
        {
           
            try
            {
                var tempObj = socketObjList.Where(item => item.UserCode == messObj.Code).FirstOrDefault();
                if (tempObj == null)
                {
                    SocketObject obj = new SocketObject();
                    obj.RemoteEndpoint = session.RemoteEndPoint;
                    obj.IsOnline = true;
                    obj.SessionId = session.SessionID;
                    obj.UserCode = messObj.Code;
                    obj.DisplayText = messObj.Code + "\t在线";
                    socketObjList.Add(obj);

                }
                else
                {
                    tempObj.SessionId = session.SessionID;
                    tempObj.IsOnline = true;
                    tempObj.RemoteEndpoint = session.RemoteEndPoint;
                    tempObj.DisplayText = messObj.Code + "\t在线";
                }


                SetDataSource();
                byte[] success = ConvertServerMsgToByte(messObj.Code + " 登陆成功 ");
                session.Send(success, 0, success.Length);
            }catch (Exception e)
            {
                // CloseSocket(obj.ClientSocket);
            }
            if (IsShowLog)
                logQueue.Enqueue(new SocketLog { FileFlag = false, Log = messObj.Code + "【" + session.RemoteEndPoint + "】连接成功" });
        }
        /// <summary>
        /// 比分数据具体操作
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messObj"></param>
        /// <param name="obj"></param>
        private void TalkOperation(string message, SocketMessage messObj, TcpSession session)
        {

            if (IsShowLog)
                logQueue.Enqueue(new SocketLog { FileFlag = false, Log = "当前客户数:" + socketObjList.Where(item => item.IsOnline).Count() + "->接收比分数据：" + message });

            ///////////////////////////////////////////////////////////////////
            //获取裁判账号绑定的 TV 集合
            var bindTVList = tvConfigListAll.Where(e => e.UserCode == messObj.Code).ToList()
                ?? new List<TVConfig>();
            //循环转发消息到此TV集合
            foreach (var item in bindTVList)
            {
                var tempObj = socketObjList.ToList().Where(item1 => item1.UserCode == item.TVCode && item1.IsOnline).FirstOrDefault();
                if (tempObj == null)
                    continue;
                byte[] dataBytes = ConvertMessageToByte(message);
                tempObj.MessObj = messObj;
                tempObj.SendMessage = message;
                try
                {
                    var tempSession = session.AppServer.GetSessionByID(tempObj.SessionId);
                    if (IsShowLog)
                    {

                        logQueue.Enqueue(new SocketLog { FileFlag = false, Log = "开始向" + tempSession.RemoteEndPoint + "【" + tempObj.UserCode + "】发送数据" });
                    }
                    tempSession.Send(dataBytes, 0, dataBytes.Length);

                }
                catch (Exception e)
                {

                }
            }
            /////////////假如 IsEnableLiveScore 为TRUE，此条数据转发到远程SOCKET服务器
            if (messObj.IsLive == true)
            {
                if (!messObj.Data.IsSplit)//不是拆场数据才转发给直播
                {
                    string liveMess = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<LiveMessage>(message)).Trim();
                    // new Thread(() =>
                    //{
                    try
                    {
                        bool result = SocketClient.SendMessage(liveMess + Terminator.packet_sep);//转发直播比分数据到云SOCKET加入分割符

                        if (IsShowLog && result)
                            logQueue.Enqueue(new SocketLog { FileFlag = false, Log = "转发直播比分数据:" + message });
                    }
                    catch (Exception e) { }
                    // }).Start();
                }
            }

        }
        /// <summary>
        /// 心跳包操作
        /// </summary>
        /// <param name="messObj"></param>
        /// /// <param name="obj"></param>
        private void HeartOperation(SocketMessage messObj, TcpSession session)
        {
            if (heartFlag)
            {
                try
                {
                    var tempObj = socketObjList.Where(item => item.UserCode == messObj.Code).FirstOrDefault();
                    if (tempObj == null)
                    {
                        SocketObject obj = new SocketObject();
                        obj.RemoteEndpoint = session.RemoteEndPoint;
                        obj.IsOnline = true;
                        obj.SessionId = session.SessionID;
                        obj.DisplayText = messObj.Code + "\t在线";
                        socketObjList.Add(obj);

                    }
                    else
                    {
                        tempObj.SessionId = session.SessionID;
                        tempObj.IsOnline = true;
                        tempObj.RemoteEndpoint = session.RemoteEndPoint;
                        tempObj.DisplayText = messObj.Code + "\t在线";
                    }


                    SetDataSource();
                    byte[] success = ConvertServerMsgToByte(messObj.Code + " 登陆成功 ");
                    session.Send(success, 0, success.Length);
                }
                catch (Exception e)
                {

                }
            }
        }


        #region  日志
        private delegate void DelegateDoLog(string mess);
        private void WriteLog(string mess)
        {

            if (logCount > logMaxCount)
            {
                logCount = 0;
                txt_message.Clear();
            }

            logCount += 1;
            string log = string.Format("{0}：{1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), mess);
            txt_message.AppendText(log);
            txt_message.ScrollToCaret();


        }
        /// <summary>
        /// 日志写入主界面日志框
        /// </summary>
        /// <param name="message">消息体</param>
        /// <param name="isFileLog">是否写入文件</param>
        /// <param name="notErrorLog">是否是错误日志(只针对写入文件时有效)</param>
        public void DoLog(string message, bool isFileLog = false, bool notErrorLog = false)
        {
            if (isFileLog)
            {
                if (!notErrorLog)
                    FileLog.WriteLog(message);
                else
                    FileLog.WriteLog(message, false);
                return;
            }

            if (txt_message.InvokeRequired)
            {
                txt_message.Invoke(new DelegateDoLog(WriteLog), message);
                //new Thread(() => { txt_message.BeginInvoke(new DelegateDoLog(WriteLog), message); }).Start();
            }
            else
            {
                WriteLog(message);
            }

        }
        #endregion
  
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要关闭吗?", "关闭提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
                server.Stop();
            else
                e.Cancel = true;
        }

        private void btn_log_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (IsShowLog)
            {
                btn.Text = "显示日志";
                IsShowLog = false;
                logTimer.Stop();
                logQueue = new ConcurrentQueue<SocketLog>();
            }
            else
            {
                btn.Text = "关闭日志";
                IsShowLog = true;
                logTimer.Start();
            }
        }
        private byte[] ConvertServerMsgToByte(String message)
        {
            SocketMessage so = new SocketMessage();
            so.ServerMessage = message;
            message = JsonConvert.SerializeObject(so);
            //byte[] messageByte = Encoding.UTF8.GetBytes(aes.Encrypt(message) + "\n");
            byte[] messageByte = Encoding.UTF8.GetBytes(message + "\n");
            return messageByte;
        }

        private byte[] ConvertMessageToByte(String message)
        {
            //message = aes.Encrypt(message.Trim()) + "\n";
            message = message.Trim() + "\n";
            byte[] messageByte = Encoding.UTF8.GetBytes(message);
            return messageByte;
        }

        private void btn_listen_Click(object sender, EventArgs e)
        {
            StartSocket();
            if (server != null)
            {
                btn_listen.Enabled = false;
                btn_close.Enabled = true;
            }

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (socketConf.IntranetHttpIpAndPort == txt_inner_iis.Text.Trim() && socketConf.IntranetSocketIpAndPort == txt_inner_socket.Text.Trim() && socketConf.SocketIpAndPort == txt_cloud_socket.Text.Trim())
            {
                MessageBox.Show("内容未修改,操作取消", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                socketConf.IntranetHttpIpAndPort = txt_inner_iis.Text.Trim();
                socketConf.IntranetSocketIpAndPort = txt_inner_socket.Text.Trim();
                socketConf.SocketIpAndPort = txt_cloud_socket.Text.Trim();
            }
            DialogResult dr = MessageBox.Show("确定要保存配置吗?", "保存提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                try
                {
                    int result = DBHelper.SaveSocketConfig(txt_cloud_socket.Text.Trim(), txt_inner_socket.Text.Trim(), txt_inner_iis.Text.Trim());
                    if (result > 0)
                        MessageBox.Show("保存成功", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception e2)
                {
                    DoLog(e2.TargetSite + "->" + e2.Message, true);
                }
            }
        }
        /// <summary>
        /// 显示SOCKET配置地址
        /// </summary>
        private void ShowSocketConf()
        {
            try
            {
                socketConf = DBHelper.GetSocketConfig();
            }
            catch (Exception e)
            {
                DoLog(e.Source + "->" + e.TargetSite + "->" + e.Message);
                return;
            }
            this.txt_cloud_socket.Text = socketConf.SocketIpAndPort;
            this.txt_inner_socket.Text = socketConf.IntranetSocketIpAndPort;
            this.txt_inner_iis.Text = socketConf.IntranetHttpIpAndPort;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            server.Stop();
            btn_listen.Enabled = true;
            btn_close.Enabled = false;

            txt_message.Clear();
            logQueue = new ConcurrentQueue<SocketLog>();
            socketObjList.Clear();
            this.list_SocketClient.DataSource = null;
            this.lb_count.Text = string.Empty;
        }

        private void btGameOper_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_message.Clear();
        }

        private void btn_heart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (heartFlag)
            {
                btn.Text = "启用心跳处理";
                heartFlag = false;

            }
            else
            {
                btn.Text = "关闭心跳处理";
                heartFlag = true;
            }
        }
    }
}
