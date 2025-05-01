namespace PedidoProdutoCliente.Application.Models.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = true;
        public bool ValidParameters { get; set; } = true;
        public List<string> Messages { get; set; } = [];
        public T? Data { get; set; }

        public BaseResponse() { }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Messages = [message];
        }

        public BaseResponse(bool success, bool parameters, List<string> messages)
        {
            Success = success;
            ValidParameters = parameters;
            Messages = messages;
        }

        public BaseResponse(T data)
        {
            Data = data;
        }
    }
}
