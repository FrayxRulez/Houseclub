namespace Clubhouse.Services
{
    public class BaseResponse
    {
        public bool success { get; set; }
        public string errorMessage { get; set; }

        public override string ToString()
        {
            return "BaseResponse{" +
                    "success=" + success +
                    ", errorMessage='" + errorMessage + '\'' +
                    '}';
        }
    }
}
