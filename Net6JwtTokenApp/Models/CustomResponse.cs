using System.Text.Json.Serialization;

namespace Net6JwtTokenApp.Models
{
    public class CustomResponse
    {
        public object Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public static CustomResponse Success(int statusCode, object data)
        {
            return new CustomResponse { Data = data, StatusCode = statusCode, IsSuccess = true };
        }
        public static CustomResponse SuccessWithoutData(int statusCode)
        {
            return new CustomResponse { StatusCode = statusCode, IsSuccess = true };
        }

        public static CustomResponse Error(int statusCode, List<string> errors)
        {
            return new CustomResponse { StatusCode = statusCode, Errors = errors, IsSuccess = false };
        }

        public static CustomResponse Error(int statusCode, string error)
        {
            return new CustomResponse { StatusCode = statusCode, Errors = new List<string> { error }, IsSuccess = false };
        }

    }
}
