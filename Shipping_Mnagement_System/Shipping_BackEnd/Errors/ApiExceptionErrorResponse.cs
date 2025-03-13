namespace Shipping_APIs.Errors
{
    public class ApiExceptionErrorResponse : ApiErrorResponse
    {
        public string? Details { get; set; }

        public ApiExceptionErrorResponse(int statusCode, string? message = null, string? details = null)
            : base(statusCode, message)
        {
            Details = details;
        }
    }
}
