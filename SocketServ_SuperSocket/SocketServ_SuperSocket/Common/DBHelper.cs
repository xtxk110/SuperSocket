using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace  SocketServ_SuperSocket
{
    public class DBHelper
    {
        

        private static SqlConnection sqlServerCon = null;
        private static string conStr=ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private static IDbConnection GetConnection()
        {
            try
            {
                sqlServerCon = new SqlConnection(conStr);
                sqlServerCon.Open();

                return sqlServerCon;
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message, "错误提示");
                return sqlServerCon;
            }

        }
        /// <summary>
        /// 获取SOCKET监听的地址（确保数据库上Config表 IntranetSocketIpAndPort字段有值）
        /// </summary>
        /// <returns></returns>
        public static string GetSocket()
        {
            string result = string.Empty;
            string sql = " SELECT TOP 1 IntranetSocketIpAndPort FROM dbo.Config ";
            using (var con = GetConnection())
            {
                result = con.Query<string>(sql).FirstOrDefault();
            }

            return result;
        }
        /// <summary>
        /// 获取云SOCKET监听的地址（确保数据库上Config表 SocketIpAndPort字段有值）
        /// </summary>
        /// <returns></returns>
        public static string GetCloudSocket()
        {
            string result = string.Empty;
            string sql = " SELECT TOP 1 SocketIpAndPort FROM dbo.Config ";
            using (var con = GetConnection())
            {
                result = con.Query<string>(sql).FirstOrDefault();
            }

            return result;
        }
        /// <summary>
        /// 获取SOCKET配置
        /// </summary>
        /// <returns></returns>
        public static Config GetSocketConfig()
        {
            Config result = null;
            string sql = " SELECT TOP 1 SocketIpAndPort,IntranetSocketIpAndPort,IntranetHttpIpAndPort FROM dbo.Config ";
            using (var con = GetConnection())
            {
                result = con.Query<Config>(sql).FirstOrDefault();
            }
            return result;
        }
        /// <summary>
        /// 保存SOCKET配置
        /// </summary>
        /// <param name="cloudSocket">云SOCKET地址</param>
        /// <param name="innerSocket">局域网SOCKET地址</param>
        /// <param name="innerIis">局域网IIS地址</param>
        public static int SaveSocketConfig(string cloudSocket,string innerSocket,string innerIis)
        {
            int result = 0;
            string sql = @"UPDATE Config SET SocketIpAndPort=@cloudSocket,IntranetHttpIpAndPort=@innerIIS,IntranetSocketIpAndPort=@innerSocket";
            using (var con = GetConnection())
            {
                result= con.Execute(sql, new { cloudSocket = cloudSocket, innerIIS = innerIis, innerSocket = innerSocket });
            }
            return result;
        }
        /// <summary>
        /// 根据对阵ID查询对应的裁判
        /// </summary>
        /// <param name="loopId">对阵ID</param>
        /// <returns></returns>
        public static string GetLoopJudge(string loopId)
        {
            string result = string.Empty;
            string sql = " SELECT a.Code FROM UserAccount AS a INNER JOIN GameLoop AS b ON a.Id=b.JudgeId WHERE b.Id=@loopId ";
            using (var con = GetConnection())
            {
                result = con.Query<string>(sql,new { loopId=loopId}).FirstOrDefault();
            }

            return result;
        }
        /// <summary>
        /// 获取TV SOCKET配置
        /// </summary>
        /// <returns></returns>
        public static List<TVConfig> GetTvConfig()
        {
            List<TVConfig> list = new List<TVConfig>();
            string sql = " SELECT * FROM dbo.TVConfig  ORDER BY CreateDate DESC  ";
            using (var con = GetConnection())
            {
                list= con.Query<TVConfig>(sql).ToList();
            }
            return list;
        }
       
    }
   
}
