namespace NewDemoWebApp.Common
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public string MessageContent { get; set; }
        public object Content { get; set; }

        public ApiException(int statusCode, string errorCode, string message = null, object value = null)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            MessageContent = message;
            Content = value;
        }
    }
}
