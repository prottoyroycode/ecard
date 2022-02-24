namespace Library.Core.Response
{
    public class CommonResponse
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
