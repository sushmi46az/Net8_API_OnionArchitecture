namespace ExtModule.API.Model.DataResponse
{
    public class APIResponse<T>
    {
        public int status { get; set; }
        public string sMessage { get; set; }
        public T data { get; set; }

    }
    

}
