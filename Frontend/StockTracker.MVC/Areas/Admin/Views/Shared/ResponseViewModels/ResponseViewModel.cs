
using System.Text.Json.Serialization;

namespace ECommerce.MVC.Areas.Admin.Views.Shared.ResponseViewModels
{
    public class ResponseViewModel<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }

        [JsonPropertyName("success")]
        public bool IsSucceeded { get; set; }
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        public static ResponseViewModel<T> Success(T data, int statusCode)
        {
            return new ResponseViewModel<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSucceeded = true
            };
        }


        public static ResponseViewModel<T> Success(int statusCode)
        {
            return new ResponseViewModel<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSucceeded = true
            };
        }


        public static ResponseViewModel<T> Fail(string error, int statusCode)
        {
            return new ResponseViewModel<T>
            {
                Errors = new List<string> { error },
                StatusCode = statusCode,
                IsSucceeded = false
            };
        }


        public static ResponseViewModel<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseViewModel<T>
            {
                Errors = errors,
                StatusCode = statusCode,
                IsSucceeded = false
            };
        }
    }
}

