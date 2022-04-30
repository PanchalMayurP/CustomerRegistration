namespace CustomerRegistration.Core.ApiResponceModels;
public class SingleResponse<TModel>
{
    public string Result { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public TModel Data { get; set; }
}


public class SingleResponse
{
    public string Result { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Data { get; set; }
}

public class SingleResponseForByteData
{
    public string Result { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public byte[] Data { get; set; }
}
