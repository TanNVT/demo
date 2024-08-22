using System.Net;

namespace NewDemoWebApp.Common
{
    public class ApiResponse
    {
        public class Response
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public object Content { get; set; }
        }

        public static Response Ok(object value = null)
        {
            var rs = new Response
            {
                Code = "200",
                Content = value
            };
            return rs;
        }
        public static Response Error(string message, string errorCode, object value = null)
        {
            var rs = new Response
            {
                Message = message,
                Code = errorCode,
                Content = value
            };
            return rs;
        }

        public static void ErrorApiResult(string message, HttpStatusCode statusCode, string errorCode, object value = null)
        {
            throw new ApiException((int)statusCode, errorCode, message, value);
        }
    }
}
