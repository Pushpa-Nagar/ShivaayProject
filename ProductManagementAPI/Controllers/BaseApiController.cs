using Common.CommonEnum;
using Common.ViewModels.AuditLogModel;
using Common.ViewModels.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        DateTime StartDate;
        DateTime EndDate;
        protected async Task<IActionResult> Execute(Func<Task<IActionResult>> action)
        {
            try
            {
                StartDate = DateTime.UtcNow;
                var result = action().GetAwaiter().GetResult();
                EndDate = DateTime.UtcNow;
                AuditLogging(StartDate, EndDate, true);
                return result;
            }
            catch (Exception ex)
            {
                AuditLogging(StartDate, EndDate, false, ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                BaseResponseView response = new BaseResponseView();
                logger.Error(ex, "Stopped program because of exception");
                response.Message = "Stopped program because of exception.";
                response.Type = "Error";
                response.Code = Convert.ToInt32(ResponseCodeEnum.Failed);
                return BadRequest(response);
            }
        }

        void AuditLogging(DateTime startDate, DateTime endDate, bool status, string message = "Success")
        {
            TimeSpan timeDiff = endDate - startDate;
            int ms = (int)timeDiff.TotalMilliseconds;

            long UserId = 0;
            if (HttpContext.Request.Headers.ContainsKey("userid"))
            {
                UserId = Convert.ToInt64(HttpContext.Request.Headers["userid"]);
            }
            var absoluteUri = string.Concat(
                        HttpContext.Request.Scheme,
                        "://",
                        HttpContext.Request.Host.ToUriComponent(),
                        HttpContext.Request.PathBase.ToUriComponent(),
                        HttpContext.Request.Path.ToUriComponent(),
                        HttpContext.Request.QueryString.ToUriComponent());
            //AuditLogger.WriteLog(new AuditLogView { LoggedDate = DateTime.Now, StartDate = startDate, EndDate = endDate, Url = absoluteUri.ToString(), IsSuccess = status, MethodName = HttpContext.Request.Path.ToUriComponent(), MethodType = HttpContext.Request.Method, ProcessedTime = ms.ToString(), UserId = UserId, Message = message, ModuleId = 1 }); // Convert.ToInt16(ModuleType.Login) });
        }
    }
}
