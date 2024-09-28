using ExtModule.API.Application.Factory;
using ExtModule.API.Application.Interfaces;
using ExtModule.API.Core.ERP;
using ExtModule.API.Infrastructure.Repositories;
using ExtModule.API.Model.DataRequest;
using Microsoft.AspNetCore.Mvc;
using ExtModule.API.Logging;
using log4net;
using log4net.Config;

namespace ExtModule.API.Controllers
{
    [ApiController]
    public class F8APIController : ControllerBase
    {
        private readonly IERPRepository _repositoryERP;
        private IWebHostEnvironment Environment;
        private IConfiguration configuration { get; set; }
        public F8APIController(IERPRepository repositoryERP, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _repositoryERP = repositoryERP;
            Environment = environment;
            this.configuration = configuration;
        }

        

        #region PostToJVBySP
        /// <summary>
        /// InterCompanyPostBySP
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/{type}/F8API/PostToJVBySP")]

        public async Task<FocusPostResponse<TestAPI.PostResponse>> PostToJVBySP(FocusAPIInputBySP obj, string type)
        {
            FocusPostResponse<TestAPI.PostResponse> res = new FocusPostResponse<TestAPI.PostResponse>();
            
            string fileName = type + "_" + obj.CompId;
            string baseFocusAPIUrl = configuration.GetValue<string>("AppSettings:FocusAPI");
            try
            {
                if (!string.IsNullOrEmpty(obj.CompId))
                {                    
                    res = await _repositoryERP.PostToJVByStoredProcedureVoucherWise(obj.SPName, obj.Param, obj.SessionId, obj.CompId, baseFocusAPIUrl);

                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogError(fileName, ex.Message, ex);                           
                res.sMessage = ex.Message;
                res.status = 0;  
            }
            return res;
        }
        #endregion
    }
}
