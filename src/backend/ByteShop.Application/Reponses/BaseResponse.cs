namespace ByteShop.Application.Reponses;
public class BaseResponse<Data>
{
    public Data ReturnData { get; set; }
    public string[] Errors { get; set; }
}
