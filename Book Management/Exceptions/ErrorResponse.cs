namespace Book_Management.Exceptions
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Title { get; set; }
        public string? ExceptionMessage { get; set; }

        // Parameterless constructor
        public ErrorResponse() { }

        // Constructor with parameters for easy initialization
        public ErrorResponse(int statusCode, string title, string exceptionMessage)
        {
            StatusCode = statusCode;
            Title = title;
            ExceptionMessage = exceptionMessage;
        }
    }
}
