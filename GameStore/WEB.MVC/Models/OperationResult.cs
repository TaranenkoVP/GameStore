namespace Web.MVC.Models
{
    public class OperationResult
    {
        public OperationResult(bool result, string message = null)
        {
            Result = result;
            Message = message;
        }

        public bool Result { get; set; }

        public string Message { get; set; }
    }
}