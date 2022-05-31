using Common.ViewModels.AuditLogModel;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace LoggerSystem
{
    public class AuditLogger
    {
        static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        public static void WriteLog(AuditLogView loggerView)
        {
            string ConnectionString = AppConfiguration.ConnectionString;
            InsertAuditLog(ConnectionString, loggerView);
        }

        private static void InsertAuditLog(string connectionString, AuditLogView loggerView)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    cmd.Connection = connection;

                    cmd.CommandText = @"Insert into ""Logger"".""AuditLogger""(""UserId"", ""LoggedDate"", ""StartDate"", ""EndDate"", ""ProcessedTime"", ""Status"", ""Url"", ""MethodName"", ""MethodType"", ""Message"",""ModuleId"") values(@UserId, @LoggedDate, @StartDate, @EndDate, @ProcessedTime, @Status, @Url, @MethodName,@MethodType, @Message,@ModuleId)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@UserId", loggerView.UserId));
                    cmd.Parameters.Add(new SqlParameter("@LoggedDate", loggerView.LoggedDate));
                    cmd.Parameters.Add(new SqlParameter("@StartDate", loggerView.StartDate));
                    cmd.Parameters.Add(new SqlParameter("@EndDate", loggerView.EndDate));
                    cmd.Parameters.Add(new SqlParameter("@ProcessedTime", loggerView.ProcessedTime));
                    cmd.Parameters.Add(new SqlParameter("@Status", Convert.ToByte(loggerView.IsSuccess == true ? 1 : 0)));
                    cmd.Parameters.Add(new SqlParameter("@Url", loggerView.Url));
                    cmd.Parameters.Add(new SqlParameter("@MethodName", loggerView.MethodName));
                    cmd.Parameters.Add(new SqlParameter("@MethodType", loggerView.MethodType));
                    cmd.Parameters.Add(new SqlParameter("@Message", loggerView.Message));
                    cmd.Parameters.Add(new SqlParameter("@ModuleId", loggerView.ModuleId));
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Logger.AuditLogger.InsertAuditLog: Stopped program because of exception");
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
    }
}
