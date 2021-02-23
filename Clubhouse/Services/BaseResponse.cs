using System.Text.Json.Serialization;

namespace Clubhouse.Services
{
    public class BaseResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return "BaseResponse {" +
                    " success = " + Success +
                    ", error_message = '" + ErrorMessage + "'" +
                    " }";
        }
    }
}
