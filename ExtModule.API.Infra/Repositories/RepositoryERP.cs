using ExtModule.API.Application.Interfaces;
using ExtModule.API.Core.ERP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Xml.Linq;


using ExtModule.API.Core;
using Newtonsoft.Json;
using System.Reflection;
using System.Xml;

namespace ExtModule.API.Infrastructure.Repositories
{
    public class RepositoryERP : IERPRepository
    {
        public async Task<DataTable> GetDataTableByStoredProcedure(string spName, Hashtable Params, string CompId)
        {
            var dt = new DataTable();
            string conString = GetConnectionString(CompId);

            SqlConnection con = new SqlConnection(conString);

            try
            {
                SqlCommand cmd = new SqlCommand(spName, con);
                if (Params != null)
                {
                    foreach (DictionaryEntry s in Params)
                    {
                        cmd.Parameters.AddWithValue("@" + s.Key, s.Value.ToString());
                    }
                }

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new CustomException("GetDataTableByStoredProcedure",ex);
               
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dt;
        }
        public async Task<DataSet> GetDataSetByStoredProcedure(string spName, Hashtable Params, string CompId)
        {
            var ds = new DataSet();
            string conString = GetConnectionString(CompId);

            SqlConnection con = new SqlConnection(conString);

            try
            {
                SqlCommand cmd = new SqlCommand(spName, con);
                if (Params != null)
                {
                    foreach (DictionaryEntry s in Params)
                    {

                        cmd.Parameters.AddWithValue("@" + s.Key, s.Value.ToString());
                    }
                }

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new CustomException("GetDataSetByStoredProcedure", ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return ds;
        }

        public async Task<string> GetScalarByStoredProcedure(string spName, Hashtable Params, string CompId)
        {
            string retval= "" ;
            string ConnStr = GetConnectionString(CompId);
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            try
            {
                SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection);
                if (Params != null)
                {
                    foreach (DictionaryEntry Param in Params)
                    {
                        sqlCommand.Parameters.AddWithValue("@" + Param.Key, Param.Value);
                    }
                }

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                retval = Convert.ToString(sqlCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new CustomException("GetScalarByStoredProcedure", ex);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return await Task.FromResult(retval);

        }

        public async Task<DataTable> GetMasterData(int iMasterTypeId, string[] Columns, string Condition, string CompId)
        {
            var dt = new DataTable();
            string conString = GetConnectionString(CompId);

            SqlConnection con = new SqlConnection(conString);

            try
            {
                string text = "Select " + string.Join(",", Columns) + " From " + GetTableNameOfTag(iMasterTypeId, CompId, conString) + " Where iStatus <> 5 and sCode <>'' ";
                if (!string.IsNullOrEmpty(Condition))
                {
                    text = text + " and " + Condition;
                }

                dt = GetDataTableByQuery(text, CompId);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new CustomException("GetMasterData", ex);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dt;
        }

        public async Task<string> InsertByStoredProcedure(string spName, Hashtable Params, string CompId)
         {
            string retval = "";
            string ConnStr = GetConnectionString(CompId);
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            try
            {
                SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection);
                if (Params != null)
                {
                    foreach (DictionaryEntry Param in Params)
                    {
                        sqlCommand.Parameters.AddWithValue("@" + Param.Key, Param.Value);
                    }
                }

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                retval = Convert.ToString(sqlCommand.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                throw new CustomException("InsertByStoredProcedure", ex);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return await Task.FromResult(retval);
         }

        public async Task<string> BulkInsertByStoredProcedure(string spName, List<Hashtable> Params, string CompId)
        {

            int rows = 0;
            string ConnStr = GetConnectionString(CompId);
            SqlCommand sqlCommand = null;
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            try
            {
                foreach(Hashtable s in Params)
                {
                    sqlCommand = new SqlCommand(spName, sqlConnection);
                    if (Params != null)
                    {
                        foreach (DictionaryEntry Param in s)
                        {
                            sqlCommand.Parameters.AddWithValue("@" + Param.Key, Param.Value);
                        }
                    }

                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    rows = rows+ (sqlCommand.ExecuteNonQuery());
                }
                 
            }
            catch (Exception ex)
            {
                throw new CustomException("BulkInsertByStoredProcedure", ex);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return await Task.FromResult(rows.ToString());
        }
      
       
        public async Task<FocusPostResponse<TestAPI.PostResponse>> PostToJVByStoredProcedureVoucherWise(string spName, Hashtable Param, string SessionId, string CompId,string baseFocusAPIUrl)
        {
            FocusPostResponse<TestAPI.PostResponse> res = new FocusPostResponse<TestAPI.PostResponse>();
           
            string err = "";
            try
            {

                if (!string.IsNullOrEmpty(CompId))
                {
                    // Hashtable Param = JsonConvert.DeserializeObject<Hashtable>(obj.Param);


                    DataSet ds = await GetDataSetByStoredProcedure(spName, Param, CompId);
                    if (ds.Tables.Count == 2)
                    {
                        DataTable dt_h = ds.Tables[0];
                        DataTable dt_b = ds.Tables[1];
                        if (dt_h.Rows.Count > 0)
                        {
                            Hashtable header = new Hashtable();
                            foreach (DataColumn dc in dt_h.Columns)
                            {
                                if (!dc.ColumnName.Contains('*'))
                                    header.Add(dc.ColumnName, dt_h.Rows[0][dc.ColumnName]);
                            }
                            string PostVoucher = dt_h.Rows[0]["*PostToVoucher"].ToString();
                            if (dt_b.Rows.Count > 0)
                            {
                                List<Hashtable> body = new List<Hashtable>();
                                foreach (DataRow dr in dt_b.Rows)
                                {
                                    Hashtable row = new Hashtable();
                                    foreach (DataColumn dc in dt_b.Columns)
                                    {
                                        if (!dc.ColumnName.Contains('*'))
                                        {
                                            row.Add(dc.ColumnName, dr[dc.ColumnName]);
                                        }
                                    }
                                   
                                    body.Add(row);
                                }


                                TestAPI.PostingData postingData = new TestAPI.PostingData();
                                postingData.data.Add(new Hashtable { { "Header", header }, { "Body", body } });
                                string sContent = JsonConvert.SerializeObject(postingData);
                                //xlib.EventLog(sContent);
                                string url ="";
                                var response = Focus8API.Post(url, sContent, SessionId, ref err);
                                if (response != null)
                                {
                                    var responseData = JsonConvert.DeserializeObject<TestAPI.PostResponse>(response);
                                    if (responseData.result == 1)
                                    {
                                        res.sMessage = "success";
                                        res.F8APIPost = responseData;
                                        res.status = 1;
                                        
                                        return res;
                                    }
                                    else
                                    {
                                        res.sMessage = "Failed";
                                        res.F8APIPost = responseData;
                                        res.status = 0;
                                    }
                                }

                            }
                            else
                            {
                                res.sMessage = "No data for the body part";
                                res.F8APIPost = null;
                                res.status = 0;
                            }
                        }
                        else
                        {
                            res.sMessage = "No data for the header part";
                            res.F8APIPost = null;
                            res.status = 0;
                        }


                        return res;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new CustomException("PostToJVByStoredProcedureVoucherWise", ex);
            }

            return res;

        }


        #region common functions
        public string GetConnectionString(string CompId)
        {
            string constring = "";
            try
            {
                CRM.ServerDetail objSer = GetCompanyServerDetailsById(CompId, "config", "ServerConfig");
                constring = "server=" + objSer.ServerName + ";Database=" + objSer.DatabaseName + " ;Integrated Security=false;User ID=" + objSer.UserID + ";password=" + objSer.password;
            }
            catch (Exception ex)
            {

            }
            return constring;
        }
        public CRM.ServerDetail GetCompanyServerDetailsById(string compId, string fileName, string rootNode)
        {
            CRM.ServerDetail serverDetail = new CRM.ServerDetail();
            try
            {
                if (!string.IsNullOrEmpty(compId))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                    xmlDocument.Load(directoryName + "\\XMLFiles\\" + fileName + ".xml");
                    foreach (XmlNode item in xmlDocument.SelectNodes(rootNode))
                    {
                        foreach (XmlNode item2 in item.SelectNodes("Company"))
                        {
                            string innerText = item2.SelectSingleNode("CompId").InnerText;
                            if (innerText == compId)
                            {
                                serverDetail.ServerName = item2.SelectSingleNode("ServerName").InnerText;
                                serverDetail.password = item2.SelectSingleNode("Password").InnerText;
                                serverDetail.DatabaseName = item2.SelectSingleNode("DatabaseName").InnerText;
                                serverDetail.UserID = item2.SelectSingleNode("UserID").InnerText;
                                return serverDetail;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                // ErrLog(err, "GetCompanyServerDetails ");
            }

            return serverDetail;
        }
        private string GetTableNameOfTag(int iMasterTypeId, string CompId, string connString = "")
        {
            string result = "";
            try
            {
                string text = "";
                text = "Select 'v' + sModule +'_' + sMasterName [TableName] From cCore_MasterDef Where iMasterTypeId = " + Convert.ToString(iMasterTypeId);
                result = GetScalarByQuery(text, CompId);
            }
            catch (Exception err)
            {
                //ErrLog(err, "DevLib.GetTableNameOfTag()");
            }

            return result;
        }
        public string GetScalarByQuery(string strQry, string CompId)
        {
            string result = "";
          
             string ConnStr=GetConnectionString(CompId);
            

            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            try
            {
                SqlCommand sqlCommand = new SqlCommand(strQry, sqlConnection);
                sqlConnection.Open();
                result = Convert.ToString(sqlCommand.ExecuteScalar());
            }
            catch (Exception err)
            {
               // ErrLog(err, "DevLib.GetScalarByQuery()");
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return result;
        }

        public DataTable GetDataTableByQuery(string strQry, string CompId = "")
        {
        
            DataTable dataTable = new DataTable();
            string ConnStr=GetConnectionString(CompId);
            
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            try
            {
                SqlCommand sqlCommand = new SqlCommand(strQry, sqlConnection);
                int commandTimeout = 6000;
                sqlCommand.CommandTimeout = commandTimeout;
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
               
                sqlDataAdapter.Fill(dataTable);
            
            }
            catch (Exception err)
            {
               // ErrLog(err, "DevLibWeb.GetDataTableByQuery(" + strQry + ")");
            }
            finally
            {
            }

            //EventLog("GetDataTable - OUT");
            return dataTable;
        }

      


        #endregion

        #region API
        public class Focus8API
        {
            public static string Post(string url, string data, string sessionId, ref string err)
            {                
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.Encoding = Encoding.UTF8;
                        webClient.Headers.Add("fSessionId", sessionId);
                        webClient.Headers.Add("Content-Type", "application/json");
                        webClient.Timeout = 300000;
                        string text = webClient.UploadString(url, data);
                        //devLibWeb.EventLog("response:" + Convert.ToString(text), "xdevlibApi");
                        return text;
                    };                   
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    return null;
                }
            }

           
            
         

            public static string GetApi(string url, string sessionId, ref string err)
            {
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.Encoding = Encoding.UTF8;
                        webClient.Headers.Add("fSessionId", sessionId);
                        webClient.Timeout = 300000;
                        return webClient.DownloadString(url);
                    }
                
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    return null;
                }
            }

         
            public class WebClient : System.Net.WebClient
            {
                public int Timeout { get; set; }

                protected override WebRequest GetWebRequest(Uri uri)
                {
                    WebRequest webRequest = base.GetWebRequest(uri);
                    webRequest.Timeout = Timeout;
                    ((HttpWebRequest)webRequest).ReadWriteTimeout = Timeout;
                    return webRequest;
                }
            }
        }
        #endregion
    }
}
