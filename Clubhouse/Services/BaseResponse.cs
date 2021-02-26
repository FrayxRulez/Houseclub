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

    public class PagedResponse : BaseResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("previous")]
        public int? Previous { get; set; }

        [JsonPropertyName("next")]
        public int? Next { get; set; }
    }
}
