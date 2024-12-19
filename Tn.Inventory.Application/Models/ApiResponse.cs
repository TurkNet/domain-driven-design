using System.Text.Json.Serialization;
using Tn.Inventory.Application.Constants;

namespace Tn.Inventory.Application.Models;

public class ApiResponse
{
    public ApiResponse(bool isOk, ApiResponseType responseType, string message)
    {
        IsOk = isOk;
        ResponseType = responseType;
        Message = message;
    }

    public bool IsOk { get; set; }

    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public ApiResponseType ResponseType { get; set; }

    public string Message { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public ApiResponse(bool isOk, ApiResponseType responseType, string message) : base(isOk, responseType, message)
    {
    }

    public ApiResponse(bool isOk, ApiResponseType responseType, string message, T data) : base(isOk, responseType, message)
    {
        Data = data;
    }

    public T? Data { get; set; }
}