using System.Text.Json.Serialization;

namespace ByteShop.Application.UseCases.Results;

public class RequestResult<T>
{
    [JsonIgnore]
    public int StatusCode { get; private set; }
    [JsonIgnore]
    public Tuple<string, string>? Header { get; private set; }
    public string Message { get; private set; }
    public T Data { get; private set; }

    public RequestResult<T> Ok(T data, string message = "Requisição realizada com sucesso")
    {
        StatusCode = 200;
        Message = message;
        Data = data;
        return this;
    }
    public RequestResult<T> Ok(
        T data,
        Tuple<string, string> header,
        string message = "Requisição realizada com sucesso")
    {
        StatusCode = 200;
        Message = message;
        Header = header;
        Data = data;
        return this;
    }
    public RequestResult<T> Created(T data)
    {
        StatusCode = 201;
        Message = "Recurso adicionado com sucesso";
        Data = data;
        return this;
    }

    public RequestResult<T> Accepted()
    {
        StatusCode = 202;
        Message = $"Operação realizar com sucesso";
        return this;
    }

    public RequestResult<T> BadRequest(string detail, T data)
    {
        StatusCode = 400;
        Message = $"Falha ao realizar a requisição. Mais detalhes: {detail}";
        Data = data;
        return this;
    }
    public RequestResult<T> NoContext()
    {
        StatusCode = 204;
        Message = $"Sem conteúdo";
        return this;
    }
    public RequestResult<T> NotFound()
    {
        StatusCode = 404;
        Message = $"Recurso não encontrado.";
        return this;
    }


}