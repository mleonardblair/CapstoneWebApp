namespace EcommerceApp.Shared.Models
{
    /// <summary>
    /// Wrapper object containing the data returned from the server.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; } = 200; // Default to 200 OK
    }

}
