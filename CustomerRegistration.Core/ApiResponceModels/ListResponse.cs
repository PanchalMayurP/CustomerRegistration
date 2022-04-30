namespace CustomerRegistration.Core.ApiResponceModels;
public class ListResponse<TModel>
{
    public string Result { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public List<TModel> Data { get; set; }
    public long TotalRecords { get; set; }

}
