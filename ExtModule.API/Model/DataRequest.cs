
using System.Collections;
using System.Text.Json.Serialization;

namespace ExtModule.API.Model.DataRequest
{
    
    public class DbCallStoredProcedureInput
    {
        public string SPName { get; set; }
        public string CompId { get; set; }              
        public Hashtable Param { get; set; }
    }
    public class DbCallMasterInput
    {
        public int MasterTypeId { get; set; }
        public string[] Columns { get; set; }//coulm seperated by comma
        public string Condition { get; set; }
        public string CompId { get; set; }

    }
    public class DbCallBulkInputBySp
    {
        public string SPName { get; set; }
        public string CompId { get; set; }
        public List<Hashtable> Params { get; set; }
    }
    public class FocusAPIInputBySP
    {
        public string SPName { get; set; }
        public string CompId { get; set; }
        public string SessionId { get; set; }
        public Hashtable Param { get; set; }
    }
}
