using Microsoft.AspNetCore.Mvc;
using ExtModule.API.Application.Factory;
using ExtModule.API.Core;
using System.Data;
using ExtModule.API.Model.DataResponse;
using ExtModule.API.Model.DataRequest;
using System.Collections;
using ExtModule.API.Core.ERP;
using ExtModule.API.Logging;
using log4net;
using log4net.Config;
using System.Reflection;


namespace ExtModule.API.Controllers
{
    [ApiController]    
    public class DataController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public DataController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
       
        }
        #region Test
        [HttpGet]
        [Route("api/Data")]
        public string[] Get()
        {
            string filename = "test";
             string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
            Logger.Instance.LogInfo(filename, "entered");
            return Summaries;
        }
        #endregion

        #region GetDataTableByStoredProcedure
        [HttpPost]
        [Route("api/{type}/Data/GetDataBySp")]
        public async Task<IActionResult> GetDataTableByStoredProcedure(DbCallStoredProcedureInput obj, string type)
        {
            var objRes = new APIResponse<DataTable>();
          string fileName= type + "_"+obj.CompId;
            string conString = "";
            if (obj != null)
            {
                try
                {                    
                    var repository = _repositoryFactory.CreateRepository(type);
                    DataTable dt = await repository.GetDataTableByStoredProcedure(obj.SPName, obj.Param, obj.CompId);

                    objRes.data = dt;
                    objRes.status = 1;
                    objRes.sMessage = "success";
                }
                catch (Exception ex)
                {
                    Logger.Instance.LogError(fileName,ex.Message,ex);
                    objRes.status = 0;
                    objRes.sMessage = ex.Message;
                }
            }
            return Ok(objRes);

        }

        #endregion

        #region GetScalarBySP
        [HttpPost]
        [Route("api/{type}/Data/GetScalarBySP")]
        public async Task<APIResponse<string>> GetScalarBySP(DbCallStoredProcedureInput obj, string type)
        {
            var objRes = new APIResponse<string>();

            string conString = "";
            if (obj != null)
            {
                try
                {
                    var repository = _repositoryFactory.CreateRepository(type);
                    string val = await repository.GetScalarByStoredProcedure(obj.SPName, obj.Param, obj.CompId);

                    objRes.data = val;
                    objRes.status = 1;
                    objRes.sMessage = "success";
                }
                catch (Exception ex)
                {
                    objRes.status = 0;
                    objRes.sMessage = ex.Message;
                }
            }
            return objRes;

        }

        #endregion

        #region GetMasterData
        [HttpPost]
        [Route("api/{type}/Data/GetMasterData")]
        public async Task<IActionResult> GetMasterData(DbCallMasterInput obj, string type)
        {
            var objRes = new APIResponse<DataTable>();

            string conString = "";
            if (obj != null)
            {
                try
                {
                    var repository = _repositoryFactory.CreateRepository(type);
                    DataTable dt = await repository.GetMasterData(obj.MasterTypeId,obj.Columns,obj.Condition,  obj.CompId);

                    objRes.data = dt;
                    objRes.status = 1;
                    objRes.sMessage = "success";
                }
                catch (Exception ex)
                {
                    objRes.status = 0;
                    objRes.sMessage = ex.Message;
                }
            }
            return Ok(objRes);

        }

        #endregion

        #region InsertBySP
        [HttpPost]
        [Route("api/{type}/Data/InsertBySP")]
        public async Task<APIResponse<int>> InsertBySP(DbCallStoredProcedureInput obj, string type)
        {
            var objRes = new APIResponse<int>();

            string conString = "";
            if (obj != null)
            {
                try
                {
                    var repository = _repositoryFactory.CreateRepository(type);
                    string rows = await (repository.InsertByStoredProcedure(obj.SPName, obj.Param, obj.CompId));

                    objRes.data = int.Parse(rows);
                    objRes.status = 1;
                    objRes.sMessage = "success";
                }
                catch (Exception ex)
                {
                    objRes.status = 0;
                    objRes.sMessage = ex.Message;
                }
            }
            return objRes;

        }

        #endregion

        #region BulkInsertBySP
        [HttpPost]
        [Route("api/{type}/Data/BulkInsertBySP")]
        public async Task<APIResponse<string>> BulkInsertBySP(DbCallBulkInputBySp obj, string type)
        {
            var objRes = new APIResponse<string>();

            string conString = "";
            if (obj != null)
            {
                try
                {
                    var repository = _repositoryFactory.CreateRepository(type);
                    string rows = await (repository.BulkInsertByStoredProcedure(obj.SPName, obj.Params, obj.CompId));

                    objRes.data = rows;
                    objRes.status = 1;
                    objRes.sMessage = "success";
                }
                catch (Exception ex)
                {
                    objRes.status = 0;
                    objRes.sMessage = ex.Message;
                }
            }
            return objRes;

        }

        #endregion

       
    }
}
