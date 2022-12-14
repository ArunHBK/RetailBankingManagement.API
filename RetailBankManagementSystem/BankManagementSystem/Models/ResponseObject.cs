namespace BankManagementSystem.Models
{
    public class ResponseObject
    {
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public object? Value { get; set; }
    }
}
