using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtModule.API.Core;
using ExtModule.API.Core.ERP;

namespace ExtModule.API.Application.Interfaces
{
    public interface IERPRepository : IRepository
    {
      
        Task<FocusPostResponse<TestAPI.PostResponse>> PostToJVByStoredProcedureVoucherWise(string spName, Hashtable Param, string SessionId, string CompId,string baseFocusAPIUrl);
    }
}
