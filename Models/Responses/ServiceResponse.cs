namespace AWSS3Service.Models.Responses
{
    public class ServiceResponse
    {
        public ServiceResponse(string Message, bool Success)
        {
            this.Message = Message;
            this.Success = Success;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
    }
}