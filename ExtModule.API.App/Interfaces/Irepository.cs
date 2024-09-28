using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtModule.API.Application.Interfaces
{
    public interface IRepository
    {
        Task<DataTable> GetDataTableByStoredProcedure(string spName, Hashtable Params, string Compid);
        Task<string> GetScalarByStoredProcedure(string spName, Hashtable Params, string Compid);
        Task<DataTable> GetMasterData(int iMasterTypeId, string[] Columns, string Condition, string CompId);
        Task<string> InsertByStoredProcedure(string spName, Hashtable Params, string CompId);
        Task<string> BulkInsertByStoredProcedure(string spName, List<Hashtable> Params, string CompId);
     
       
    }
}
